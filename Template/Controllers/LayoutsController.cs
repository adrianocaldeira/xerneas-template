using System.Web.Mvc;
using Template.Models;
using Template.Repository;
using Thunder.NHibernate;
using Controller = System.Web.Mvc.Controller;

namespace Template.Controllers
{
    public class LayoutsController : Controller
    {
        public LayoutsController()
        {
            ModuleRepository = new ModuleRepository();
        }

        public IModuleRepository ModuleRepository { get; private set; }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView("Header");
        }

        [ChildActionOnly]
        [SessionPerRequest]
        public ActionResult Menu()
        {
            return PartialView("Menu", ModuleRepository.Find(new User { Id = XerneasAuthentication.GetUserData().Id }));
        }
    }
}