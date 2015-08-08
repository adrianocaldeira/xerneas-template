using System.Collections.Generic;
using System.Web.Mvc;
using Template.Models.Filters;

namespace Template.Models.Views.Users
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