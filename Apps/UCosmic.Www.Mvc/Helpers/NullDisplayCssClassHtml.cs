using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc
{
    public static class NullDisplayCssClassHtml
    {
        public static MvcHtmlString NullDisplayCssClassFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string cssClass = "null-display")
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.NullDisplayText == metadata.SimpleDisplayText
                       ? new MvcHtmlString(cssClass)
                       : null;
        }
    }
}