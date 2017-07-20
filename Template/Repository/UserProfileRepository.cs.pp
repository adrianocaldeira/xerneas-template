using System.Linq;
using NHibernate.Linq;
using $rootnamespace$.Models;
using $rootnamespace$.Models.Filters;
using Thunder.Collections;
using Thunder.Collections.Extensions;
using Thunder.NHibernate.Pattern;

namespace $rootnamespace$.Repository
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
            using (var transaction = Session.BeginTransaction())
            {
                var query = Session.Query<UserProfile>();

                if (filter.ByActive) query = query.Where(x => x.Active == filter.Active.Value);
                if (filter.ByName) query = query.Where(x => x.Name.ToLower().Contains(filter.Name.Trim().ToLower()));

                var userProfiles = query.OrderBy(x => x.Name).Paging(filter.CurrentPage, filter.PageSize);

                transaction.Commit();

                return userProfiles;
            }
        }

        /// <summary>
        ///     Verifica se o perfil pode ser excluído
        /// </summary>
        /// <param name="id">Código</param>
        /// <returns>Pode excluir</returns>
        public bool CanDelete(int id)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var canDelete = Session.Query<User>().Count(x=>x.Profile.Id == id) == 0;

                transaction.Commit();

                return canDelete;
            }
        }

        /// <summary>
        ///     Verifica se o nome do perfil existe
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="name">Nome</param>
        /// <returns>Nome do perfil existe ou não</returns>
        public bool ExistName(int id, string name)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var exist = Session.Query<UserProfile>().Count(x => x.Id != id &&
                        x.Name.ToLower() == name.ToLower().Trim()) > 0;

                transaction.Commit();

                return exist;
            }
        }

        /// <summary>
        ///     Atualiza <see cref="UserProfile" />
        /// </summary>
        /// <param name="entity">
        ///     <see cref="UserProfile" />
        /// </param>
        public new void Update(UserProfile entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var userProfile = Session.Get<UserProfile>(entity.Id);

                userProfile.Active = entity.Active;
                userProfile.Functionalities = entity.Functionalities;
                userProfile.Name = entity.Name;
                
                Session.Update(userProfile);

                transaction.Commit();
            }
        }
    }
}