using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using $rootnamespace$.Models;
using Thunder.Web;

namespace $rootnamespace$
{
    public class XerneasAuthentication
    {
        public static XerneasUserData GetUserData()
        {
            var httpCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(httpCookie.Value);

            return ticket.UserData.Json<XerneasUserData>();    
        }

        public static void SetAuthCookie(User user)
        {
            var authenticationTicket = new FormsAuthenticationTicket(
                version: 1,
                name: user.Name,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                isPersistent: false,
                userData: XerneasUserData.Create(user).Json(Formatting.None),
                cookiePath: FormsAuthentication.FormsCookiePath
                );

            var encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath,
                Domain = FormsAuthentication.CookieDomain
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}