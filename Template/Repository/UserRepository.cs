using System;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Template.Models;
using Template.Models.Filters;
using Thunder.Collections;
using Thunder.Collections.Extensions;
using Thunder.Data;
using Thunder.Data.Extensions;
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
            using (var transaction = Session.BeginTransaction())
            {
                entity.Salt = Guid.NewGuid().ToString("N");
                entity.Password = User.EncriptPassword(entity.Salt, entity.Password);

                Session.Save(entity);

                transaction.Commit();
            }
        }

        /// <summary>
        ///     Atualiza usuário
        /// </summary>
        /// <param name="entity">
        ///     <see cref="User" />
        /// </param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        public new User Update(User entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var user = Session.Get<User>(entity.Id);

                user.Name = entity.Name;
                user.Active = entity.Active;
                user.Email = entity.Email;
                user.Login = entity.Login;
                user.Profile = entity.Profile;

                if (!string.IsNullOrWhiteSpace(entity.Password))
                {
                    user.Password = User.EncriptPassword(user.Salt, entity.Password);    
                }

                Session.Update(user);

                transaction.Commit();

                return user;
            }
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
            using (var transaction = Session.BeginTransaction())
            {
                var count = Session.Query<User>().Count(x=> x.Id != id && 
                        x.Login.ToLower() == login.ToLower().Trim()) > 0;

                transaction.Commit();

                return count;
            }
        }

        /// <summary>
        ///     Verifica se o e-mail do usuário já existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="email">E-mail</param>
        /// <returns>E-mail existe ou não</returns>
        public bool ExistEmail(int id, string email)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var count = Session.Query<User>().Count(x => x.Id != id &&
                        x.Email.ToLower() == email.ToLower().Trim()) > 0;

                transaction.Commit();

                return count;
            }
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
            using (var transaction = Session.BeginTransaction())
            {
                var query = Session.Query<User>();

                if (filter.ByActive) query = query.Where(x => x.Active == filter.Active.Value);
                if (filter.ByName) query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
                if (filter.ByProfile) query = query.Where(x => x.Profile.Id == filter.Profile.Id);

                var users = query.OrderBy(x => x.Name).Paging(filter.CurrentPage, filter.PageSize);

                transaction.Commit();

                return users;
            }
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
            using (var transaction = Session.BeginTransaction())
            {
                var user = Session.Query<User>().SingleOrDefault(x => 
                    x.Email.ToLower() == email.ToLower().Trim()) ;

                transaction.Commit();

                return user;
            }
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

        /// <summary>
        /// Pode excluir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CanDelete(int id)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var canDelete = false;
                var user = Session.Get<User>(id);

                if (user.Id != 1)
                {
                    canDelete = true;
                }

                transaction.Commit();

                return canDelete;
            }
        }
    }
}