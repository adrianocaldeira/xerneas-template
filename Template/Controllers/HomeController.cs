using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Thunder.Data;
using Thunder.Web.Mvc;
using Controller = System.Web.Mvc.Controller;

namespace Template.Controllers
{
    [Authorization(IgnoreActions = "Index")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dump();

            return View();
        }

        public ActionResult Dump()
        {
            using (var session = SessionManager.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var seguranca = new Module();
                    seguranca.Name = "Segurança";
                    seguranca.Description = "Módulo de segurança";
                    seguranca.CssClass = "fa-lock";
                    seguranca.Order = 0;
                    session.Save(seguranca);

                    var segurancaPerfil = new Module();
                    segurancaPerfil.Parent = seguranca;
                    segurancaPerfil.Name = "Perfis de Usuário";
                    segurancaPerfil.Description = "Perfis de usuários do sistema";
                    segurancaPerfil.Order = 0;
                    session.Save(segurancaPerfil);

                    var segurancaUsuario = new Module();
                    segurancaUsuario.Parent = seguranca;
                    segurancaUsuario.Name = "Usuários";
                    segurancaUsuario.Description = "Usuários do sistema";
                    segurancaUsuario.Order = 1;
                    session.Save(segurancaUsuario);

                    transaction.Commit();
                }
            }

            return Content("OK");
        }
    }
}