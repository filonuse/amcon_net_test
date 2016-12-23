using Serious.Users.AppCode.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Serious.Users.Controllers
{
    [SessionExpireFilterAttribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}