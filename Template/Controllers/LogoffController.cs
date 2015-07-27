using System.Web.Mvc;
using Template.Filters;

namespace Template.Controllers
{
    [Authorization]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
           return Content("Index");
        }

        public ActionResult New()
        {
            return Content("New");
        }
    }
    public class LogoffController : Controller
    {
        public ActionResult Index()
        {
            XerneasAuthentication.SignOut();

            return Redirect("~/");
        }
    }
}