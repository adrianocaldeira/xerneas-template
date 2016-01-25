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

        #region ButtonBack
        public static MvcHtmlString ButtonBack(this HtmlHelper htmlHelper, string url)
        {
            var anchor = new TagBuilder("a");
            var icon = new TagBuilder("i");

            anchor.Attributes["href"] = url;
            anchor.Attributes["title"] = "Voltar";

            icon.AddCssClass("fa fa-reply-all");

            anchor.InnerHtml += icon.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }
        #endregion 

        #region ButtonNew
        public static MvcHtmlString ButtonNew(this HtmlHelper htmlHelper, string url, object htmlAttributes = null)
        {
            var anchor = new TagBuilder("a");
            var icon = new TagBuilder("i");
            var attributes = HtmlAttributesUtility.ObjectToHtmlAttributesDictionary(htmlAttributes);

            anchor.MergeAttributes(attributes);
            anchor.Attributes["href"] = url;

            icon.AddCssClass("fa fa-plus");

            anchor.InnerHtml += icon.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region ButtonDelete
        public static MvcHtmlString ButtonDelete(this HtmlHelper htmlHelper, string url, object htmlAttributes = null)
        {
            var anchor = new TagBuilder("a");
            var icon = new TagBuilder("i");
            var attributes = HtmlAttributesUtility.ObjectToHtmlAttributesDictionary(htmlAttributes);

            anchor.MergeAttributes(attributes);
            anchor.AddCssClass("btn-delete-row");
            anchor.Attributes["href"] = url;
            anchor.Attributes["title"] = "Excluir";

            icon.AddCssClass("fa fa-trash-o");

            anchor.InnerHtml = icon.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }
        #endregion        

        #region ButtonEdit
        public static MvcHtmlString ButtonEdit(this HtmlHelper htmlHelper, string url, object htmlAttributes = null)
        {
            var anchor = new TagBuilder("a");
            var icon = new TagBuilder("i");
            var attributes = HtmlAttributesUtility.ObjectToHtmlAttributesDictionary(htmlAttributes);

            anchor.MergeAttributes(attributes);
            anchor.Attributes["href"] = url;
            anchor.Attributes["title"] = "Editar";

            icon.AddCssClass("fa fa-pencil");

            anchor.InnerHtml = icon.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchor.ToString(TagRenderMode.Normal));
        }
        #endregion  
    }
}