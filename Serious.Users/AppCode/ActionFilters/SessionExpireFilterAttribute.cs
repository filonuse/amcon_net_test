using Serious.Users.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Serious.Users.AppCode.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (UserManager.Instance.CheckSessionExpired(filterContext.HttpContext.Session.SessionID))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    { "controller", "Account" },
                    { "action", "LogOff" }
                 });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}