using System.ComponentModel.DataAnnotations;

namespace $rootnamespace$.Models.Views.Login
{
    public class Index
    {
        [Required]
        [Display(Name = "Login ou e-mail de cadastro")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}