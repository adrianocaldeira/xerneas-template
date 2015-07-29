using System.Web.Mvc;
using Template.Filters;

namespace Template.Controllers
{
    [Authorization]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult New()
        {
            return Content("New");
        }
    }
}