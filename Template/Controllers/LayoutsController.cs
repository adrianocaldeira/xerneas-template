using System.Web.Mvc;

namespace Template.Controllers
{
    public class LayoutsController : Controller
    {
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView("Header");
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView("Menu");
        }
    }
}