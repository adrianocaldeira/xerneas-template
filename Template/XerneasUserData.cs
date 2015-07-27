using System;
using System.Collections.Generic;
using System.Linq;
using Template.Models;

namespace Template
{
    [Serializable]
    public class XerneasUserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public IList<XerneasUserDataFunctionality> Functionalities { get; set; }

        public static XerneasUserData Create(User user)
        {
            return new XerneasUserData
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id,
                Login = user.Login,
                Functionalities = user.Profile.Functionalities.Select(x=> new XerneasUserDataFunctionality
                {
                    Name = x.Name,
                    Action = x.Action,
                    Controller = x.Controller,
                    HttpMethod = x.HttpMethod
                }).ToList()
            };
        }
    }
}