using System.Globalization;
using System.Web.Mvc;
using Template.Filters;
using Template.Repository;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization]
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
    }
}