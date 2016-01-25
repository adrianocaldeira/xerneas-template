using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Template.Helpers
{
    public static class HtmlHelperExtensions
    {
        #region StatusFor
        public static MvcHtmlString StatusFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string optionLabel)
        {
            return htmlHelper.StatusFor(expression, optionLabel, null);
        }

        public static MvcHtmlString StatusFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.StatusFor(expression, optionLabel, null, htmlAttributes);
        }

        public static MvcHtmlString StatusFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string optionLabel, bool? selectedValue)
        {
            return htmlHelper.StatusFor(expression, optionLabel, selectedValue, null);
        }

        public static MvcHtmlString StatusFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string optionLabel, bool? selectedValue,
            object htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.StatusFor(metadata, name, optionLabel, selectedValue,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        private static MvcHtmlString StatusFor(this HtmlHelper htmlHelper,
            ModelMetadata metadata, string name, string optionLabel,
            bool? selectedValue, IDictionary<string, object> htmlAttributes)
        {
            var fieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var fieldId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(name);

            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentException("name");
            }

            var dropdown = new TagBuilder("select");

            dropdown.Attributes.Add("name", fieldName);
            dropdown.Attributes.Add("id", fieldId);
            dropdown.MergeAttributes(htmlAttributes);
            dropdown.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            var options = new StringBuilder();

            if (optionLabel != null)
                options = options.Append("<option value=\"\">" + optionLabel + "</option>");

            options.Append("<option value=\"true\"" +
                           (selectedValue.HasValue && selectedValue.Value ? " selected=\"selected\" " : "") +
                           ">Ativo</option>")
                .Append("<option value=\"false\"" +
                        (selectedValue.HasValue && !selectedValue.Value ? " selected=\"selected\" " : "") +
                        ">Inativo</option>");

            dropdown.InnerHtml = options.ToString();

            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
        #endregion

        #region Active

        public static MvcHtmlString ActiveFor<T>(this HtmlHelper<T> htmlHelper, Expression<Func<T, bool>> expression, object htmlAttributes = null)
        {
            var attributes = HtmlAttributesUtility.ObjectToHtmlAttributesDictionary(htmlAttributes);

            attributes["class"] = "make-switch";
            attributes["data-on-color"] = "primary";
            attributes["data-on-text"] = "Ativo";
            attributes["data-off-color"] = "danger";
            attributes["data-off-text"] = "Inativo";

            return htmlHelper.CheckBoxFor(expression, attributes);
        }
        #endregion
    }
}