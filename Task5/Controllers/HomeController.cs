using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Task5.Models;

namespace Task5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MembershipUser user = UserManagerSingleton.GetUser();
            if (user != null && HttpContext.User.Identity.IsAuthenticated)
            {
                if (ViewData["Images"] == null)
                {
                    ViewData["Images"] = DataFactory.GetInstance().GetAllByPreferences((Pets) HttpContext.Profile.GetPropertyValue("Preferences"));
                }
            }

            return View(user);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Favourite()
        {
            MembershipUser user = UserManagerSingleton.GetUser();
            if (user != null)
                ViewData["Images"] = DataFactory.GetInstance().GetFavourites((Pets) HttpContext.Profile.GetPropertyValue("Preferences"));

            return View("~/Views/Home/Index.cshtml", user);
        }
    }
}