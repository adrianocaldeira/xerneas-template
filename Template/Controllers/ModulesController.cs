using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Template.Filters;
using Template.Models.Extensions;
using Template.Models.Views.Modules;
using Template.Repository;
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
            Assembly asm = Assembly.GetExecutingAssembly();


            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            

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
    }
}