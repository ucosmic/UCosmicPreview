using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UCosmic.Www.Mvc
{
    public static class RadioButtonsHtml
    {
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (var name in names)
            {
                var id = string.Format(
                    "{0}_{1}_{2}",
                    htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                    metaData.PropertyName,
                    name
                );

                var radio = htmlHelper.RadioButtonFor(expression, name, new { id }).ToHtmlString();
                sb.AppendFormat(
                    "<label for=\"{0}\">{1}</label> {2}",
                    id,
                    HttpUtility.HtmlEncode(name),
                    radio
                );
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}