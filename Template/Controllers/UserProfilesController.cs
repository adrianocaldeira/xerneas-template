using System.Globalization;
using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Template.Models.Filters;
using Template.Models.Views.UserProfiles;
using Template.Properties;
using Template.Repository;
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
    }
}