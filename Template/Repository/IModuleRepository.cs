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

        /// <summary>
        ///     Organiza módulos
        /// </summary>
        /// <param name="modules"></param>
        void Organizer(IList<Module> modules);

        /// <summary>
        ///     Verifica se existe módulo pelo nome
        /// </summary>
        /// <param name="id">Código</param>
        /// <param name="parent">
        ///     <see cref="Module" />
        /// </param>
        /// <param name="name">Nome</param>
        /// <returns></returns>
        bool Exist(int id, Module parent, string name);

        /// <summary>
        ///     Cria <see cref="Module" />
        /// </summary>
        /// <param name="module"></param>
        new void Create(Module module);

        /// <summary>
        ///     Atualiza módulo
        /// </summary>
        /// <param name="module"></param>
        new void Update(Module module);

        /// <summary>
        ///     Possui funcionaliade com relacionamento com algum perfil de usuário
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        bool HasFunctionalitiesRelationshipUserProfiles(Module module);
    }
}