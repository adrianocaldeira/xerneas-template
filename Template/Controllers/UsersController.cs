using System;
using System.Globalization;
using System.Web.Mvc;
using Template.Filters;
using Template.Models.Filters;
using Template.Models.Views.Users;
using Template.Repository;
using Thunder;
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
            return Content("New");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Form");
        }
    }
}