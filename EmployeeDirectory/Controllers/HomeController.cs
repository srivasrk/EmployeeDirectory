using EmployeeDirectory.Helpers;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            Session["IsAdmin"] = SignInHelper.IsAdmin(User.Identity.GetUserName());
            return View();
		}
	}
}