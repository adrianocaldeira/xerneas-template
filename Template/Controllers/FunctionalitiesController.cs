using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Template.Models.Views.Functionalities;
using Template.Properties;
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

        [HttpGet]
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

        private IList<string> GetAllControllers()
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any())
                .Select(x => new { ControllerName = x.DeclaringType.Name.Replace("Controller","") })
                .Select(x=> x.ControllerName)
                .Distinct()
                .OrderBy(x => x).ToList();
        }

        private IList<string> GetAllActions(string controllerName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any()
                        && m.DeclaringType.Name.Replace("Controller","").Equals(controllerName))
                .Select(x => x.Name)
                .Distinct()
                .OrderBy(x => x).ToList();
        }
    }
}