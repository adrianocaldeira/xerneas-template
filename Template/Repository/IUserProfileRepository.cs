using Template.Models;
using Template.Models.Filters;
using Thunder.Collections;
using Thunder.Data.Pattern;

namespace Template.Repository
{
    /// <summary>
    ///     Repositório de <see cref="UserProfile" />
    /// </summary>
    public interface IUserProfileRepository : IRepository<UserProfile, int>
    {
        /// <summary>
        ///     Lista <see cref="UserProfile" /> paginado
        /// </summary>
        /// <param name="filter">
        ///     <see cref="UserProfileFilter" />
        /// </param>
        /// <returns>Lista de <see cref="UserProfile" /></returns>
        IPaging<UserProfile> Page(UserProfileFilter filter);

        /// <summary>
        ///     Verifica se o perfil pode ser excluído
        /// </summary>
        /// <param name="id">Código</param>
        /// <returns>Pode excluir</returns>
        bool CanDelete(int id);

        /// <summary>
        ///     Verifica se o nome do perfil existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="name">Nome</param>
        /// <returns>Nome do perfil existe ou não</returns>
        bool ExistName(int id, string name);

        /// <summary>
        ///     Atualiza <see cref="UserProfile" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="UserProfile" />
        /// </param>
        new void Update(UserProfile entity);
   }
}