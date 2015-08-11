using System;
using System.Globalization;
using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Template.Models.Filters;
using Template.Models.Views.Users;
using Template.Repository;
using Thunder;
using Thunder.Extensions;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization]
    [SessionPerRequest]
    public class UsersController : Controller
    {
        public UsersController()
        {
            UserRepository = new UserRepository();
            UserProfileRepository = new UserProfileRepository();
        }

        public IUserRepository UserRepository { get; set; }
        public IUserProfileRepository UserProfileRepository { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new Index
            {
                Profiles = UserProfileRepository.All(x => x.Active).ToSelectList(x => x.Name, x => x.Id.ToString(CultureInfo.InvariantCulture),
                            new SelectListItem { Selected = true, Text = "Todos", Value = "0" })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(UserFilter filter)
        {
            return PartialView("_List", UserRepository.Page(filter));
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                if (!UserRepository.CanDelete(id)) return Notify(NotifyType.Warning, "Esse registro não pode ser excluído, pois está relacionado com outras áreas do sistema.");

                UserRepository.Delete(UserRepository.Find(id));

                return Success();
            }
            catch (Exception ex)
            {
                return Notify(NotifyType.Danger, ex.Message);
            }
        }
        
        [HttpGet]
        public ActionResult New()
        {
            return View("Form", new Form
            {
                User = new User{Active = true},
                Profiles = UserProfileRepository.All(x => x.Active).ToSelectList(x => x.Name, x => x.Id.ToString(CultureInfo.InvariantCulture),
                    new SelectListItem { Selected = true, Text = "Todos", Value = "0" })
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = UserRepository.Find(id);

            if (user == null)
            {
                return new HttpNotFoundResult("Usuário {0} não foi encontrado.".With(id));
            }

            return View("Form", new Form
            {
                User = user,
                Profiles = UserProfileRepository.All(x => x.Active).ToSelectList(x => x.Name, x => x.Id.ToString(CultureInfo.InvariantCulture),
                    new SelectListItem { Selected = true, Text = "Todos", Value = "0" })
            });
        }
    }
}