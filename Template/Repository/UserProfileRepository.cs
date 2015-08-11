using System;
using Template.Models;
using Template.Models.Filters;
using Thunder.Collections;
using Thunder.Data.Pattern;

namespace Template.Repository
{
    public class UserProfileRepository : Repository<UserProfile, int>, IUserProfileRepository
    {
        /// <summary>
        ///     Lista <see cref="UserProfile" /> paginado
        /// </summary>
        /// <param name="filter">
        ///     <see cref="UserProfileFilter" />
        /// </param>
        /// <returns>Lista de <see cref="UserProfile" /></returns>
        public IPaging<UserProfile> Page(UserProfileFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Verifica se o perfil pode ser excluído
        /// </summary>
        /// <param name="id">Código</param>
        /// <returns>Pode excluir</returns>
        public bool CanDelete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Verifica se o nome do perfil existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="name">Nome</param>
        /// <returns>Nome do perfil existe ou não</returns>
        public bool ExistName(int id, string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Atualiza <see cref="UserProfile" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="UserProfile" />
        /// </param>
        public new void Update(UserProfile entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Cria <see cref="UserProfile" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="UserProfile" />
        /// </param>
        public new void Create(UserProfile entity)
        {
            throw new NotImplementedException();
        }
    }
}