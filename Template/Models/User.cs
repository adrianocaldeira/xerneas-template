using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Web.Mvc;
using Template.Repository;
using Thunder.ComponentModel.DataAnnotations;
using Thunder.Data.Pattern;
using Thunder.Extensions;
using Thunder.Security;

namespace Template.Models
{
    /// <summary>
    ///     Usuário
    /// </summary>
    public class User : Persist<User, int>{

        /// <summary>
        ///     Recupera ou define perfil
        /// </summary>
        public virtual UserProfile Profile { get; set; }

        /// <summary>
        ///     Recupera ou define nome
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Recupera ou define login
        /// </summary>
        [Required]
        [StringLength(20)]
        public virtual string Login { get; set; }

        /// <summary>
        ///     Recupera ou define e-mail
        /// </summary>
        [Required]
        [Email(ErrorMessage = "E-mail informado é inválido.")]
        [StringLength(100)]
        [Display(Name = "E-mail")]
        public virtual string Email { get; set; }

        /// <summary>
        ///     Recupera ou define senha
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "Senha")]
        public virtual string Password { get; set; }

        /// <summary>
        ///     Recupera ou define status
        /// </summary>
        [Display(Name = "Ativo")]
        public virtual bool Active { get; set; }

        /// <summary>
        /// Recupera ou define salt
        /// </summary>
        public virtual string Salt { get; set; }

        /// <summary>
        ///     Recupera ou define última data de acesso
        /// </summary>
        public virtual DateTime? LastAccess { get; set; }

        /// <summary>
        ///     Encripta senha
        /// </summary>
        /// <param name="salt">Salt</param>
        /// <param name="password">Senha</param>
        /// <returns>Senha criptografada</returns>
        public static string EncriptPassword(string salt, string password)
        {
            return password.Encrypt(salt);
        }

        /// <summary>
        ///     Recupera senha descriptografada
        /// </summary>
        public virtual string GetPlanPassword()
        {
            return string.IsNullOrEmpty(Password) ? string.Empty : Password.Decrypt(Salt);
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{0} - {1}".With(Id, Name);
        }

        /// <summary>
        ///     Valida <see cref="User" />
        /// </summary>
        /// <param name="modelState">
        ///     <see cref="ModelStateDictionary" />
        /// </param>
        /// <param name="repository">
        ///     <see cref="IUserRepository" />
        /// </param>
        /// <returns>Válido</returns>
        public virtual bool IsValid(ModelStateDictionary modelState, IUserRepository repository)
        {
            if (modelState.IsValid)
            {
                if (repository.ExistLogin(Id, Login))
                {
                    modelState.AddModelError("User.Login", "O login informado já existe.");
                }

                if (repository.ExistEmail(Id, Email))
                {
                    modelState.AddModelError("User.Email", "O e-mail informado já existe.");
                }
            }
            else
            {
                if (!IsNew() && modelState.ContainsKey("Password"))
                {
                    modelState.Remove("Password");
                }
            }

            return modelState.IsValid;
        }
    }
}