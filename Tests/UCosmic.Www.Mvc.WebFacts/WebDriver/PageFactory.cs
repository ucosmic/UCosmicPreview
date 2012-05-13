using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc
{
    public static class PageFactory
    {
        private static readonly IDictionary<string, Page> Registry = new Dictionary<string, Page>();

        public static Page GetPage(this IWebDriver browser)
        {
            return GetCurrentPage(browser);
        }

        private static Page GetCurrentPage(IWebDriver browser)
        {
            if (browser.IsNull()) throw new ArgumentNullException("browser");
            InitializeRegistry();

            // try to match page with path variables
            var entry = Registry.SingleOrDefault(r => browser.Url.MatchesParameterizedUrl(r.Value.AbsoluteUrl));
            if (entry.Value.IsNotNull()) return entry.Value.CreateNewInstance(browser);

            // try to find a page whose URL matches exactly (with and without query string)
            entry = Registry.SingleOrDefault(r => browser.Url.MatchesUrl(r.Value.AbsoluteUrl));
            if (entry.Value.IsNotNull()) return entry.Value.CreateNewInstance(browser);

            return null;
        }

        public static Page GetPage(this string title)
        {
            return GetTitledPage(title);
        }

        private static Page GetTitledPage(string title)
        {
            if (title.IsNull()) throw new ArgumentNullException("title");
            InitializeRegistry();
            return Registry.ContainsKey(title)
                ? Registry[title].CreateNewInstance() : null;
        }

        private static void InitializeRegistry()
        {
            if (Registry.Any()) return;

            var basePageType = typeof(Page);
            var pageTypes = basePageType.Assembly.GetTypes()
                .Where(t => t != basePageType && basePageType.IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();
            if (!pageTypes.Any()) throw new InvalidOperationException(
                "Expected at least 1 concrete implementation of '{0}'."
                    .FormatWith(basePageType.Name));

            var pages = pageTypes.Select(t => Activator.CreateInstance(t, null as IWebDriver))
                .OfType<Page>().ToArray();
            foreach (var page in pages) Registry.Add(page.Title, page);
        }

        private static Page CreateNewInstance(this Page page, IWebDriver browser = null)
        {
            return (Page)Activator.CreateInstance(page.GetType(), browser);
        }

        public static string AutoCompleteMenuKey(this string fieldKey)
        {
            return "{0}[AutoCompleteMenu]".FormatWith(fieldKey);
        }

        public static string ComboBoxDownArrowKey(this string fieldKey)
        {
            return "{0}[ComboBoxDownArrow]".FormatWith(fieldKey);
        }

        public static string ErrorKey(this string fieldKey)
        {
            return "{0}[Error]".FormatWith(fieldKey);
        }

        public static string ErrorTextKey(this string fieldKey, string errorType)
        {
            return "{0}[ErrorText='{1}']".FormatWith(fieldKey, errorType);
        }

        private const string CollectionItemKeyFormat = "{0}[Collection='{1}']";

        public static string CollectionItemKey(this string fieldKey, string itemDetail)
        {
            return CollectionItemKeyFormat.FormatWith(fieldKey, itemDetail);
        }

        public static string CollectionItemTextKey(this string fieldKey)
        {
            return CollectionItemKeyFormat.FormatWith(fieldKey, Content.CollectionItemTextToken);
        }

        public static string CollectionItemRemoveKey(this string fieldKey, string removeDetail)
        {
            var removeItemKey = Content.CollectionItemRemoveFormat.FormatWith(removeDetail);
            return CollectionItemKeyFormat.FormatWith(fieldKey, removeItemKey);
        }

        public static string CollectionItemClickKey(this string fieldKey, string clickDetail)
        {
            var clickItemKey = Content.CollectionItemClickFormat.FormatWith(clickDetail);
            return CollectionItemKeyFormat.FormatWith(fieldKey, clickItemKey);
        }

        public static string RadioKey(this string fieldKey, string radioLabel)
        {
            return "{0}[Radio='{1}']".FormatWith(fieldKey, radioLabel);
        }
    }
}