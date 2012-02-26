using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.Steps;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class AuthenticationSteps : StepDefinitionBase
    {
        #region My Username and Password Form

        [Given(@"I have signed in as ""(.*)"" with password ""(.*)""")]
        [When(@"I sign in as ""(.*)"" with password ""(.*)""")]
        [Then(@"I should sign in as ""(.*)"" with password ""(.*)""")]
        public void SignInAs(string email, string password)
        {
            var shared = new SharedSteps();
            shared.BrowseToPageAt(RelativeUrl.SignIn);
            TypeIntoTextBox(email, "EmailAddress");
            TypeIntoTextBox(password, "Password");
            ClickSignInButton();
            shared.SeePageAt(RelativeUrl.Me);
            SeeATopIdentityAreaWithPartialGreeting("in", email);
        }

        [Given(@"I have signed out")]
        [When(@"I sign out")]
        [Then(@"I should sign out")]
        public void SignOut()
        {
            new SharedSteps().BrowseToPageAt("sign-out");
        }

        // ReSharper disable MemberCanBePrivate.Global
        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box on the Username & Password form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Username & Password form")]
        [Then(@"I should type ""(.*)"" into the ""(.*)"" text box on the Username & Password form")]
        public void TypeIntoTextBox(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)), string.Format(
                    "The '{0}' text box on the Username & Password form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }
        // ReSharper restore MemberCanBePrivate.Global

        // ReSharper disable MemberCanBePrivate.Global
        [Given(@"I have clicked the ""Sign In"" button on the Username & Password form")]
        [When(@"I click the ""Sign In"" button on the Username & Password form")]
        [Then(@"I should click the ""Sign In"" button on the Username & Password form")]
        public void ClickSignInButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the sign in submit button is located
                var button = browser.WaitUntil(b => b.FindElement(By.Id("sign_in_submit_button")), string.Format(
                    "Sign In button could not be found on the Username & Password form using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }
        // ReSharper restore MemberCanBePrivate.Global

        [Given(@"I have seen the error message ""(.*)"" for the ""(.*)"" text box on the Username & Password form")]
        [When(@"I see the error message ""(.*)"" for the ""(.*)"" text box on the Username & Password form")]
        [Then(@"I should see the error message ""(.*)"" for the ""(.*)"" text box on the Username & Password form")]
        public void SeeErrorMessageForInputElement(string expectedErrorMessage, string inputElementId)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            var shouldSee = (!string.IsNullOrWhiteSpace(expectedErrorMessage));

            // define the CSS Selector
            var cssSelector = string.Format(
                "#sign_in_editor span.field-validation-error[data-valmsg-for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the validation DOM element is located
                    var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                        "Validation element for the '{0}' input on the Username & Password form could not be found using @Browser.",
                            inputElementId));

                    // verify the validation error message
                    browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage), string.Format(
                        "Expected error message '{1}' was not displayed for the '{0}' input on the Username & Password form using @Browser. " +
                            "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text));
                }
                else
                {
                    // ensure that the validation DOM element is not located, not visible, or contains empty text
                    var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                    if (validationElement != null)
                    {
                        browser.WaitUntil(b => !validationElement.Displayed || !validationElement.Text.Trim().Equals(string.Empty), string.Format(
                            "Validation element for the '{0}' element on the Username & Password form was unexpectedly displayed using @Browser. "
                                + "(Actual message was '{0}', but nothing was expected.)", inputElementId));
                    }
                }
            });
        }

        [Given(@"I have seen the error message ""(.*)"" in the error summary on the Username & Password form")]
        [When(@"I see the error message ""(.*)"" in the error summary on the Username & Password form")]
        [Then(@"I should see the error message ""(.*)"" in the error summary on the Username & Password form")]
        public void SeeErrorSummaryWithMessage(string expectedErrorMessage)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            if (string.IsNullOrWhiteSpace(expectedErrorMessage))
                return;

            Browsers.ForEach(browser =>
            {
                // find the validation summary
                var validationSummary = browser.WaitUntil(b => b.FindElement(By.CssSelector("div.validation-summary-errors ul")),
                    string.Format("Validation summary does not exist on the Username & Password form using @Browser."));

                // make sure the validation summary is visible
                browser.WaitUntil(b => validationSummary.Displayed,
                    string.Format("Validation summary is not displayed on the Username & Password form using @Browser."));

                // find the summary list items
                var errorMessages = browser.WaitUntil(b => b.FindElements(By.CssSelector("div.validation-summary-errors ul li")),
                    string.Format("Validation summary list items do not exist on the Username & Password form using @Browser."));

                // search the items for the expected error message
                // foreach (var errorMessage in errorMessages) // converted to LINQ expression
                // {
                //    if (errorMessage.Displayed && errorMessage.Text.Equals(expectedErrorMessage)) return;
                // }
                if (errorMessages.Any(errorMessage => errorMessage.Displayed && errorMessage.Text.Equals(expectedErrorMessage)))
                {
                    return; // the error message was found
                }

                // if we got this far, the error message could not be found]
                Assert.Fail(string.Format("Validation summary list item with error message '{0}' could not be found or was not displayed using {1} browser.",
                    expectedErrorMessage, browser.Name()));
            });
        }

        [Given(@"I have seen a top  identity signed-(.*) message with ""(.*)"" greeting")]
        [When(@"I see a top identity signed-(.*) message with ""(.*)"" greeting")]
        [Then(@"I should see a top identity signed-(.*) message with ""(.*)"" greeting")]
        // ReSharper disable MemberCanBePrivate.Global
        public void SeeATopIdentityAreaWithPartialGreeting(string inOrOut, string partialGreeting)
        // ReSharper restore MemberCanBePrivate.Global
        {
            var cssSelector = string.Format(".identity .signed-{0}", inOrOut);
            Browsers.ForEach(browser =>
            {
                // ensure that the validation DOM element is located
                var successElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Top identity authentication element could not be found using @Browser."));

                // verify the validation message exists & is visible
                browser.WaitUntil(b => successElement.Displayed && successElement.Text.Contains(partialGreeting),
                    string.Format("Expected top identity authentication partialGreeting '{0}' was not displayed in @Browser. "
                        + "(Actual value was '{1}'.)", partialGreeting, successElement.Text));
            });
        }

        #endregion

    }
    // ReSharper restore UnusedMember.Global
}
