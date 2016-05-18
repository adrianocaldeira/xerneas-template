using System.Collections.Generic;
using Newtonsoft.Json;

namespace $rootnamespace$.Models.Views.Modules
{
    public class TreeView
    {
        public int Id { get; set; }

        public IList<TreeView> Children { get; set; }

        public static IList<TreeView> Create(string json)
        {
            return JsonConvert.DeserializeObject<List<TreeView>>(json);
        }
    }
}