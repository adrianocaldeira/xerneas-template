﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace Template.Models.Views.Users
{
    public class Form
    {
        public IList<SelectListItem> Profiles { get; set; }
        public User User { get; set; }
    }
}