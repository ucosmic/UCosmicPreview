using System;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;
using Should;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class FormSteps : BaseStepDefinition
    {
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
                90.WaitThisManyMilleseconds();
                button.Submit();
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
                100.WaitThisManyMilleseconds();
                button.Submit();
            });
        }

        [Given(@"I (.*) the (.*) error message for the (.*) field")]
        [When(@"I (.*) the (.*) error message for the (.*) field")]
        [Then(@"I should (.*) the (.*) error message for the (.*) field")]
        public void SeeErrorMessage(string seeOrNot, string messageKey, string fieldLabel)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "saw");
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                switch (shouldSee)
                {
                    case true:
                        var errorMessage = page.GetErrorMessage(fieldLabel);
                        var messageText = page.GetErrorText(fieldLabel, messageKey);
                        browser.WaitUntil(b => errorMessage.Displayed == shouldSee,
                            string.Format("Error message '{0}' was not displayed in @Browser.",
                                messageText));
                        browser.WaitUntil(b => errorMessage.Text.StartsWith(messageText),
                            string.Format("Error message displayed was '{0}' instead of the expected '{1}' in @Browser.",
                                errorMessage.Text, messageText));
                        break;

                    case false:
                        errorMessage = page.GetErrorMessage(fieldLabel, true);
                        browser.WaitUntil(b => errorMessage == null || !errorMessage.Displayed,
                            string.Format("Error message '{0}' was unexpectedly displayed in @Browser",
                            errorMessage != null ? errorMessage.Text : null));
                        break;
                }
            });
        }

        [Given(@"I (.*) the message ""(.*)"" included in (.*) error summaries")]
        [Given(@"I (.*) the message ""(.*)"" included in (.*) error summary")]
        [When(@"I (.*) the message ""(.*)"" included in (.*) error summaries")]
        [When(@"I (.*) the message ""(.*)"" included in (.*) error summary")]
        [Then(@"I should (.*) the (.*) field's (.*) error message included in (.*) error summaries")]
        [Then(@"I should (.*) the (.*) field's (.*) error message included in (.*) error summary")]
        public void SeeErrorMessageInSummaries(string seeOrNot, string fieldLabel, string messageKey, string expectedSummaries)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "saw");
            var expectedSummaryCount = 1;
            int.TryParse(expectedSummaries, out expectedSummaryCount);

            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var messageText = page.GetErrorText(fieldLabel, messageKey);

                // ensure that there are the required number of validation summaries (perhaps 1 at top, 1 at bottom)
                var errorSummaries = browser.WaitUntil(b => b.FindElements(By.CssSelector("div[data-valmsg-summary=true]")),
                    string.Format("Error summary or summaries do not exist using @Browser."));
                errorSummaries.Count.ShouldEqual(expectedSummaryCount, string.Format(
                    "The expected number of error summaries '{0}' were not displayed in @Browser. " +
                    "(Actual number of summaries is '{1}')", expectedSummaryCount, errorSummaries.Count));

                // loop over all summaries
                foreach (var errorSummary in errorSummaries)
                {
                    var summary = errorSummary;
                    browser.WaitUntil(b => summary.Displayed, string.Format(
                        "At least one error summary was not displayed in @Browser."));

                    var errorMessages = browser.WaitUntil(b => summary.FindElements(By.CssSelector("li")), string.Format(
                        "Error summary items could not be found in error summary using @Browser."));

                    // make sure the expected message is part of the summary
                    if (shouldSee && errorMessages.Any(errorMessage => errorMessage.Text.Equals(messageText)))
                        continue;

                    if (!shouldSee && !errorMessages.Any(errorMessage => errorMessage.Text.Equals(messageText)))
                        continue;

                    if (shouldSee)
                        Assert.Fail(string.Format("Error summary did not contain expected message '{0}' in {2}. " +
                            "(Actual content of validation summary was '{1}'.)",
                                messageText, summary.Text, browser.Name()));

                    //if (!shouldSee)
                    Assert.Fail(string.Format("Error summary contained unexpected message '{0}' in {2}. " +
                        "(Actual content of validation summary was '{1}'.)",
                            messageText, summary.Text, browser.Name()));
                }
            });
        }

        [Given(@"I typed ""(.*)"" into the (.*) field")]
        [When(@"I type ""(.*)"" into the (.*) field")]
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

        [Given(@"I saw ""(.*)"" in the (.*) field")]
        [When(@"I see ""(.*)"" in the (.*) field")]
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

        [Given(@"I didn't type ""(.*)"" into the (.*) field")]
        [When(@"I don't type ""(.*)"" into the (.*) field")]
        [Then(@"I shouldn't type ""(.*)"" into the (.*) field")]
        public void DoNotTypeIntoTextBox(string textToType, string fieldLabel)
        {
            // place marker step for skipping text typing steps in exampled scenario outlines
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
