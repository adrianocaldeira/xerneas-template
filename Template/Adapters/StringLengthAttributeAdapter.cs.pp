using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using $rootnamespace$.Properties;

namespace $rootnamespace$.Adapters
{
    public class StringLengthAttributeAdapter : System.Web.Mvc.StringLengthAttributeAdapter
    {
        public StringLengthAttributeAdapter(
            ModelMetadata metadata,
            ControllerContext context,
            StringLengthAttribute attribute)
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
                    if (Attribute.MinimumLength == 0)
                    {
                        Attribute.ErrorMessageResourceName = "PropertyMaxStringLength";
                    }
                    else
                    {
                        Attribute.ErrorMessageResourceName = "PropertyMinMaxStringLength";
                    }
                }
            }
        }
    }
}