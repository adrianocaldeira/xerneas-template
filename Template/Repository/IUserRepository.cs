using Template.Models;
using Template.Models.Filters;
using Thunder.Collections;
using Thunder.Data.Pattern;

namespace Template.Repository
{
    /// <summary>
    ///     Repositório de <see cref="User" />
    /// </summary>
    public interface IUserRepository : IRepository<User, int>
    {
        /// <summary>
        ///     Cria <see cref="User" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="User" />
        /// </param>
        new void Create(User entity);

        /// <summary>
        ///     Localiza usuário
        /// </summary>
        /// <param name="loginOrEmail">Login ou E-mail</param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Find(string loginOrEmail, string password);

        /// <summary>
        ///     Atualiza último acesso
        /// </summary>
        /// <param name="user"></param>
        void UpdateLastAccess(User user);

        /// <summary>
        ///     Verifica se o login do usuário já existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="login">Login</param>
        /// <returns>Login existe ou não</returns>
        bool ExistLogin(int id, string login);

        /// <summary>
        ///     Verifica se o e-mail do usuário já existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="email">E-mail</param>
        /// <returns>E-mail existe ou não</returns>
        bool ExistEmail(int id, string email);

        /// <summary>
        ///     Atualiza usuário
        /// </summary>
        /// <param name="entity">
        ///     <see cref="User" />
        /// </param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        new User Update(User entity);

        /// <summary>
        ///     Lista <see cref="User" /> paginados
        /// </summary>
        /// <param name="filter">
        ///     <see cref="UserFilter" />
        /// </param>
        /// <returns>Lista de <see cref="User" /></returns>
        IPaging<User> Page(UserFilter filter);

        /// <summary>
        ///     Recupera usuário pelo e-mail
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        User Find(string email);

        /// <summary>
        ///     Exclui <see cref="User" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="User" />
        /// </param>
        new void Delete(User entity);

        /// <summary>
        ///     Verifica se o usuário possui acesso a uma área do sistema
        /// </summary>
        /// <param name="id"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        bool AllowAccess(int id, string controllerName, string actionName, string httpMethod);

        /// <summary>
        /// Pode excluir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CanDelete(int id);
    }
}