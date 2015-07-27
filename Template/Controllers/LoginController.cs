using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Template.Models;

namespace Template.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authentication(string login, string password, string returnUrl)
        {
            var user = new User
            {
                Name = "Adriano",
                Email = "adcaldeira@outlook.com",
                Id = 1,
                Login = "adriano",
                Profile = new UserProfile
                {
                    Functionalities = new List<Functionality>
                    {
                        new Functionality
                        {
                            Name = "Home de Usuário",
                            Action = "Index",
                            Controller = "Users",
                            HttpMethod = "GET"
                        }
                    }
                }
            };

            XerneasAuthentication.SetAuthCookie(user);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");
        }
    }
}