using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Template.Models;
using Thunder.Data.Pattern;

namespace Template.Repository
{
    public class ModuleRepository : Repository<Module, int>, IModuleRepository
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
                    Find(module, modules, userDb);
                }

                transaction.Commit();

                return modules;
            }
        }

        /// <summary>
        /// Organiza módulos
        /// </summary>
        /// <param name="modules"></param>
        public void Organizer(IList<Module> modules)
        {
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var module in modules)
                {
                    var moduleDb = Session.Get<Module>(module.Id);
                    
                    Organizer(moduleDb, module.Childs);
                    
                    moduleDb.Order = module.Order;
                }

                transaction.Commit();
            }
        }

        private void Organizer(Module moduleDb, IList<Module> modules)
        {
            if (modules.Any())
            {
                foreach (var child in modules)
                {
                    var moduleChildDb = Session.Get<Module>(child.Id);

                    moduleChildDb.Order = child.Order;

                    if (moduleChildDb.Parent != null && !moduleChildDb.Parent.Equals(moduleDb))
                    {
                        moduleChildDb.Parent.Childs.Remove(moduleChildDb);
                        moduleChildDb.Parent = moduleDb;

                        moduleDb.Childs.Add(moduleChildDb);
                    }
                    else if (moduleChildDb.Parent == null)
                    {
                        moduleChildDb.Parent = moduleDb;
                        moduleDb.Childs.Add(moduleChildDb);
                    }

                    Organizer(moduleChildDb, child.Childs);
                }
            }
            else
            {
                foreach (var child in moduleDb.Childs.ToList())
                {
                    child.Parent.Childs.Remove(child);
                    child.Parent = null;
                }
                ;   
            }
        }

        private static void Find(Module module, IList<Module> modules, User user)
        {
            if (module == null) return;
            if (!user.Profile.HasAccess(module)) return;

            var clone = new Module
            {
                Id = module.Id,
                Name = module.Name,
                Order = module.Order,
                Description = module.Description,
                CssClass = module.CssClass,
                Created = module.Created,
                Updated = module.Updated,
                Functionalities = module.Functionalities,
                Childs = new List<Module>()
            };

            foreach (var child in module.Childs)
            {
                Find(child, clone.Childs, user);
            }

            modules.Add(clone);
        }
    }
}