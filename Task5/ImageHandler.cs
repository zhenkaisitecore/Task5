using System.Web;
using Task5.Models;

namespace Task5
{
    public class ImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var requestFile = context.Request.AppRelativeCurrentExecutionFilePath;
            var isAuth = context.User.Identity.IsAuthenticated;
            bool containPref = (isAuth) ? requestFile.Contains(context.Profile.GetPropertyValue("Preferences").ToString()) : false;
            if (isAuth && containPref)
            {
                context.Response.WriteFile(requestFile);
                context.Response.ContentType = "image/jpeg";
            }
            else
            {
                if (!isAuth)
                    context.Response.Write($"These picture aren't for the {context.Profile.GetPropertyValue("Preferences").ToString()} lover!");
                else
                    context.Response.Write("You need to login first so you can view these pictures!");
                context.Response.ContentType = "text/plain";
            }


        }
    }
}