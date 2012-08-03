using System.Linq;
using Should;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class FieldErrorSteps : BaseStepDefinition
    {
        [Then(@"I should see the (.*) error message for the (.*) (text|upload) field")]
        [Then(@"I should still see the (.*) error message for the (.*) (text|upload) field")]
        public void SeeErrorMessage(string errorType, string fieldLabel, string fieldType)
        {
            if (errorType.IsNullOrWhiteSpace())
            {
                DoNotSeeErrorMessage(errorType, fieldLabel, fieldType);
                return;
            }

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var errorMessage = page.GetErrorMessage(fieldLabel);
                var messageText = page.GetErrorText(fieldLabel, errorType);
                browser.WaitUntil(b => errorMessage.Displayed,
                    "Error message '{0}' for the '{1}' field was not displayed by @Browser."
                        .FormatWith(messageText, fieldLabel));
                browser.WaitUntil(b => errorMessage.Text.Equals(messageText),
                    "@Browser displayed error message '{0}' instead of the expected '{1}' for the '{2}' field."
                        .FormatWith(errorMessage.Text, messageText, fieldLabel));
            });
        }

        [Then(@"I shouldn't see the (.*) error message for the (.*) (text|upload) field")]
        [Then(@"I should not see the (.*) error message for the (.*) (text|upload) field")]
        public void DoNotSeeErrorMessage(string errorType, string fieldLabel, string fieldType)
        {
            if (errorType.IsNullOrWhiteSpace()) return;

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var errorMessage = page.GetErrorMessage(fieldLabel, true);
                var messageText = page.GetErrorText(fieldLabel, errorType);
                browser.WaitUntil(b =>
                        errorMessage == null ||
                        !errorMessage.Displayed ||
                        !errorMessage.Text.Equals(messageText),
                    "Error message '{0}' was unexpectedly displayed for the '{1}' field by @Browser"
                        .FormatWith(errorMessage != null ? errorMessage.Text : null, fieldLabel));
            });
        }

        [Then(@"I shouldn't see any error messages for the (.*) (text|upload) field")]
        [Then(@"I should not see any error messages for the (.*) (text|upload) field")]
        public void DoNotSeeAnyErrorMessages(string fieldLabel, string fieldType)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var errorMessage = page.GetErrorMessage(fieldLabel, true);
                browser.WaitUntil(b =>
                        errorMessage == null ||
                        !errorMessage.Displayed,
                    "An error message was unexpectedly displayed for the '{0}' field by @Browser"
                        .FormatWith(fieldLabel));
            });
        }

        [Then(@"I should see the (.*) (text|upload) field's (.*) error message included in (.*) error summaries")]
        public void SeeErrorMessageInSummaries(string fieldLabel, string fieldType, string errorType, string expectedSummaries)
        {
            var expectedSummaryCount = 1;
            int.TryParse(expectedSummaries, out expectedSummaryCount);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var messageText = page.GetErrorText(fieldLabel, errorType);

                browser.WaitUntil(b =>
                        page.GetErrorSummaries() != null &&
                        page.GetErrorSummaries().Any(s => s.GetElements(ByTagNameLi).Any(ElementTextEquals(messageText))),
                    "@Browser did not find any error summaries with the '{0}' field's '{1}' error message."
                        .FormatWith(fieldLabel, errorType));

                // ensure that there are the required number of validation summaries (perhaps 1 at top, 1 at bottom)
                var errorSummaries = page.GetErrorSummaries().ToArray();
                errorSummaries.Length.ShouldEqual(expectedSummaryCount,
                    "@Browser did not display {0} error summar{1} for the '{2}' field's '{3}' error message (actual summary count was {4})."
                        .FormatWith(expectedSummaryCount, expectedSummaryCount == 1 ? "y" : "ies", fieldLabel, errorType, errorSummaries.Length));

                // loop over all summaries
                foreach (var errorSummary in errorSummaries)
                {
                    var summary = errorSummary;
                    browser.WaitUntil(b => summary.Displayed,
                        "At least one expected error summary including the '{0}' message for the '{1}' field was not displayed by @Browser."
                            .FormatWith(errorType, fieldLabel));

                    var item = summary.GetElements(ByTagNameLi).FirstOrDefault(ElementTextEquals(messageText));
                    browser.WaitUntil(b => item != null && item.Displayed,
                        "Error summary item '{0}' for the '{1}' field was not displayed by @Browser."
                            .FormatWith(errorType, fieldLabel));
                }
            });
        }

        [Then(@"I shouldn't see the (.*) (text|upload) field's (.*) error message included in (.*) error summaries")]
        [Then(@"I should not see the (.*) (text|upload) field's (.*) error message included in (.*) error summary")]
        public void DoNotSeeErrorMessageInSummaries(string fieldLabel, string fieldType, string errorType, string expectedSummaries)
        {
            var expectedSummaryCount = 1;
            int.TryParse(expectedSummaries, out expectedSummaryCount);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var messageText = page.GetErrorText(fieldLabel, errorType);

                browser.WaitUntil(b =>
                        page.GetErrorSummaries().IsNull() ||
                        !page.GetErrorSummaries().Any(s => s.FindElements(ByTagNameLi).Any(
                            li => li.Text.Equals(messageText) && li.Displayed)),
                    "@Browser unexpectedly found at least one error summary with the '{0}' field's '{1}' error message."
                        .FormatWith(fieldLabel, errorType));
            });
        }
    }
}
