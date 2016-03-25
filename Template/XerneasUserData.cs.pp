using System;
using $rootnamespace$.Models;

namespace $rootnamespace$
{
    [Serializable]
    public class XerneasUserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }

        public static XerneasUserData Create(User user)
        {
            return new XerneasUserData
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Name = user.Name
            };
        }
    }
}