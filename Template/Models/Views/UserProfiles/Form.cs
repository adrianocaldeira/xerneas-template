using System.Collections.Generic;

namespace Template.Models.Views.UserProfiles
{
    public class Form
    {
        public UserProfile UserProfile { get; set; }
        public IList<Module> Modules { get; set; }
    }
}