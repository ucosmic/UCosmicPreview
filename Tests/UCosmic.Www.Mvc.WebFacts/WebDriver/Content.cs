using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc
{
    public abstract class Content
    {
        public Content(IWebDriver browser)
        {
            browser = browser ?? new NullWebDriver();
            Browser = browser;
        }

        protected IWebDriver Browser { get; private set; }

        public abstract string Title { get; }

        public virtual IEnumerable<Content> NestedContents
        {
            get { return Enumerable.Empty<Page>(); }
        }

        public virtual IDictionary<string, string> Fields
        {
            get { return new Dictionary<string, string>(); }
        }

        public IWebElement GetField(string fieldLabel, bool allowNull = false)
        {
            if (!Fields.ContainsKey(fieldLabel))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(fieldLabel));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The field '{0}' is not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.GetField(fieldLabel, allowNull);
            }

            var cssSelector = Fields[fieldLabel];

            if (!allowNull)
                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    "The '{0}' field could not be found by @Browser (CSS selector was '{1}')."
                        .FormatWith(fieldLabel, cssSelector));

            return Browser.TryFindElement(By.CssSelector(cssSelector));
        }

        public string GetTextInputValue(string fieldLabel)
        {
            if (!Fields.ContainsKey(fieldLabel))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(fieldLabel));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The text input field '{0}' is not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.GetTextInputValue(fieldLabel);
            }

            var cssSelector = Fields[fieldLabel];

            var jQuery = string.Format("return $('{0}').val();", cssSelector);
            var value = Browser.ExecuteScript(jQuery);
            return value.IsNotNull() ? value.ToString() : null;
        }

        public bool IsChecked(string fieldLabel)
        {
            if (!Fields.ContainsKey(fieldLabel))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(fieldLabel));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The '{0}' check box is not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.IsChecked(fieldLabel);
            }

            var cssSelector = Fields[fieldLabel];

            var jQuery = string.Format("return $('{0}').is(':checked');", cssSelector);
            var value = Browser.ExecuteScript(jQuery);
            if (value == null)
                throw new InvalidOperationException(
                    "An error occurred while trying to get the value of the '{0}' check box in {1} (jQuery was {2})."
                        .FormatWith(fieldLabel, Browser.Name(), jQuery));
            return bool.Parse(value.ToString());
        }

        public IWebElement GetAutoCompleteMenu(string fieldLabel, bool allowNull = true)
        {
            var key = fieldLabel.AutoCompleteMenuKey();
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The menu '{0}' is not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.GetAutoCompleteMenu(fieldLabel, allowNull);
            }

            var cssSelector = Fields[key];

            if (!allowNull)
                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    "An autocomplete menu for the '{0}' field was not found by @Browser (CSS selector was '{1}')."
                        .FormatWith(fieldLabel, cssSelector));

            return Browser.TryFindElement(By.CssSelector(cssSelector));
        }

        public IWebElement GetComboBoxDownArrowButton(string fieldLabel, bool allowNull = false)
        {
            var key = fieldLabel.ComboBoxDownArrowKey();
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The '{0}' dropdown arrow button is not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.GetComboBoxDownArrowButton(fieldLabel, allowNull);
            }

            var cssSelector = Fields[key];

            if (!allowNull)
                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    "A dropdown arrow button for the '{0}' field was not found by @Browser (CSS selector was '{1}')."
                        .FormatWith(fieldLabel, cssSelector));

            return Browser.TryFindElement(By.CssSelector(cssSelector));
        }

        public IWebElement GetErrorMessage(string fieldLabel, bool allowNull = false)
        {
            var key = fieldLabel.ErrorKey();
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "No '{0}' errors are not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.GetErrorMessage(fieldLabel, allowNull);
            }

            var cssSelector = Fields[key];

            if (!allowNull)
                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    "An error message for the '{0}' field was not found by @Browser (CSS selector was '{1}')."
                        .FormatWith(fieldLabel, cssSelector));

            return Browser.TryFindElement(By.CssSelector(cssSelector));
        }

        public string GetErrorText(string fieldLabel, string errorType)
        {
            var key = fieldLabel.ErrorTextKey(errorType);
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "No '{0}' error messages are defined for the '{1}' field on this page."
                            .FormatWith(errorType, fieldLabel));
                return nested.GetErrorText(fieldLabel, errorType);
            }

            return Fields[key];
        }

        public const string ErrorSummaryKey = "[ErrorSummaries]";

        public IEnumerable<IWebElement> GetErrorSummaries(bool allowNull = true)
        {
            const string key = ErrorSummaryKey;
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "No error summaries are defined for this page.");
                return nested.GetErrorSummaries(allowNull);
            }

            var cssSelector = Fields[key];

            if (!allowNull)
                return Browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)),
                    "No error summaries were found by @Browser (CSS selector was '{0}')."
                        .FormatWith(cssSelector));

            return Browser.TryFindElements(By.CssSelector(cssSelector));
        }

        public const string CollectionItemToken = "Item";
        public const string CollectionItemTextToken = "ItemText";
        public const string CollectionItemRemoveFormat = "RemoveItem-{0}";
        public const string CollectionItemClickFormat = "ClickItem-{0}";

        public IEnumerable<IWebElement> GetCollectionItems(string fieldLabel, string itemDetail = CollectionItemTextToken)
        {
            var key = fieldLabel.CollectionItemKey(itemDetail);
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The '{0}' selector for the '{1}' collection is not defined for this page."
                            .FormatWith(itemDetail, fieldLabel));
                return nested.GetCollectionItems(fieldLabel, itemDetail);
            }

            var cssSelector = Fields[key];

            return Browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)),
                "No '{1}' items were found for the '{0}' collection by @Browser (CSS selector was '{2}')."
                    .FormatWith(fieldLabel, itemDetail, cssSelector));
        }

        public IWebElement GetCollectionItem(string fieldLabel, string itemDetail)
        {
            var key = fieldLabel.CollectionItemKey(itemDetail);
            if (!Fields.ContainsKey(key))
            {
                var nested = NestedContents.SingleOrDefault(n => n.Fields.ContainsKey(key));
                if (nested == null)
                    throw new InvalidOperationException(
                        "The '{0}' selector for the '{1}' collection is not defined for this page."
                            .FormatWith(fieldLabel));
                return nested.GetCollectionItem(fieldLabel, itemDetail);
            }

            var cssSelector = Fields[key];

            return Browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                "The '{0}' collection's '{1}' item was not found by @Browser (CSS selector was '{2}')."
                    .FormatWith(fieldLabel, itemDetail, cssSelector));
        }

        protected virtual string MailExcerptStart { get { return null; } }
        protected virtual string MailExcerptEnd { get { return null; } }

        public string GetMailExcerpt(string message)
        {
            if (MailExcerptStart != null && MailExcerptEnd != null)
            {
                var excerptStart = message.IndexOf(MailExcerptStart, StringComparison.Ordinal) + MailExcerptStart.Length;
                var excerptEnd = message.Substring(excerptStart).IndexOf(MailExcerptEnd, StringComparison.Ordinal);
                var excerpt = message.Substring(excerptStart, excerptEnd);
                return excerpt;
            }

            throw new NotSupportedException(
                "Mail was not expected on the '{0}' page (URL: {1}).".FormatWith(Title, Browser.Url));
        }
    }
}