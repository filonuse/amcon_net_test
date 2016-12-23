using Serious.Users.AppCode;
using Serious.Users.AppCode.ActionFilters;
using Serious.Users.AppCode.Contracts;
using Serious.Users.AppCode.Enums;
using Serious.Users.Models;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Security;

namespace Serious.Users.Controllers
{

    public class AccountController : Controller
    {
        public AccountController()
        {

        }     

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {           
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult OnLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (FormsAuthentication.Authenticate(model.Email, model.Password))//if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    this.Session[Constants.USER_NAME] = model.Email;
                    UserManager.Instance.AddUserSession(this.Session.SessionID, this.Session);

                    if (model.Email.Contains(Constants.MASTER))
                        return base.RedirectToAction("ManageUsers");
                }
                
            }
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult LogOff()
        {
            UserManager.Instance.RemoveSession(this.Session.SessionID);

            FormsAuthentication.SignOut();
            this.Session.Abandon();
            
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles=Constants.MASTER)]
        [SessionExpireFilterAttribute]
        public ActionResult ManageUsers()
        {
            IUserRepository userRepo = new UserRepository();
            return View(new ManageUserViewModel(userRepo.GetAllUsers().ToArray()));
        }

        [HttpPost]
        public JsonResult OnLoggOutUser(string email)
        {
            UserManager.Instance.EndUserSession(email);

            return Json(new
            {
                message = "ok"
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        

        #region helper members
        /// <summary>
        /// Gets the principal.
        /// </summary>
        /// <param name="userEmail">Name of the user.</param>
        /// <returns>IPrincipal.</returns>
        protected IPrincipal GetPrincipal(string userEmail)
        {
            return new GenericPrincipal(new GenericIdentity(userEmail), null);
        }
        
        #endregion

    }
}