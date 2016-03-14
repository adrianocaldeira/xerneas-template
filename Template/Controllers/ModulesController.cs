using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Template.Filters;
using Template.Models.Extensions;
using Template.Models.Views.Modules;
using Template.Properties;
using Template.Repository;
using Thunder;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;
using Thunder.Extensions;
using Module = Template.Models.Module;

namespace Template.Controllers
{
    [SessionPerRequest]
    [Authorization(IgnoreActions = "Organazer")]
    public class ModulesController : Controller
    {
        public ModulesController()
        {
            ModuleRepository = new ModuleRepository();
        }

        public IModuleRepository ModuleRepository { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View(ModuleRepository.Parents());
        }

        [HttpPost]
        public ActionResult Organazer(string json)
        {
            var modules = TreeView.Create(json).ToModules();

            ModuleRepository.Organizer(modules);

            return Success();
        }

        [HttpGet]
        public ActionResult New(Module parent = null)
        {
            return View("Form", new Module
            {
                Parent = parent != null ? ModuleRepository.Find(parent.Id) : null
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var module = ModuleRepository.Find(id);

            if (module == null)
            {
                return new HttpNotFoundResult("Módulo {0} não foi encontrado.".With(id));
            }

            return View("Form", module);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var module = ModuleRepository.Find(id);

            if (module == null)
            {
                return Notify(NotifyType.Warning, "Módulo {0} não foi encontrado.".With(id));
            }

            if (module.Childs.Any())
            {
                return Notify(NotifyType.Warning, "Módulo não pode ser excluído, pois possui outro(s) módulo(s) relacionado(s).");
            }

            if (ModuleRepository.HasFunctionalitiesRelationshipUserProfiles(module))
            {
                return Notify(NotifyType.Warning, "Módulo não pode ser excluído, pois possui funcionalidade(s) relacionada(s) com algum perfil de usuário.");
            }

            ModuleRepository.Delete(module);

            return Success();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Module module)
        {
            if (module.IsValid(ModuleRepository, ModelState))
            {
                if (module.IsNew())
                {
                    ModuleRepository.Create(module);
                }
                else
                {
                    ModuleRepository.Update(module);
                }

                return Success(new { message = Resources.SaveWithSuccess, Url = Url.Action("Index") });
            }

            return Notify(NotifyType.Warning, ModelState);
        }
    }
}