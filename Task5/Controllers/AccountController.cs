using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using Task5.Models;

namespace Task5.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            MembershipUser user = UserManagerSingleton.GetUser();
            if (user != null)
                return RedirectToAction("Index", "Home", user);

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            ActionResult result = null;
            if (ModelState.IsValid)
            {
                var isValidUser = Membership.ValidateUser(login.Username, login.Password);
                if (isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(login.Username, false);
                    result = RedirectToAction("Index", "Home");
                }
                else
                {
                    result = Login();
                }
            }

            return result;
        }

        public ActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(RegisterModel registration)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status;
                var newUser = Membership.CreateUser(registration.UserName, registration.Password, null, null, null, true, out status);
                if (status == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(registration.UserName, false);
                    HttpContext.Profile.Initialize(registration.UserName, true);
                    HttpContext.Profile.SetPropertyValue("Preferences", registration.Preferences);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}