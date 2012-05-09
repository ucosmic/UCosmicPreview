using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Should;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class FieldErrorSteps : BaseStepDefinition
    {
        [Then(@"I should (.*) the (.*) error message for the (.*) (text|upload) field")]
        public void SeeErrorMessage(string seeOrNot, string messageKey, string fieldLabel, string fieldType)
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

        [Then(@"I should (.*) the (.*) (text|upload) field's (.*) error message included in (.*) error summaries")]
        [Then(@"I should (.*) the (.*) (text|upload) field's (.*) error message included in (.*) error summary")]
        public void SeeErrorMessageInSummaries(string seeOrNot, string fieldLabel, string fieldType, string messageKey, string expectedSummaries)
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
    }
}
