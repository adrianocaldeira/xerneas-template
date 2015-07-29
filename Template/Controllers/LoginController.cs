using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Template.Models;
using Template.Repository;
using Thunder.Web.Mvc;
using Controller = System.Web.Mvc.Controller;

namespace Template.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            UserRepository = new UserRepository();
        }
        public IUserRepository UserRepository { get; set; }
        

        public ActionResult Index()
        {
            return View();
        }

        [SessionPerRequest]
        public ActionResult Authentication(string login, string password, string returnUrl)
        {
            var user = UserRepository.Find(login, password);

            XerneasAuthentication.SetAuthCookie(user);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");
        }
    }
}