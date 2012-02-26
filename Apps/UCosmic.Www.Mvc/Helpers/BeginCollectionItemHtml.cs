using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc
{
    public static class BeginCollectionItemHtml
    {
        public static IDisposable BeginCollectionItem(this HtmlHelper html, string collectionName)
        {
            var idsToReuse = GetIdsToReuse(html.ViewContext.HttpContext, collectionName);
            var itemIndex = idsToReuse.Count > 0 ? idsToReuse.Dequeue() : Guid.NewGuid().ToString();

            // autocomplete="off" is needed to work around a very annoying Chrome behaviour whereby it reuses 
            // old values after the user clicks "Back", which causes the xyz.index and xyz[...] values to get out of sync.
            html.ViewContext.Writer.WriteLine(string.Format(
                "<input type=\"hidden\" name=\"{0}.index\" autocomplete=\"off\" value=\"{1}\" />",
                    collectionName, html.Encode(itemIndex)));

            return BeginHtmlFieldPrefixScope(html, string.Format("{0}[{1}]", collectionName, itemIndex));
        }

        private static IDisposable BeginHtmlFieldPrefixScope(this HtmlHelper html, string htmlFieldPrefix)
        {
            return new HtmlFieldPrefixScope(html.ViewData.TemplateInfo, htmlFieldPrefix);
        }

        private static Queue<string> GetIdsToReuse(HttpContextBase httpContext, string collectionName)
        {
            // We need to use the same sequence of IDs following a server-side validation failure,  
            // otherwise the framework won't render the validation error messages next to each item.
            var key = "__htmlPrefixScopeExtensions_IdsToReuse_" + collectionName;
            var queue = (Queue<string>)httpContext.Items[key];
            if (queue == null)
            {
                httpContext.Items[key] = queue = new Queue<string>();
                var previouslyUsedIds = httpContext.Request[collectionName + ".index"];
                if (!string.IsNullOrEmpty(previouslyUsedIds))
                    foreach (var previouslyUsedId in previouslyUsedIds.Split(','))
                        queue.Enqueue(previouslyUsedId);
            }
            return queue;
        }

        private class HtmlFieldPrefixScope : IDisposable
        {
            private readonly TemplateInfo _templateInfo;
            private readonly string _previousHtmlFieldPrefix;

            public HtmlFieldPrefixScope(TemplateInfo templateInfo, string htmlFieldPrefix)
            {
                _templateInfo = templateInfo;

                _previousHtmlFieldPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = htmlFieldPrefix;
            }

            public void Dispose()
            {
                _templateInfo.HtmlFieldPrefix = _previousHtmlFieldPrefix;
            }
        }
    }
}