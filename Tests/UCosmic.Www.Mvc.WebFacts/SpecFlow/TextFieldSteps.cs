using System;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class TextFieldSteps : BaseStepDefinition
    {
        [Then(@"I should(.*) see a (.*) text field")]
        public void SeeTextBox(string seeOrNot, string fieldLabel)
        {
            var shouldSee = string.IsNullOrWhiteSpace(seeOrNot);
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                if (shouldSee)
                {
                    var textBox = page.GetTextInputField(fieldLabel);
                    browser.WaitUntil(b => textBox != null && textBox.Displayed,
                        string.Format("The '{0}' text field was either not found or not displayed in @Browser",
                            fieldLabel));
                }
                else
                {
                    try
                    {
                        var textBox = page.GetTextInputField(fieldLabel);
                        browser.WaitUntil(b => textBox == null || !textBox.Displayed,
                            string.Format("The '{0}' text field was unexpectedly displayed in @Browser",
                                fieldLabel));
                    }
                    catch (NotSupportedException)
                    {
                        // input may not exist on page
                    }
                }
            });
        }

        [When(@"I type ""(.*)"" into the (.*) text field")]
        [When(@"I do type ""(.*)"" into the (.*) text field")]
        public void TypeIntoTextBox(string textToType, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                textBox.Clear();
                textBox.SendKeys(textToType);
            });
        }

        [When(@"I don't type ""(.*)"" into the (.*) text field")]
        public void DoNotTypeIntoTextBox(string textToType, string fieldLabel)
        {
            // place marker step for skipping text typing steps in exampled scenario outlines
        }

        [Then(@"I should see ""(.*)"" in the (.*) text field")]
        public void SeeValueInTextBox(string expectedValue, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                var value = page.GetTextInputValue(fieldLabel);

                browser.WaitUntil(b => textBox.Displayed && value.Equals(expectedValue),
                    string.Format("The value '{0}' was not displayed in the '{1}' text field by @Browser (actual value was '{2}').",
                        expectedValue, fieldLabel, textBox.Text));
            });
        }

        [Then(@"I shouldn't see ""(.*)"" in the (.*) text field")]
        public void DoNotSeeValueInTextBox(string unexpectedValue, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                var value = page.GetTextInputValue(fieldLabel);

                browser.WaitUntil(b => textBox.Displayed && !value.Equals(unexpectedValue),
                    string.Format("The value '{0}' was unexpectedly displayed in the '{1}' text field by @Browser.",
                        unexpectedValue, fieldLabel));
            });
        }

        [Then(@"The (.*) text field should be read only")]
        public void CheckTextBoxReadOnlyAttribute(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                browser.WaitUntil(b => textBox.GetAttribute("readonly").Equals("true"),
                    string.Format("The '{0}' text field is not in read-only mode.",
                        fieldLabel));
            });
        }
    }
}
