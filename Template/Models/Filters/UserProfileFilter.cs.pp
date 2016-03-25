using System.ComponentModel.DataAnnotations;
using Thunder.Model;

namespace $rootnamespace$.Models.Filters
{
    /// <summary>
    ///     Filtro de <see cref="UserProfile" />
    /// </summary>
    public class UserProfileFilter : Filter
    {
        /// <summary>
        ///     Tamanho padrão da paginação
        /// </summary>
        public const int DefaultPageSize = 15;

        /// <summary>
        ///     Inicializa uma nova instância da classe <see cref="UserProfileFilter" />
        /// </summary>
        public UserProfileFilter()
        {
            PageSize = DefaultPageSize;
        }

        /// <summary>
        ///     Recupera ou define nome
        /// </summary>
        [Display(Name = "Nome")]
        public string Name { get; set; }

        /// <summary>
        ///     Recupera ou define ativo
        /// </summary>
        [Display(Name = "Status")]
        public bool? Active { get; set; }

        /// <summary>
        ///     Por nome
        /// </summary>
        public bool ByName
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        /// <summary>
        ///     Por ativo
        /// </summary>
        public bool ByActive
        {
            get { return Active.HasValue; }
        }
    }
}