using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class TextFieldSteps : BaseStepDefinition
    {
        [Then(@"I should see a (.*) text field")]
        [Then(@"I should see an (.*) text field")]
        public void SeeTextField(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var textField = page.GetField(fieldLabel);
                browser.WaitUntil(b => textField.Displayed,
                    "The '{0}' text field was not displayed by @Browser"
                        .FormatWith(fieldLabel));
            });
        }

        [Then(@"I should not see a (.*) text field")]
        [Then(@"I should not see an (.*) text field")]
        [Then(@"I shouldn't see a (.*) text field")]
        [Then(@"I shouldn't see an (.*) text field")]
        public void DoNotSeeTextField(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var textField = page.GetField(fieldLabel, true);
                browser.WaitUntil(b => textField == null || !textField.Displayed,
                    "The '{0}' text field was unexpectedly displayed by @Browser"
                        .FormatWith(fieldLabel));
            });
        }

        [When(@"I type ""(.*)"" into the (.*) text field")]
        [When(@"I do type ""(.*)"" into the (.*) text field")]
        public void TypeIntoTextField(string textToType, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var textField = page.GetField(fieldLabel);
                textField.Clear();
                textField.SendKeys(textToType);
            });
        }

        [When(@"I don't type ""(.*)"" into the (.*) text field")]
        [When(@"I do not type ""(.*)"" into the (.*) text field")]
        public void DoNotTypeIntoTextField(string textToType, string fieldLabel)
        {
            // place marker step for skipping text typing steps in exampled scenario outlines
        }

        [Then(@"I should see ""(.*)"" in the (.*) text field")]
        public void SeeValueInTextField(string expectedValue, string fieldLabel)
        {
            SeeTextField(fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => page.GetTextInputValue(fieldLabel).Equals(expectedValue),
                    string.Format("The value '{0}' was not displayed in the '{1}' text field by @Browser (actual value was '{2}').",
                        expectedValue, fieldLabel, page.GetTextInputValue(fieldLabel)));
            });
        }

        [Then(@"I shouldn't see ""(.*)"" in the (.*) text field")]
        [Then(@"I should not see ""(.*)"" in the (.*) text field")]
        public void DoNotSeeValueInTextField(string unexpectedValue, string fieldLabel)
        {
            SeeTextField(fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                browser.WaitUntil(b => !page.GetTextInputValue(fieldLabel).Equals(unexpectedValue),
                    string.Format("The value '{0}' was unexpectedly displayed in the '{1}' text field by @Browser.",
                        unexpectedValue, fieldLabel));
            });
        }

        [Then(@"the (.*) text field should be read only")]
        public void EnsureTextFieldIsReadOnly(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var textBox = page.GetField(fieldLabel);
                browser.WaitUntil(b => textBox.GetAttribute("readonly").Equals("true"),
                    string.Format("The '{0}' text field is not in read-only mode on @Browser.",
                        fieldLabel));
            });
        }

        [Then(@"the (.*) text field shouldn't be read only")]
        [Then(@"the (.*) text field should not be read only")]
        public void EnsureTextFieldIsNotReadOnly(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var textBox = page.GetField(fieldLabel);
                browser.WaitUntil(b => textBox.GetAttribute("readonly").Equals("false"),
                    string.Format("The '{0}' text field is unexpectedly in read-only mode on @Browser.",
                        fieldLabel));
            });
        }
    }
}
