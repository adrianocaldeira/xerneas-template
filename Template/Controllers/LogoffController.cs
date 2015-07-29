using System.Web.Mvc;

namespace Template.Controllers
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