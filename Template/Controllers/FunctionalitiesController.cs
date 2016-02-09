using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Template.Models.Views.Functionalities;
using Thunder;
using Thunder.Web.Mvc;
using Controller = Thunder.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization]
    [SessionPerRequest]
    public class FunctionalitiesController : Controller
    {
        [HttpGet]
        public ActionResult Form(int index)
        {
            return View(new Form
            {
                Index = index,
                Functionality = new Functionality()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Prefix = "Functionality")]Functionality model, int index)
        {
            if (!ModelState.IsValid) return Notify(NotifyType.Warning, ModelState);
            
            return Success(new
            {
                Index = index,
                model.Id,
                model.Name,
                model.Action,
                model.Controller,
                model.Default,
                model.Description,
                model.HttpMethod
            });
        }
    }
}