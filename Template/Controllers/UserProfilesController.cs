using System;
using System.Globalization;
using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Template.Models.Filters;
using Template.Models.Views.UserProfiles;
using Template.Properties;
using Template.Repository;
using Thunder;
using Thunder.Extensions;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization]
    [SessionPerRequest]
    public class UserProfilesController : Controller
    {
        public UserProfilesController()
        {
            UserProfileRepository = new UserProfileRepository();
            ModuleRepository = new ModuleRepository();
        }

        public IUserProfileRepository UserProfileRepository { get; set; }
        public IModuleRepository ModuleRepository { get; set; }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(UserProfileFilter filter)
        {
            return PartialView("_List", UserProfileRepository.Page(filter));
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                if (!UserProfileRepository.CanDelete(id)) return Notify(NotifyType.Warning, Resources.DeleteBlockMessage);

                UserProfileRepository.Delete(id);

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
                UserProfile = new UserProfile { Active = true },
                Modules = ModuleRepository.Parents()
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userProfile = UserProfileRepository.Find(id);

            if (userProfile == null) return new HttpNotFoundResult("Perfil de Usuário {0} não foi encontrado.".With(id));

            return View("Form", new Form
            {
                UserProfile = userProfile,
                Modules = ModuleRepository.Parents()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Prefix = "UserProfile")] UserProfile model)
        {
            ExcludePropertiesWithKeyPart("UserProfile.Functionalities","Id");

            if (!model.IsValid(ModelState, UserProfileRepository)) return Notify(NotifyType.Warning, ModelState);

            if (model.IsNew())
            {
                UserProfileRepository.Create(model);
            }
            else
            {
                UserProfileRepository.Update(model);
            }

            return Success(new
            {
                Message = Resources.SaveWithSuccess,
                Url = Url.Action("Index")
            });
        }
    }
}