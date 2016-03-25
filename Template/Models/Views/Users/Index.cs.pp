using System.Collections.Generic;
using System.Web.Mvc;
using $rootnamespace$.Models.Filters;

namespace $rootnamespace$.Models.Views.Users
{
    public class Index
    {
        public Index()
        {
            Profiles= new List<SelectListItem>();
        }
        public UserFilter Filter { get; set; }
        public IList<SelectListItem> Profiles { get; set; } 
    }
}