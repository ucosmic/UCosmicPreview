using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.Common.WebPages
{
    public abstract class WebPageBase
    {
        protected abstract string EditorSelector { get; }
        protected abstract Dictionary<string, string> SpecToWeb { get; }

        protected virtual string EmailExcerptStart { get { return null; } }
        protected virtual string EmailExcerptEnd { get { return null; } }

        private IWebDriver Browser { get; set; }

        protected WebPageBase(IWebDriver driver)
        {
            Browser = driver;
        }

        public IWebElement GetTextBox(string fieldLabel)
        {
            if (SpecToWeb.ContainsKey(fieldLabel))
            {
                var fieldName = SpecToWeb[fieldLabel];
                var selector = string.Format(@"{0} input[name=""{1}""]", EditorSelector, fieldName);
                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
                    "A field '{0}' (input element named '{1}') should exist using @Browser.", 
                        fieldLabel, fieldName));
            }

            throw new NotImplementedException(
                string.Format("The field label '{0}' was not expected.", fieldLabel));
        }

        public IWebElement GetErrorMessage(string fieldLabel, bool allowNull = false)
        {
            if (SpecToWeb.ContainsKey(fieldLabel))
            {
                var fieldName = SpecToWeb[fieldLabel];
                var selector = string.Format(@"{0} .field-validation-error[data-valmsg-for=""{1}""]", EditorSelector, fieldName);
                if (!allowNull)
                    return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
                        "An error message for the '{0}' field (element named '{1}') should exist using @Browser.", 
                            fieldLabel, fieldName));

                return Browser.TryFindElement(By.CssSelector(selector));
            }

            throw new NotImplementedException(
                string.Format("The field label '{0}' was not expected.", fieldLabel));
        }

        public IWebElement GetButton(string buttonLabel)
        {
            if (SpecToWeb.ContainsKey(buttonLabel))
            {
                var buttonType = SpecToWeb[buttonLabel];
                var selector = string.Format(@"{0} input[value=""{1}""][type=""{2}""]", EditorSelector, buttonLabel, buttonType);
                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
                    "An input {1} element with value '{0}' should exist using @Browser.", buttonLabel, buttonType));
            }

            throw new NotImplementedException(
                string.Format("The button label '{0}' was not expected.", buttonLabel));
        }

        public IWebElement GetCustomContent(string content, bool allowNull = false)
        {
            if (SpecToWeb.ContainsKey(content))
            {
                var contentSelector = SpecToWeb[content];
                var selector = string.Format(@"{0} {1}", EditorSelector, contentSelector);
                if (!allowNull)
                    return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
                        "Custom content '{0}' (with CSS selector '{1}') should exist using @Browser.", 
                            content, selector));

                return Browser.TryFindElement(By.CssSelector(selector));
            }

            throw new NotImplementedException(
                string.Format("Custom content '{0}' was not expected.", content));
        }

        public string GetEmailExcerpt(string message)
        {
            if (EmailExcerptStart != null && EmailExcerptEnd != null)
            {
                var secretCodeStart = message.IndexOf(EmailExcerptStart, System.StringComparison.Ordinal) + EmailExcerptStart.Length;
                var secretCodeEnd = message.Substring(secretCodeStart).IndexOf(EmailExcerptEnd, System.StringComparison.Ordinal);
                var secretCode = message.Substring(secretCodeStart, secretCodeEnd);
                return secretCode;
            }

            throw new NotImplementedException(
                string.Format("An email was not expected on the '{0}' page.", Browser.Url));
        }

    }
}
