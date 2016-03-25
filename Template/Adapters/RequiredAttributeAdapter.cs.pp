using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using $rootnamespace$.Properties;

namespace $rootnamespace$.Adapters
{
    public class RequiredAttributeAdapter : System.Web.Mvc.RequiredAttributeAdapter
    {
        public RequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (string.IsNullOrWhiteSpace(Attribute.ErrorMessage))
            {
                if (Attribute.ErrorMessageResourceType == null)
                {
                    Attribute.ErrorMessageResourceType = typeof(Resources);
                }

                if (string.IsNullOrWhiteSpace(Attribute.ErrorMessageResourceName))
                {
                    Attribute.ErrorMessageResourceName = "PropertyValueRequired";
                }
            }
        }
    }
}