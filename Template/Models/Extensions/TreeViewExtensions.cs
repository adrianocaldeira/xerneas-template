using System.Collections.Generic;
using System.Linq;
using Template.Models.Views.Modules;

namespace Template.Models.Extensions
{
    public static class TreeViewExtensions
    {
        public static IList<Module> ToModules(this IList<TreeView> source)
        {
            var modules = new List<Module>();
            var order = 0;
            foreach (var item in source)
            {
                var module = new Module
                {
                    Id = item.Id,
                    Order = order
                };

                ToModules(module, item.Children);

                modules.Add(module);

                order++;
            }

            return modules;
        }

        private static void ToModules(Module module, IList<TreeView> treeViews)
        {
            if(treeViews == null || !treeViews.Any()) return;
            
            var order = 0;
            foreach (var item in treeViews)
            {
                var child = new Module
                {
                    Id = item.Id,
                    Parent = module,
                    Order = order
                };

                if (item.Children != null && item.Children.Any())
                {
                    ToModules(child, item.Children);    
                }

                module.Childs.Add(child);

                order++;
            }
        }
    }
}