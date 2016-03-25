using System.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    public class LogoffController : Controller
    {
        public ActionResult Index()
        {
            XerneasAuthentication.SignOut();

            return Redirect("~/");
        }
    }
}