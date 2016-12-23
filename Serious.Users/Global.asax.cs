using Serious.Users.AppCode;
using Serious.Users.AppCode.Contracts;
using Serious.Users.AppCode.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Serious.Users
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private List<User> _onlineUsers = new List<User>();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {

                    string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                    
                    var roles = new List<string>();
                    IUserRepository userRepo = new UserRepository();
                    User user = userRepo.GetUser(username);
                    roles.Add(user.Role.GetStringValue());
                    
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(username, Constants.FORMS), roles.ToArray());
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            UserManager.Instance.RemoveSession(this.Session.SessionID);
        }
    }
}
