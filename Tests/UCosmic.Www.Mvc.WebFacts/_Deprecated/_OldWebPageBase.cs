//using System;
//using System.Collections.Generic;
//using OpenQA.Selenium;

//namespace UCosmic.Www.Mvc.Areas.Common.WebPages
//{
//    public abstract class WebPageBase
//    {
//        protected virtual string EditorSelector { get { return null; } }
//        protected abstract Dictionary<string, string> SpecToWeb { get; }

//        protected virtual string EmailExcerptStart { get { return null; } }
//        protected virtual string EmailExcerptEnd { get { return null; } }

//        private IWebDriver Browser { get; set; }

//        protected WebPageBase(IWebDriver driver)
//        {
//            Browser = driver;
//        }

//        //public IWebElement GetTextInputField(string label)
//        //{
//        //    if (SpecToWeb.ContainsKey(label))
//        //    {
//        //        var field = GetInputBySelector(label) ??
//        //                    GetInputByName(label) ??
//        //                    GetTextAreaByName(label) ??
//        //                    GetInputById(label) ??
//        //                    GetTextAreaById(label);
//        //        if (field != null) return field;
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", label));
//        //}

//        //public string GetTextInputValue(string label)
//        //{
//        //    var input = GetTextInputField(label);
//        //    if (input != null)
//        //    {
//        //        var id = input.GetAttribute("id");
//        //        var jQuery = string.Format("return $('#{0}').val();", id);
//        //        if (!string.IsNullOrWhiteSpace(id))
//        //            return Browser.WaitUntil(b => b.ExecuteScript(jQuery).ToString(),
//        //                string.Format("An unexpected error occurred while executing jQuery code '{0}' for input field '{1}' in @Browser.",
//        //                    jQuery, label));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", label));
//        //}

//        //private IWebElement GetInputBySelector(string fieldLabel)
//        //{
//        //    var webValue = SpecToWeb[fieldLabel];
//        //    if (webValue.Contains("#") || webValue.Contains(".") ||
//        //        (webValue.Contains("[") && webValue.Contains("]") && webValue.Contains("=")))
//        //    {
//        //        var selector = string.Format(
//        //            @"{0} {1}",
//        //                EditorSelector, webValue
//        //        ).Trim();
//        //        return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)),
//        //            string.Format("The '{0}' field does not appear to exist in @Browser (CSS selector was '{1}').",
//        //                fieldLabel, selector));
//        //    }
//        //    return null;
//        //}

//        //private IWebElement GetInputByName(string fieldLabel)
//        //{
//        //    var fieldName = SpecToWeb[fieldLabel];
//        //    var selector = string.Format(
//        //        @"{0} input[name=""{1}""]",
//        //            EditorSelector, fieldName
//        //    ).Trim();
//        //    return Browser.TryFindElement(By.CssSelector(selector));
//        //}

//        //private IWebElement GetTextAreaByName(string fieldLabel)
//        //{
//        //    var fieldName = SpecToWeb[fieldLabel];
//        //    var selector = string.Format(
//        //        @"{0} textarea[name=""{1}""]",
//        //            EditorSelector, fieldName
//        //    ).Trim();
//        //    return Browser.TryFindElement(By.CssSelector(selector));
//        //}

//        //private IWebElement GetInputById(string fieldLabel)
//        //{
//        //    var fieldName = SpecToWeb[fieldLabel];
//        //    var selector = string.Format(
//        //        @"{0} input#{1}",
//        //            EditorSelector, fieldName
//        //    ).Trim();
//        //    return Browser.TryFindElement(By.CssSelector(selector));
//        //}

//        //private IWebElement GetTextAreaById(string fieldLabel)
//        //{
//        //    var fieldName = SpecToWeb[fieldLabel];
//        //    var selector = string.Format(
//        //        @"{0} textarea#{1}",
//        //            EditorSelector, fieldName
//        //    ).Trim();
//        //    return Browser.TryFindElement(By.CssSelector(selector));
//        //}

//        //public IWebElement GetFileUploadField(string fieldLabel)
//        //{
//        //    const string keyFormat = "{0}[FileUpload]";
//        //    var key = string.Format(keyFormat, fieldLabel);
//        //    if (SpecToWeb.ContainsKey(key))
//        //    {
//        //        var fieldSelector = SpecToWeb[key];
//        //        var selector = string.Format(
//        //            @"{0} {1}",
//        //                EditorSelector, fieldSelector
//        //        ).Trim();
//        //        return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//        //            "A file upload input for the '{0}' field was not found in @Browser (CSS selector was '{1}').",
//        //                fieldLabel, selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //public IWebElement GetCheckBox(string fieldLabel)
//        //{
//        //    if (SpecToWeb.ContainsKey(fieldLabel))
//        //    {
//        //        var fieldSelector = SpecToWeb[fieldLabel];
//        //        var selector = string.Format(
//        //            @"{0} {1}",
//        //                EditorSelector, fieldSelector
//        //        ).Trim();
//        //        return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//        //            "A checkbox for the '{0}' field was not found in @Browser (CSS selector was '{1}').",
//        //                fieldLabel, selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //public IEnumerable<IWebElement> GetCollectionItems(string fieldLabel, string subSelector = "ItemText")
//        //{
//        //    const string keyFormat = "{0}[Collection='{1}']";
//        //    var key = string.Format(keyFormat, fieldLabel, subSelector);
//        //    if (SpecToWeb.ContainsKey(key))
//        //    {
//        //        var fieldSelector = SpecToWeb[key];
//        //        var selector = string.Format(
//        //            @"{0} {1}",
//        //                EditorSelector, fieldSelector
//        //        ).Trim();
//        //        return Browser.WaitUntil(b => b.FindElements(By.CssSelector(selector)), string.Format(
//        //            "A '{0}' field collection item (with '{1}' sub-selection) was not found in @Browser (CSS selector was '{2}').",
//        //                fieldLabel, subSelector, selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //public IWebElement GetCollectionItem(string fieldLabel, string subSelector)
//        //{
//        //    const string keyFormat = "{0}[Collection='{1}']";
//        //    var key = string.Format(keyFormat, fieldLabel, subSelector);
//        //    if (SpecToWeb.ContainsKey(key))
//        //    {
//        //        var fieldSelector = SpecToWeb[key];
//        //        var selector = string.Format(
//        //            @"{0} {1}",
//        //                EditorSelector, fieldSelector
//        //        ).Trim();
//        //        return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//        //            "A '{0}' field collection item (with '{1}' sub-selection) was not found in @Browser (CSS selector was '{2}').",
//        //                fieldLabel, subSelector, selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //public IWebElement GetErrorMessage(string fieldLabel, bool allowNull = false)
//        //{
//        //    if (SpecToWeb.ContainsKey(fieldLabel))
//        //    {
//        //        var fieldName = SpecToWeb[fieldLabel].Replace("_", ".");
//        //        var selector = string.Format(
//        //            @"{0} .field-validation-error[data-valmsg-for=""{1}""]",
//        //                EditorSelector, fieldName
//        //        ).Trim();
//        //        if (!allowNull)
//        //            return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//        //                "An error message for the '{0}' field (element named '{1}') should exist in @Browser.",
//        //                    fieldLabel, fieldName));

