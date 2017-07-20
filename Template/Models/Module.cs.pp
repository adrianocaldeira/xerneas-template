using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using $rootnamespace$.Repository;
using Thunder.Data;
using Thunder.Extensions;

namespace $rootnamespace$.Models
{
    /// <summary>
    ///     Módulos do sistema
    /// </summary>
    public class Module : Persist<Module, int>
    {
        private IList<Functionality> _allFunctionalities;

        /// <summary>
        ///     Inicializa uma nova instância da classe <see cref="Module" />.
        /// </summary>
        public Module()
        {
            Functionalities = new List<Functionality>();
            Childs = new List<Module>();
        }

        /// <summary>
        ///     Recupera ou define código
        /// </summary>
        [Display(Name = "Módulo Pai")]
        public new virtual int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        /// <summary>
        ///     Recupera ou define nome do módulo
        /// </summary>
        [Required]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Recupera ou define módulo pai
        /// </summary>
        public virtual Module Parent { get; set; }

        /// <summary>
        ///     Recupera ou define descrição do módulo
        /// </summary>
        [Display(Name = "Descrição")]
        [StringLength(100)]
        public virtual string Description { get; set; }

        /// <summary>
        ///     Recupera ou define css class
        /// </summary>
        [StringLength(50)]
        public virtual string CssClass { get; set; }

        /// <summary>
        ///     Recupera ou define ordem do módulo
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        ///     Recupera ou define funcionalidades do módulo
        /// </summary>
        public virtual IList<Functionality> Functionalities { get; set; }

        /// <summary>
        ///     Recupera ou define funcionalidade padrão do módulo
        /// </summary>
        public virtual Functionality DefaultFunctionality
        {
            get { return Functionalities.SingleOrDefault(x => x.Default); }
        }

        /// <summary>
        ///     Recupera ou define filhos do módulo
        /// </summary>
        public virtual IList<Module> Childs { get; set; }

        /// <summary>
        ///     Lista todas as funcionalidades do módulo e de seus filhos
        /// </summary>
        /// <param name="module">
        ///     <see cref="Module" />
        /// </param>
        /// <returns></returns>
        private static IList<Functionality> AllFunctionalities(Module module)
        {
            var functionalities = module.Functionalities.ToList();

            foreach (var child in module.Childs)
            {
                functionalities.AddRange(AllFunctionalities(child));
            }

            return functionalities;
        }

        /// <summary>
        ///     Lista todas as funcionalidades do módulo e de seus filhos
        /// </summary>
        /// <returns></returns>
        private IList<Functionality> AllFunctionalities()
        {
            return _allFunctionalities ?? (_allFunctionalities = AllFunctionalities(this));
        }

        /// <summary>
        ///     Módulo contém funcionalidade
        /// </summary>
        /// <param name="functionality">
        ///     <see cref="Functionality" />
        /// </param>
        /// <returns>Contém</returns>
        public virtual bool Contains(Functionality functionality)
        {
            return AllFunctionalities().Contains(functionality);
        }

        /// <summary>
        ///     Módulo contém funcionalidade
        /// </summary>
        /// <param name="controllerName">Nome da controller</param>
        /// <param name="actionName">Nome da action</param>
        /// <returns>Contém</returns>
        public virtual bool Contains(string controllerName, string actionName)
        {
            return AllFunctionalities().Any(functionality =>
                functionality.Controller.ToLower().Equals(controllerName.ToLower()) &&
                functionality.Action.ToLower().Equals(actionName.ToLower()));
        }

        /// <summary>
        ///     Valida <see cref="Module" />
        /// </summary>
        /// <param name="moduleRepository"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public virtual bool IsValid(IModuleRepository moduleRepository, ModelStateDictionary modelState)
        {
            modelState.Remove("Parent.Id");
            modelState.Remove("Parent.Name");

            if (IsNew() && Parent.Id <= 0)
            {
                Parent = null;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                if (moduleRepository.Exist(Id, Parent, Name))
                {
                    modelState.AddModelError("Name", "O nome do módulo \"{0}\" já encontra-se cadastrado.".With(Name));    
                }
            }

            if (Functionalities != null && Functionalities.Any())
            {
                if (Functionalities.Count(x => x.Default) == 0)
                {
                    modelState.AddModelError("Functionalities", "É necessário definir ao menos uma funcionalidade como sendo a principal do módulo.");
                }

                if (Functionalities.Count(x => x.Default) > 1)
                {
                    modelState.AddModelError("Functionalities", "É permitido definir somente uma funcionalidade como a principal do módulo.");
                }
            }
            return modelState.IsValid;
        }
    }
}