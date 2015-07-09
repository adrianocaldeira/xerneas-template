using System.Collections.Generic;
using Template.Models;
using Thunder.Data.Pattern;

namespace Template.Repository
{
    /// <summary>
    ///     Repositório de <see cref="System.Reflection.Module" />
    /// </summary>
    public interface IModuleRepository : IRepository<Module, int>
    {
        /// <summary>
        ///     Lista módulos pai
        /// </summary>
        /// <returns></returns>
        IList<Module> Parents();

        /// <summary>
        ///     Localiza os módulos que o usuário possui acesso
        /// </summary>
        /// <param name="user">
        ///     <see cref="Template.Models.User" />
        /// </param>
        /// <returns>Lista de <see cref="Module" /></returns>
        IList<Module> Find(User user);
    }
}