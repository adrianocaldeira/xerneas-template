using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Template.Filters;
using Template.Models.Extensions;
using Template.Models.Views.Modules;
using Template.Repository;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization(IgnoreActions = "Organazer")]
    [SessionPerRequest]
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
    }
}