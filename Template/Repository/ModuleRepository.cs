using System.Collections.Generic;
using System.Linq;
using Template.Models;
using NHibernate.Linq;
using NHibernate.Util;

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