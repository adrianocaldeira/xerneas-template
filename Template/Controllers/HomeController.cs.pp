using System.Web.Mvc;
using $rootnamespace$.Filters;

namespace $rootnamespace$.Controllers
{
    [Authorization(IgnoreActions = "Index")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}