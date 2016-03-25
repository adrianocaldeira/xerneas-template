using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using $rootnamespace$.Filters;
using $rootnamespace$.Models;
using $rootnamespace$.Models.Filters;
using $rootnamespace$.Models.Views.Users;
using $rootnamespace$.Properties;
using $rootnamespace$.Repository;
using Thunder;
using Thunder.Extensions;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace $rootnamespace$.Controllers
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
                            new SelectListItem { Selected = true, Text = Resources.All, Value = "0" })
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
                    new SelectListItem { Selected = true, Text = Resources.Select, Value = "" })
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
                    new SelectListItem { Selected = true, Text = Resources.Select, Value = "" })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Prefix = "User")] User model)
        {
            var exclude = new List<string> {"User.Profile.Name", "User.Profile.Functionalities"};

            if (!model.IsNew()) exclude.Add("User.Password");

            ExcludePropertiesInValidation(exclude.ToArray());

            if (!model.IsValid(ModelState, UserRepository)) return Notify(NotifyType.Warning, ModelState);

            if (model.IsNew())
            {
                UserRepository.Create(model);
            }
            else
            {
                UserRepository.Update(model);
            }

            return Success(new { message = Resources.SaveWithSuccess, Url = Url.Action("Index") });
        }
    }
}