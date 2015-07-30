using System.Web.Mvc;
using Template.Models.Views.Login;
using Template.Repository;
using Thunder;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            UserRepository = new UserRepository();
        }
        public IUserRepository UserRepository { get; set; }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Index
            {
                ReturnUrl = string.IsNullOrWhiteSpace(Request["ReturnUrl"]) ? Url.Content("~/") : Request["ReturnUrl"]
            });
        }

        [HttpPost]
        [SessionPerRequest]
        [ValidateAntiForgeryToken]
        public ActionResult Authentication(Index model)
        {
            if (!ModelState.IsValid) return Notify(NotifyType.Warning, ModelState);

            var user = UserRepository.Find(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "Dados de acesso informado estão inválidos!");
            }
            else
            {
                XerneasAuthentication.SetAuthCookie(user);

                UserRepository.UpdateLastAccess(user);

                return Success(model.ReturnUrl);
            }

            return Notify(NotifyType.Warning, ModelState);
        }
    }
}