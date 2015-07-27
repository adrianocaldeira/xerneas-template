using System;

namespace Template
{
    [Serializable]
    public class XerneasUserDataFunctionality
    {
        /// <summary>
        ///     Recupera ou define nome
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Recupera ou define ação
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///     Recupera ou define controlador
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        ///     Recupera ou define método http
        /// </summary>
        public string HttpMethod { get; set; }

    }
}