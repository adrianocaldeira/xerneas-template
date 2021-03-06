﻿using System.ComponentModel.DataAnnotations;
using Thunder.Model;

namespace $rootnamespace$.Models.Filters
{
    /// <summary>
    ///     Filtro de <see cref="User" />
    /// </summary>
    public class UserFilter : Filter
    {
        /// <summary>
        ///     Tamanho padrão da paginação
        /// </summary>
        public const int DefaultPageSize = 15;

        /// <summary>
        ///     Inicializa uma nova instância da classe <see cref="UserFilter" />
        /// </summary>
        public UserFilter()
        {
            PageSize = DefaultPageSize;
        }

        /// <summary>
        ///     Recupera ou define nome
        /// </summary>
        [Display(Name = "Nome")]
        public string Name { get; set; }

        /// <summary>
        ///     Recupera ou define <see cref="UserProfile" />
        /// </summary>
        [Display(Name = "Perfil")]
        public UserProfile Profile { get; set; }

        /// <summary>
        ///     Recupera ou defino ativo
        /// </summary>
        [Display(Name = "Status")]
        public bool? Active { get; set; }

        /// <summary>
        ///     Por nome
        /// </summary>
        public bool ByName
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        /// <summary>
        ///     Por perfil
        /// </summary>
        public bool ByProfile
        {
            get { return Profile != null && Profile.Id > 0; }
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