//        //        return Browser.TryFindElement(By.CssSelector(selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //public IWebElement GetAutoCompleteMenu(string fieldLabel, bool allowNull = false)
//        //{
//        //    const string keyFormat = "{0}[AutoComplete]";
//        //    var key = string.Format(keyFormat, fieldLabel);
//        //    if (SpecToWeb.ContainsKey(key))
//        //    {
//        //        var fieldSelector = SpecToWeb[key];
//        //        var selector = string.Format(
//        //            @"{0} {1} .autocomplete-menu ul",
//        //                EditorSelector, fieldSelector
//        //        ).Trim();
//        //        if (!allowNull)
//        //            return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//        //                "An autocomplete menu for the '{0}' field was not found by @Browser (CSS selector was '{1}').",
//        //                    fieldLabel, selector));

//        //        return Browser.TryFindElement(By.CssSelector(selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //public IWebElement GetDownArrowButton(string fieldLabel, bool allowNull = false)
//        //{
//        //    const string keyFormat = "{0}[DownArrow]";
//        //    var key = string.Format(keyFormat, fieldLabel);
//        //    if (SpecToWeb.ContainsKey(key))
//        //    {
//        //        var fieldSelector = SpecToWeb[key];
//        //        var selector = string.Format(
//        //            @"{0} {1}",
//        //                EditorSelector, fieldSelector
//        //        ).Trim();
//        //        if (!allowNull)
//        //            return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//        //            "An dropdown arrow button for the '{0}' field was not found by @Browser (CSS selector was '{1}').",
//        //                fieldLabel, selector));

//        //        return Browser.TryFindElement(By.CssSelector(selector));
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The field label '{0}' does not exist.", fieldLabel));
//        //}

//        //private const string ErrorTextKeyFormat = "{0}[ErrorText='{1}']";

//        //public string GetErrorText(string fieldLabel, string messageKey)
//        //{
//        //    var errorTextKey = string.Format(ErrorTextKeyFormat, fieldLabel, messageKey);
//        //    if (SpecToWeb.ContainsKey(errorTextKey))
//        //    {
//        //        return SpecToWeb[errorTextKey];
//        //    }

//        //    throw new NotSupportedException(
//        //        string.Format("The error text key '{0}' could not be found.", errorTextKey));
//        //}

//        public IWebElement GetButton(string buttonLabel)
//        {
//            if (SpecToWeb.ContainsKey(buttonLabel))
//            {
//                var buttonType = SpecToWeb[buttonLabel];
//                var selector = string.Format(@"{0} input[value=""{1}""][type=""{2}""]", EditorSelector, buttonLabel, buttonType);
//                return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//                    "An input {1} element with value '{0}' should exist using @Browser.", buttonLabel, buttonType));
//            }

//            throw new NotSupportedException(
//                string.Format("The button label '{0}' does not exist.", buttonLabel));
//        }

//        public IWebElement GetCustomContent(string content, bool allowNull = false)
//        {
//            if (SpecToWeb.ContainsKey(content))
//            {
//                var contentSelector = SpecToWeb[content];
//                var selector = string.Format(@"{0} {1}", EditorSelector, contentSelector);
//                if (!allowNull)
//                    return Browser.WaitUntil(b => b.FindElement(By.CssSelector(selector)), string.Format(
//                        "Custom content '{0}' (with CSS selector '{1}') should exist using @Browser.",
//                            content, selector));

//                return Browser.TryFindElement(By.CssSelector(selector));
//            }

//            throw new NotSupportedException(
//                string.Format("Custom content '{0}' does not exist.", content));
//        }

//        public string GetEmailExcerpt(string message)
//        {
//            if (EmailExcerptStart != null && EmailExcerptEnd != null)
//            {
//                var secretCodeStart = message.IndexOf(EmailExcerptStart, StringComparison.Ordinal) + EmailExcerptStart.Length;
//                var secretCodeEnd = message.Substring(secretCodeStart).IndexOf(EmailExcerptEnd, StringComparison.Ordinal);
//                var secretCode = message.Substring(secretCodeStart, secretCodeEnd);
//                return secretCode;
//            }

//            throw new NotSupportedException(
//                string.Format("An email was not expected on the '{0}' page.", Browser.Url));
//        }

//    }
//}
