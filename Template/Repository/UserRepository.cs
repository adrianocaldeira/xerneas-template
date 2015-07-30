using System;
using System.Linq;
using NHibernate.Linq;
using Template.Models;
using Template.Models.Filters;
using Thunder.Collections;
using Thunder.Data;
using Thunder.Data.Pattern;
using Thunder.Security;

namespace Template.Repository
{
    public class UserRepository : Repository<User,int>, IUserRepository
    {
        /// <summary>
        ///     Cria <see cref="User" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="User" />
        /// </param>
        public new void Create(User entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Localiza usuário
        /// </summary>
        /// <param name="loginOrEmail">Login ou E-mail</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Find(string loginOrEmail, string password)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var user = Session.Query<User>().FirstOrDefault(x => 
                    (x.Login.Trim().ToLower() == loginOrEmail.ToLower() 
                    || x.Email.Trim().ToLower() == loginOrEmail.ToLower()));

                transaction.Commit();
                
                if (user != null && user.Password == password.Encrypt(user.Salt))
                {
                    return user;
                }

                return null;
            }
        }

        /// <summary>
        ///     Atualiza último acesso
        /// </summary>
        /// <param name="user"></param>
        public void UpdateLastAccess(User user)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var userDb = Session.Get<User>(user.Id);

                userDb.LastAccess = DateTime.Now;

                Session.Update(userDb);

                transaction.Commit();
            }
        }

        /// <summary>
        ///     Verifica se o login do usuário já existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="login">Login</param>
        /// <returns>Login existe ou não</returns>
        public bool ExistLogin(int id, string login)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Verifica se o e-mail do usuário já existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="email">E-mail</param>
        /// <returns>E-mail existe ou não</returns>
        public bool ExistEmail(int id, string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Lista <see cref="User" /> paginados
        /// </summary>
        /// <param name="filter">
        ///     <see cref="UserFilter" />
        /// </param>
        /// <returns>Lista de <see cref="User" /></returns>
        public IPaging<User> Page(UserFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Recupera usuário pelo e-mail
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        public User Find(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Verifica se o usuário possui acesso a uma área do sistema
        /// </summary>
        /// <param name="id"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public bool AllowAccess(int id, string controllerName, string actionName, string httpMethod)
        {
            using (var session = SessionManager.SessionFactory.OpenSession())
            {
                var count = session.Query<User>().Count(x => x.Id == id 
                    && x.Profile.Functionalities.Any(y => y.Controller.ToLower() == controllerName 
                            && y.Action.ToLower() == actionName 
                            && y.HttpMethod.ToLower() == httpMethod));

                return count > 0;
            }
        }
    }
}