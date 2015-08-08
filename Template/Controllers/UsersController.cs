using System.Web.Mvc;
using Template.Filters;
using Template.Models.Views.Users;

namespace Template.Controllers
{
    [Authorization]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new Index());
        }

        public ActionResult New()
        {
            return Content("New");
        }
    }
}