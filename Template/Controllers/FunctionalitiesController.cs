using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Template.Models.Views.Functionalities;
using Thunder;
using Thunder.NHibernate;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization]
    [SessionPerRequest]
    public class FunctionalitiesController : Controller
    {
        [HttpPost]
        public ActionResult Form(Functionality functionality, int index)
        {
            return View(new Form
            {
                Functionality = index >= 0 ? functionality : new Functionality(),
                Index = index
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Prefix = "Functionality")]Functionality model, int index)
        {
            if (!ModelState.IsValid) return Notify(NotifyType.Warning, ModelState);
            
            return Success(new
            {
                model.Id,
                model.Name,
                model.Action,
                model.Controller,
                model.Default,
                model.Description,
                model.HttpMethod, index
            });
        }
    }
}