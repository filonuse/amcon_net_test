using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Serious.Users.AppCode
{
    public class UserManager
    {
        private static UserManager _instance;
        private static object _syncRoot = new object();
        private ConcurrentDictionary<string, HttpSessionStateBase> _userSessionDict = new ConcurrentDictionary<string, HttpSessionStateBase>();
        private List<string> _expiredSessionList = new List<string>();

        private UserManager()
        {
        }

        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddUserSession(string sessionId, HttpSessionStateBase session)
        {
            if (_userSessionDict.Keys.Contains(sessionId))
                _userSessionDict.Keys.Remove(sessionId);

            _userSessionDict.TryAdd(sessionId, session);
        }

        public void EndUserSession(string userEmail)
        {
            var endSessions = _userSessionDict.Where(kvs =>
                kvs.Value[Constants.USER_NAME] != null ? kvs.Value[Constants.USER_NAME].ToString() == userEmail : false);

            if (endSessions != null && endSessions.Count() > 0)
            {
                var sessionIdList = endSessions.Select(kv => kv.Key);
                lock (_syncRoot)
                {
                    _expiredSessionList.AddRange(sessionIdList);
                }
                HttpSessionStateBase returnVal = null;
                foreach (var sessionId in endSessions)
                    _userSessionDict.TryRemove(sessionId.Key, out returnVal);
            }
        }

        public void RemoveSession(string sessionId)
        {
            lock (_syncRoot)
            {
                _expiredSessionList.Remove(sessionId);
            }
            HttpSessionStateBase returnVal = null;
            if (_userSessionDict.ContainsKey(sessionId))
                _userSessionDict.TryRemove(sessionId, out returnVal);
        }

        public bool CheckSessionExpired(string sessionId)
        {
            return _expiredSessionList.Contains(sessionId);
        }
    }
}