using System.ComponentModel.DataAnnotations;
using Thunder.Data;

namespace $rootnamespace$.Models
{
    /// <summary>
    ///     Funcionalida de um módulo do sistema
    /// </summary>
    public class Functionality : Persist<Functionality, int>
    {
        /// <summary>
        ///     Recupera ou define nome
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        ///     Recupera ou define nome
        /// </summary>
        [Required]
        [Display(Name = "Nome")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Recupera ou define descrição
        /// </summary>
        [Display(Name = "Descrição")]
        public virtual string Description { get; set; }

        /// <summary>
        ///     Recupera ou define ação
        /// </summary>
        [Required]
        [Display(Name = "Nome da Action")]
        public virtual string Action { get; set; }

        /// <summary>
        ///     Recupera ou define controlador
        /// </summary>
        [Required]
        [Display(Name = "Nome da Controller")]
        public virtual string Controller { get; set; }

        /// <summary>
        ///     Recupera ou define método http
        /// </summary>
        [Required]
        [Display(Name = "Método HTTP")]
        public virtual string HttpMethod { get; set; }

        /// <summary>
        ///     Recupera ou define se a funcionalidade é a padrão do módulo
        /// </summary>
        [Display(Name = "Principal")]
        public virtual bool Default { get; set; }
    }
}