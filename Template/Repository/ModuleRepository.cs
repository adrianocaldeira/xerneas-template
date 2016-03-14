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

        /// <summary>
        ///     Verifica se existe módulo pelo nome
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="parent">
        ///     <see cref="Module" />
        /// </param>
        /// <param name="name">Nome</param>
        /// <returns></returns>
        public bool Exist(int id, Module parent, string name)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var queryable = Session.Query<Module>().Where(x => x.Id != id 
                    && x.Name.Trim().ToLower().Contains(name.Trim().ToLower()));

                if (parent == null || parent.Id == 0)
                {
                    queryable = queryable.Where(x => x.Parent == null);
                }
                else
                {
                    queryable = queryable.Where(x => x.Parent.Id == parent.Id);                    
                }

                var exist = queryable.Count() > 0;

                transaction.Commit();

                return exist;
            }
        }

        /// <summary>
        ///     Cria <see cref="Module" />
        /// </summary>
        /// <param name="module"></param>
        public new void Create(Module module)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var order = Session.Query<Module>().Where(x => x.Parent == null)
                    .OrderBy(x => x.Order)
                    .Select(x=>x.Order)
                    .ToList()
                    .LastOrDefault();

                if (order > 0) order++;

                module.Order = order;

                Session.Save(module);

                transaction.Commit();
            }
        }

        /// <summary>
        ///     Atualiza módulo
        /// </summary>
        /// <param name="module"></param>
        public new void Update(Module module)
        {
            using (var transaction = Session.BeginTransaction())
            {
                var moduleDb = Session.Get<Module>(module.Id);
                var functionalitiesForInsert = module.Functionalities.Where(x => x.Id.Equals(0));
                var functionalitiesForUpdate = module.Functionalities.Where(x => x.Id > 0);
                var functionalitiesForDelete = moduleDb.Functionalities.Where(functionality => !module.Functionalities.Contains(functionality)).ToList();
                
                moduleDb.CssClass = module.CssClass;
                moduleDb.Description = module.Description;
                moduleDb.Name = module.Name;

                foreach (var functionality in functionalitiesForInsert)
                {
                    functionality.Module = moduleDb;
                    moduleDb.Functionalities.Add(functionality);
                }
                
                foreach (var functionality in functionalitiesForUpdate)
                {
                    var functionalityDb = moduleDb.Functionalities.Single(x => x.Id == functionality.Id);

                    functionalityDb.Action = functionality.Action;
                    functionalityDb.Controller = functionality.Controller;
                    functionalityDb.Default = functionality.Default;
                    functionalityDb.Description = functionality.Description;
                    functionalityDb.HttpMethod = functionality.HttpMethod;
                    functionalityDb.Name = functionality.Name;
                }

                foreach (var functionality in functionalitiesForDelete)
                {
                    moduleDb.Functionalities.Remove(functionality);

                    Session.Delete(functionality);
                }

                Session.Update(moduleDb);

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