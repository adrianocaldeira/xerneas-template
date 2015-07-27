using System.Web.Mvc;
using Template.Filters;

namespace Template.Controllers
{
    [Authorization(IgnoreActions = "Index")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}