using System;
using System.Web.Mvc;
using Template.Filters;
using Template.Models;
using Thunder.Data;

namespace Template.Controllers
{
    [Authorization(IgnoreActions = "Index")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Dump();

            return View();
        }

        public ActionResult Dump()
        {
            using (var session = SessionManager.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var modulo = new Module();
                    modulo.Name = "Módulos";
                    modulo.Description = "Módulos do sistema";
                    modulo.CssClass = "fa-sitemap";
                    modulo.Order = 0;
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "Index",
                        Controller = "Modules",
                        Default = true,
                        Description = "Exibição de módulos do sistema",
                        Module = modulo,
                        HttpMethod = "GET",
                        Name = "Exibir"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "List",
                        Controller = "Modules",
                        Description = "Listagem de módulos do sistema",
                        Module = modulo,
                        HttpMethod = "POST",
                        Name = "Listar"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "Delete",
                        Controller = "Modules",
                        Description = "Exclusão de módulos do sistema",
                        Module = modulo,
                        HttpMethod = "DELETE",
                        Name = "Excluir"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "New",
                        Controller = "Modules",
                        Description = "Criação de módulo do sistema",
                        Module = modulo,
                        HttpMethod = "GET",
                        Name = "Criar"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "Edit",
                        Controller = "Modules",
                        Description = "Alteração de módulo do sistema",
                        Module = modulo,
                        HttpMethod = "GET",
                        Name = "Alterar"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "Save",
                        Controller = "Modules",
                        Description = "Salva módulo do sistema",
                        Module = modulo,
                        HttpMethod = "POST",
                        Name = "Salvar"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "Form",
                        Controller = "Functionalities",
                        Description = "Cria/Edita funcionalidade de um módulo",
                        Module = modulo,
                        HttpMethod = "GET",
                        Name = "Criar/Editar Funcionalidade"
                    });
                    modulo.Functionalities.Add(new Functionality
                    {
                        Action = "Save",
                        Controller = "Functionalities",
                        Description = "Salva funcionalidade de um módulo",
                        Module = modulo,
                        HttpMethod = "POST",
                        Name = "Salvar Funcionalidade"
                    });
                    session.Save(modulo);

                    var seguranca = new Module();
                    seguranca.Name = "Segurança";
                    seguranca.Description = "Módulo de segurança";
                    seguranca.CssClass = "fa-lock";
                    seguranca.Order = 1;
                    session.Save(seguranca);

                    var segurancaPerfil = new Module();
                    segurancaPerfil.Parent = seguranca;
                    segurancaPerfil.Name = "Perfis de Usuário";
                    segurancaPerfil.Description = "Perfis de usuários do sistema";
                    segurancaPerfil.Order = 0;
                    segurancaPerfil.Functionalities.Add(new Functionality
                    {
                        Action = "Index",
                        Controller = "UserProfiles",
                        Default = true,
                        Description = "Exibição de perfis de usuário do sistema",
                        Module = segurancaPerfil,
                        HttpMethod = "GET",
                        Name = "Exibir"
                    });
                    segurancaPerfil.Functionalities.Add(new Functionality
                    {
                        Action = "List",
                        Controller = "UserProfiles",
                        Description = "Listagem de perfis de usuário do sistema",
                        Module = segurancaPerfil,
                        HttpMethod = "POST",
                        Name = "Listar"
                    });
                    segurancaPerfil.Functionalities.Add(new Functionality
                    {
                        Action = "Delete",
                        Controller = "UserProfiles",
                        Description = "Exclusão de perfis de usuário do sistema",
                        Module = segurancaPerfil,
                        HttpMethod = "DELETE",
                        Name = "Excluir"
                    });
                    segurancaPerfil.Functionalities.Add(new Functionality
                    {
                        Action = "New",
                        Controller = "UserProfiles",
                        Description = "Criação de perfil de usuário do sistema",
                        Module = segurancaPerfil,
                        HttpMethod = "GET",
                        Name = "Criar"
                    });
                    segurancaPerfil.Functionalities.Add(new Functionality
                    {
                        Action = "Edit",
                        Controller = "UserProfiles",
                        Description = "Alteração de perfil de usuário do sistema",
                        Module = segurancaPerfil,
                        HttpMethod = "GET",
                        Name = "Alterar"
                    });
                    segurancaPerfil.Functionalities.Add(new Functionality
                    {
                        Action = "Save",
                        Controller = "UserProfiles",
                        Description = "Salva perfil de usuário do sistema",
                        Module = segurancaPerfil,
                        HttpMethod = "POST",
                        Name = "Salvar"
                    });
                    session.Save(segurancaPerfil);

                    var segurancaUsuario = new Module();
                    segurancaUsuario.Parent = seguranca;
                    segurancaUsuario.Name = "Usuários";
                    segurancaUsuario.Description = "Usuários do sistema";
                    segurancaUsuario.Order = 1;
                    segurancaUsuario.Functionalities.Add(new Functionality
                    {
                        Action = "Index",
                        Controller = "Users",
                        Default = true,
                        Description = "Exibição de usuário do sistema",
                        Module = segurancaUsuario,
                        HttpMethod = "GET",
                        Name = "Exibir"
                    });
                    segurancaUsuario.Functionalities.Add(new Functionality
                    {
                        Action = "List",
                        Controller = "Users",
                        Description = "Listagem de usuário do sistema",
                        Module = segurancaUsuario,
                        HttpMethod = "POST",
                        Name = "Listar"
                    });
                    segurancaUsuario.Functionalities.Add(new Functionality
                    {
                        Action = "Delete",
                        Controller = "Users",
                        Description = "Exclusão de usuário do sistema",
                        Module = segurancaUsuario,
                        HttpMethod = "Delete",
                        Name = "Excluir"
                    });
                    segurancaUsuario.Functionalities.Add(new Functionality
                    {
                        Action = "New",
                        Controller = "Users",
                        Description = "Criação de usuário do sistema",
                        Module = segurancaUsuario,
                        HttpMethod = "GET",
                        Name = "Criar"
                    });
                    segurancaUsuario.Functionalities.Add(new Functionality
                    {
                        Action = "Edit",
                        Controller = "Users",
                        Description = "Alteração de usuário do sistema",
                        Module = segurancaUsuario,
                        HttpMethod = "GET",
                        Name = "Alterar"
                    });
                    segurancaUsuario.Functionalities.Add(new Functionality
                    {
                        Action = "Save",
                        Controller = "Users",
                        Description = "Salva usuário do sistema",
                        Module = segurancaUsuario,
                        HttpMethod = "POST",
                        Name = "Salvar"
                    });
                    session.Save(segurancaUsuario);
                    
                    var userProfile = new UserProfile();
                    userProfile.Active = true;
                    userProfile.Name = "Master";

                    foreach (var functionality in modulo.Functionalities)
                    {
                        userProfile.Functionalities.Add(functionality);
                    }

                    foreach (var functionality in segurancaPerfil.Functionalities)
                    {
                        userProfile.Functionalities.Add(functionality);
                    }

                    foreach (var functionality in segurancaUsuario.Functionalities)
                    {
                        userProfile.Functionalities.Add(functionality);
                    }
                 
                    session.Save(userProfile);

                    var user = new User();
                    user.Active = true;
                    user.Email = "adcaldeira@outlook.com";
                    user.Login = "adm";
                    user.Name = "Administrador";
                    user.Salt = Guid.NewGuid().ToString("N");
                    user.Password = Models.User.EncriptPassword(user.Salt, "123456");
                    user.Profile = userProfile;
                    session.Save(user);

                    transaction.Commit();
                }
            }

            return Content("OK");
        }
    }
}