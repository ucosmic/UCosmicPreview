using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class FormSteps : BaseStepDefinition
    {
        [Given(@"I clicked the ""(.*)"" button")]
        [When(@"I click the ""(.*)"" button")]
        [Then(@"I should click the ""(.*)"" button")]
        public void ClickLabeledButton(string label)
        {
            var cssSelector = string.Format("form input[type=button][value='{0}']", label);

            Browsers.ForEach(browser =>
            {
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Button labeled '{0}' was not found by @Browser (using CSS selector {1})",
                        label, cssSelector));
                button.ClickButton();
            });
        }

        [Given(@"I clicked the ""(.*)"" submit button")]
        [When(@"I click the ""(.*)"" submit button")]
        [Then(@"I should click the ""(.*)"" submit button")]
        public void ClickLabeledSubmitButton(string label)
        {
            var cssSelector = string.Format("form input[type=submit][value='{0}']", label);

            Browsers.ForEach(browser =>
            {
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Submit button labeled '{0}' was not found by @Browser (using CSS selector {1})",
                        label, cssSelector));
                button.ClickButton();
            });
        }

        [Given(@"I clicked the submit button")]
        [When(@"I click the submit button")]
        [Then(@"I should click the submit button")]
        public void ClickSubmitButton()
        {
            const string cssSelector = "form input[type=submit]";

            Browsers.ForEach(browser =>
            {
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Submit button was not found by @Browser (using CSS selector {0})",
                        cssSelector));
                button.ClickButton();
            });
        }

        [Given(@"I saw the flash feedback message ""(.*)""")]
        [When(@"I see the flash feedback message ""(.*)""")]
        [Then(@"I should see the flash feedback message ""(.*)""")]
        public void SeeFlashFeedbackMessage(string topMessage)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the element was located
                var validationElement = browser.WaitUntil(b => b.FindElement(By.Id("feedback_flash")),
                    string.Format("Flash feedback message element does not exist using @Browser."));

                // ensure the element is displayed
                browser.WaitUntil(b => validationElement.Displayed,
                    string.Format("Flash feedback message element was not displayed using @Browser."));

                // verify the success message
                browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(topMessage),
                    string.Format("Flash feedback message '{0}' was not displayed using @Browser. " +
                        "(Actual message was '{1}'.)", topMessage, validationElement.Text));
            });
        }
    }
}
