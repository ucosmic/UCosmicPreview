using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace UCosmic.Www.Mvc.WebDriver
{
    public static class WebElementExtensions
    {
        #region Clicking Links & Buttons

        public static void ClickSubmitButton(this IWebElement submitButton)
        {
            if (submitButton == null)
                throw new ArgumentNullException("submitButton");

            bool exceptionCaught;
            do
            {
                exceptionCaught = false;
                try
                {
                    submitButton.Submit();
                }
                catch (WebDriverException)
                {
                    exceptionCaught = true;
                }
            } while (exceptionCaught);
        }

        public static void ClickLink(this IWebElement link)
        {
            if (link == null)
                throw new ArgumentNullException("link");

            bool exceptionCaught;
            do
            {
                exceptionCaught = false;
                try
                {
                    // sometimes, clicking a button or hyperlink does not work in IE / Chrome
                    // solution is to click only for FF, and use enter key for IE / Chrome
                    if (link.Browser().IsChrome())
                    {
                        link.Click();
                    }
                    else
                    {
                        link.SendKeys(Keys.Enter);
                    }
                }
                catch (WebDriverException)
                {
                    exceptionCaught = true;
                }
            } while (exceptionCaught);
        }

        public static void ClickButton(this IWebElement button)
        {
            if (button == null)
                throw new ArgumentNullException("button");

            bool exceptionCaught;
            do
            {
                exceptionCaught = false;
                try
                {
                    // sometimes, clicking a button or hyperlink does not work in IE / Chrome
                    // solution is to click only for FF, and use enter key for IE / Chrome
                    if (!button.Browser().IsInternetExplorer())
                    {
                        200.WaitThisManyMilleseconds();
                        button.Click();
                    }
                    else
                    {
                        button.SendKeys(Keys.Enter);
                    }
                }
                catch (WebDriverException)
                {
                    exceptionCaught = true;
                }
            } while (exceptionCaught);
        }

        #endregion
        #region Clicking Radios & Checkboxes

        public static void ClickRadioButton(this IWebElement radioButton)
        {
            if (radioButton == null)
                throw new ArgumentNullException("radioButton");

            var elementId = radioButton.GetId();
            var jQuery = string.Format("return $('#{0}').is(':checked');", elementId);
            while (!(bool)radioButton.Browser().ExecuteScript(jQuery))
            {
                radioButton.ClickRadioOrCheckBox();
            }
        }

        // ReSharper disable UnusedMember.Global
        public static void CheckCheckBox(this IWebElement checkBox)
        {
            checkBox.CheckOrUncheckCheckBox(true);
        }

        public static void UncheckCheckBox(this IWebElement checkBox)
        {
            checkBox.CheckOrUncheckCheckBox(false);
        }
        // ReSharper restore UnusedMember.Global

        public static void CheckOrUncheckCheckBox(this IWebElement checkBox, bool shouldCheck)
        {
            if (checkBox == null)
                throw new ArgumentNullException("checkBox");

            // get the id of the checkbox & browser
            var browser = checkBox.Browser();
            var elementId = checkBox.GetId();

            // make sure checkbox is visible
            browser.WaitUntil(b => checkBox.Displayed, string.Format(
                "The checkbox with id '{0}' was not displayed using @Browser.", elementId));

            // generate script to find out whether or not the checkbox is checked
            var jQuery = string.Format("return $('#{0}').is(':checked')", elementId);

            // find out whether the box is checked or not
            var isChecked = (bool)browser.ExecuteScript(jQuery);
            if (shouldCheck == isChecked) return; // if the box is in the expected state, this step is over
            while (shouldCheck != (bool)browser.ExecuteScript(jQuery))
            {
                checkBox.ClickRadioOrCheckBox();
            }
        }

        private static void ClickRadioOrCheckBox(this IWebElement webElement)
        {
            if (webElement == null)
                throw new ArgumentNullException("webElement");

            bool exceptionCaught;
            do
            {
                exceptionCaught = false;
                try
                {
                    webElement.Click();
                }
                catch (WebDriverException)
                {
                    exceptionCaught = true;
                }

            } while (exceptionCaught);
        }

        #endregion
        #region Clicking Autocomplete Boxes

        public static void ClickAutoCompleteItem(this IWebElement autoCompleteItem)
        {
            if (autoCompleteItem == null)
                throw new ArgumentNullException("autoCompleteItem");

            while (autoCompleteItem.Displayed)
            {
                bool exceptionCaught;
                do
                {
                    exceptionCaught = false;
                    try
                    {
                        // IE requires multiple clicks before 
                        // autocomplete dropdown item disappears
                        autoCompleteItem.Click();
                    }
                    catch (WebDriverException)
                    {
                        exceptionCaught = true;
                    }
                } while (exceptionCaught);
            }
        }

        #endregion
        #region Typing into Text & File Boxes

        public static void ClearAndSendKeys(this IWebElement webElement, string textToType, string jQueryPrefix = "")
        {
            var elementId = webElement.GetId();
            var jQuery = string.Format("return $('{0}#{1}').val();", jQueryPrefix, elementId);

            while (webElement.Browser().ExecuteScript(jQuery).ToString() != textToType)
            {
                bool exceptionCaught;
                do
                {
                    exceptionCaught = false;
                    try
                    {
                        webElement.Clear();
                        webElement.SendKeys(textToType);
                    }
                    catch (WebDriverException)
                    {
                        exceptionCaught = true;
                    }
                } while (exceptionCaught);
            }
        }

        public static void TypeText(this IWebElement webElement, string textToType)
        {
            bool exceptionCaught;
            do
            {
                exceptionCaught = false;
                try
                {
                    webElement.Clear();

                    // hack to simulate actual user speed,
                    // type all but last character:
                    if (textToType.Length > 1)
                    {
                        webElement.SendKeys(textToType.Substring(0, textToType.Length - 1));
                        200.WaitThisManyMilleseconds();
                        webElement.SendKeys(textToType.Substring(textToType.Length - 1));
                    }
                    else
                        webElement.SendKeys(textToType);
                }
                catch (WebDriverException)
                {
                    exceptionCaught = true;
                }
            } while (exceptionCaught);

            // force IE keydown and change events
            if (webElement.Browser().IsInternetExplorer())
            {
                var elementId = webElement.GetId();
                webElement.Browser().ExecuteScript(string.Format(
                    "$('#{0}').keydown();$('#{0}').change();", elementId));
            }
        }

        public static void ChooseFile(this IWebElement webElement, string filePath)
        {
            bool exceptionCaught;
            do
            {
                exceptionCaught = false;
                try
                {
                    //if (!webElement.Browser().IsInternetExplorer())
                    //    webElement.Clear();
                    webElement.SendKeys(filePath);
                }
                catch (WebDriverException)
                {
                    exceptionCaught = true;
                }
            } while (exceptionCaught);

            if (!webElement.Browser().IsInternetExplorer()) return;
            try
            {
                // force file upload change/blur in IE
                var jQuery = string.Format("$('#{0}').blur();$('#{0}').change();", webElement.GetId());
                webElement.Browser().ExecuteScript(jQuery);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            { //swallow all exceptions here 
            }
        }

        #endregion
        #region Private Methods

        private static IWebDriver Browser(this IWebElement element)
        {
            return ((RemoteWebElement)element).WrappedDriver;
        }

        private static string GetId(this IWebElement webElement)
        {
            return webElement.GetAttribute("id");
        }

        #endregion
    }
}
