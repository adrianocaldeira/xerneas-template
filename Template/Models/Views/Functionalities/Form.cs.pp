using System.Collections.Generic;
using System.Web.Mvc;

namespace $rootnamespace$.Models.Views.Functionalities
{
    public class Form
    {
        public Form()
        {
            HttpMethods = new List<SelectListItem>
            {
                new SelectListItem {Text = "GET", Value = "GET"},
                new SelectListItem {Text = "POST", Value = "POST"},
                new SelectListItem {Text = "PUT", Value = "PUT"},
                new SelectListItem {Text = "DELETE", Value = "DELETE"},
                new SelectListItem {Text = "HEAD", Value = "HEAD"}
            };
            SelectedHttpMethod = new List<string>();
        }
        public IList<string> SelectedHttpMethod { get; set; }
        public Functionality Functionality { get; set; }
        public IList<SelectListItem> HttpMethods { get; set; }
        public int Index { get; set; }
    }
}