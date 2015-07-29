using System.Collections.Generic;
using System.Linq;
using Template.Models;
using NHibernate.Linq;

namespace Template.Repository
{
    public class ModuleRepository : Thunder.Data.Pattern.Repository<Module, int>, IModuleRepository
    {
        public IList<Module> Parents()
        {
            using (var transaction = Session.BeginTransaction())
            {
                var modules = Session.QueryOver<Module>()
                                    .Where(x => x.Parent == null)
                                    .OrderBy(x => x.Order).Asc
                                    .List<Module>();

                transaction.Commit();

                return modules;
            }
        }

        public IList<Module> Find(User user)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var parents = Session.Query<Module>().Where(x => x.Parent == null).OrderBy(x => x.Order);
                var modules = new List<Module>();
                var userDb = Session.Get<User>(user.Id);

                foreach (var module in parents)
                {
                    modules.AddRange(Find(userDb, module));
                }

                transaction.Commit();

                return modules;
            }
        }

        private static IEnumerable<Module> Find(User user, Module parent)
        {
            var modules = new List<Module>();

            if (user.Profile.HasAccess(parent))
            {
                var module = new Module
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    Order = parent.Order,
                    Description = parent.Description,
                    CssClass = parent.CssClass,
                    Created = parent.Created,
                    Updated = parent.Updated,
                    Functionalities = parent.Functionalities
                };

                foreach (var item in parent.Childs.SelectMany(child => Find(user, child)))
                {
                    module.Childs.Add(item);
                }

                modules.Add(module);
            }

            return modules;
        }
    }
}