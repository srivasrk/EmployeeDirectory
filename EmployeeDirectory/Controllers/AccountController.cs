using System.Web.Mvc;
using System.Web.Security;
using EmployeeDirectory.Helpers;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
			if (User.Identity.IsAuthenticated)
			{
				if (returnUrl != null)
					return Redirect(returnUrl);
				return RedirectToAction("Index", "Home");
			}
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
		
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

	        if (SignInHelper.ValidateCredentials(model.Email, model.Password, model.RememberMe))
	        {
				if (returnUrl != null && !string.IsNullOrWhiteSpace(returnUrl))
					return Redirect(returnUrl);
                Session["IsAdmin"] = SignInHelper.IsAdmin(model.Email);
				return RedirectToAction("Index", "Home");
	        }

			ModelState.AddModelError("Email", "Invalid email/password combination");
	        return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Login", "Account");
        }
    }
}