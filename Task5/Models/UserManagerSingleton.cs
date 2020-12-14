using System;
using System.Web;
using System.Web.Security;

namespace Task5.Models
{
    public class UserManagerSingleton
    {
        public static MembershipUser GetUser()
        {
            if (_userSession() != null) return _userSession();

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string name = HttpContext.Current.User.Identity.Name;
                HttpContext.Current.Session["user"] = Membership.GetUser(name);
                return _userSession();
            }

            return null;

            MembershipUser _userSession() => HttpContext.Current.Session["user"] as MembershipUser;
        }
    }
}