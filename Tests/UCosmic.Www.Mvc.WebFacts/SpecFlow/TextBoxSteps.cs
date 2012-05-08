using System;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class TextBoxSteps : BaseStepDefinition
    {
        [Then(@"I should(.*) see a (.*) field")]
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
                        string.Format("The '{0}' field was either not found or not displayed in @Browser",
                            fieldLabel));
                }
                else
                {
                    try
                    {
                        var textBox = page.GetTextInputField(fieldLabel);
                        browser.WaitUntil(b => textBox == null || !textBox.Displayed,
                            string.Format("The '{0}' field was unexpectedly displayed in @Browser",
                                fieldLabel));
                    }
                    catch (NotSupportedException)
                    {
                        // input may not exist on page
                    }
                }
            });
        }

        [Given(@"I typed ""(.*)"" into the (.*) field")]
        [When(@"I type ""(.*)"" into the (.*) field")]
        [When(@"I do type ""(.*)"" into the (.*) field")]
        [Then(@"I should type ""(.*)"" into the (.*) field")]
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

        [Given(@"I didn't type ""(.*)"" into the (.*) field")]
        [When(@"I don't type ""(.*)"" into the (.*) field")]
        [Then(@"I shouldn't type ""(.*)"" into the (.*) field")]
        public void DoNotTypeIntoTextBox(string textToType, string fieldLabel)
        {
            // place marker step for skipping text typing steps in exampled scenario outlines
        }

        [Given(@"I saw ""(.*)"" in the (.*) field")]
        [When(@"I see ""(.*)"" in the (.*) field")]
        [When(@"I do see ""(.*)"" in the (.*) field")]
        [Then(@"I should see ""(.*)"" in the (.*) field")]
        public void SeeValueInTextBox(string expectedValue, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                var value = page.GetTextInputValue(fieldLabel);

                browser.WaitUntil(b => textBox.Displayed && value.Equals(expectedValue),
                    string.Format("The value '{0}' was not displayed in the '{1}' field by @Browser (actual value was '{2}').",
                        expectedValue, fieldLabel, textBox.Text));
            });
        }

        [Given(@"I didn't see ""(.*)"" in the (.*) field")]
        [When(@"I don't see ""(.*)"" in the (.*) field")]
        [Then(@"I shouldn't see ""(.*)"" in the (.*) field")]
        public void DoNotSeeValueInTextBox(string unexpectedValue, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                var value = page.GetTextInputValue(fieldLabel);

                browser.WaitUntil(b => textBox.Displayed && !value.Equals(unexpectedValue),
                    string.Format("The value '{0}' was unexpectedly displayed in the '{1}' field by @Browser.",
                        unexpectedValue, fieldLabel));
            });
        }

        [Then(@"The (.*) field should be read only")]
        public void CheckTextBoxReadOnlyAttribute(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextInputField(fieldLabel);
                browser.WaitUntil(b => textBox.GetAttribute("readonly").Equals("true"),
                    string.Format("The '{0}' field is not in read-only mode.",
                        fieldLabel));
            });
        }
    }
}
