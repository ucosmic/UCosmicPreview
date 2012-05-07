using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class PasswordSteps : BaseStepDefinition
    {
        #region Change Password form

        [Given(@"I have clicked the ""Change Password"" button on the Change Password form")]
        [When(@"I click the ""Change Password"" button on the Change Password form")]
        [Then(@"I click the ""Change Password"" button on the Change Password form")]
        public void ClickTheChangePasswordButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.Id("change_password_submit_button")),
                    string.Format("Change Password submit button on the Change Password form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }

        [Given(@"I have clicked the ""Cancel"" button on the Change Password form")]
        [When(@"I click the ""Cancel"" button on the Change Password form")]
        [Then(@"I should click the ""Cancel"" button on the Change Password form")]
        public void ClickTheCancelButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(".cancel-form")),
                    string.Format("Cancel submit button on the Change Password form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }

        [Given(@"I have seen the error message ""(.*)"" for the ""(.*)"" text box on the Change Password form")]
        [When(@"I see the error message ""(.*)"" for the ""(.*)"" text box on the Change Password form")]
        [Then(@"I should see the error message ""(.*)"" for the ""(.*)"" text box on the Change Password form")]
        public void SeeTheErrorMessageForTheInputElement(string expectedErrorMessage, string inputElementId)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            var shouldSee = (!string.IsNullOrWhiteSpace(expectedErrorMessage));

            // define the CSS Selector
            var cssSelector = string.Format("#change_password_editor span.field-validation-error span[for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the validation DOM element is located
                    var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                        string.Format("Validation element for the '{0}' input on the Username & Password form could not be found using @Browser.",
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

        [Given(@"I have seen the validation error message ""(.*)"" for the ""(.*)"" text box on the Change Password form ")]
        [When(@"I see the validation error message ""(.*)"" for the ""(.*)"" text box on the Change Password form")]
        [Then(@"I should see the validation error message ""(.*)"" for the ""(.*)"" text box on the Change Password form")]
        public void SeeTheValidationErrorMessageForTheInputElement(string expectedErrorMessage, string inputElementId)
        {
            var cssSelector = string.Format("#change_password_editor span.field-validation-error[data-valmsg-for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                // ensure that the validation DOM element was located
                var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Validation element for the '{0}' input on the Change Password form could not be found using @Browser.",
                        inputElementId));

                // verify the validation error message
                browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage),
                     string.Format("Expected error message '{0}' was not displayed for the '{1}' input on the Change Password form using @Browser. " +
                     "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text));
            });
        }

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box on the Change Password form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Change Password form")]
        [Then(@"I type ""(.*)"" into the ""(.*)"" text box on the Change Password form")]
        public void TypeTextIntoTheTextBox(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on the My Name form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        #endregion
        #region Forgot Password form

        [Given(@"I have seen the error message ""(.*)"" for the ""(.*)"" text box on the Reset Password form")]
        [When(@"I see the error message ""(.*)"" for the ""(.*)"" text box on the Reset Password form")]
        [Then(@"I should see the error message ""(.*)"" for the ""(.*)"" text box on the Reset Password form")]
        public void SeeTheErrorMessageForTheInputElementForResetPasswordForm(string expectedErrorMessage, string inputElementId)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            var shouldSee = (!string.IsNullOrWhiteSpace(expectedErrorMessage));

            // define the CSS Selector
            var cssSelector = string.Format("#forgot_password_editor span.field-validation-error span[for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the validation DOM element is located
                    var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                        string.Format("Validation element for the '{0}' input on the Reset form could not be found using @Browser.",
                            inputElementId));

                    // verify the validation error message
                    browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage), string.Format(
                        "Expected error message '{1}' was not displayed for the '{0}' input on the Reset Password form using @Browser. " +
                            "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text));
                }
                else
                {
                    // ensure that the validation DOM element is not located, not visible, or contains empty text
                    var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                    if (validationElement != null)
                    {
                        browser.WaitUntil(b => !validationElement.Displayed || !validationElement.Text.Trim().Equals(string.Empty), string.Format(
                            "Validation element for the '{0}' element on the Reset Password form was unexpectedly displayed using @Browser. "
                                + "(Actual message was '{0}', but nothing was expected.)", inputElementId));
                    }
                }
            });
        }

        [Given(@"I have seen the validation error message ""(.*)"" for the ""(.*)"" text box on the Reset Password form")]
        [When(@"I see the validation error message ""(.*)"" for the ""(.*)"" text box on the Reset Password form")]
        [Then(@"I should see the validation error message ""(.*)"" for the ""(.*)"" text box on the Reset Password form")]
        public void SeeTheValidationErrorMessageForTheInputElementForResetPasswordForm(string expectedErrorMessage, string inputElementId)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            var shouldSee = (!string.IsNullOrWhiteSpace(expectedErrorMessage));

             //define the CSS Selector
            var cssSelector =string.Format("#forgot_password_editor span.field-validation-error[data-valmsg-for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the validation DOM element is located
                    var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                        string.Format("Validation element for the '{0}' input on the Reset form could not be found using @Browser.",
                            inputElementId));

                    // verify the validation error message
                    browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage), string.Format(
                        "Expected error message '{1}' was not displayed for the '{0}' input on the Reset Password form using @Browser. " +
                            "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text));
                }
                else
                {
                    // ensure that the validation DOM element is not located, not visible, or contains empty text
                    var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                    if (validationElement != null)
                    {
                        browser.WaitUntil(b => !validationElement.Displayed || !validationElement.Text.Trim().Equals(string.Empty), string.Format(
                            "Validation element for the '{0}' element on the Reset Password form was unexpectedly displayed using @Browser. "
                                + "(Actual message was '{0}', but nothing was expected.)", inputElementId));
                    }
                }
            });
        }
        [Given(@"I have clicked the ""Send Confirmation Email"" button on the Reset Password form")]
        [When(@"I click the ""Send Confirmation Email"" button on the Reset Password form")]
        [Then(@"I click the ""Send Confirmation Email"" button on the Reset Password form")]
        public void ClickTheResetPasswordSubmitButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.Id("forgot_password_submit_button")),
                    string.Format("Reset Password submit button on the Reset Password form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box on the Reset Password form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Reset Password form")]
        [Then(@"I type ""(.*)"" into the ""(.*)"" text box on the Reset Password form")]
        public void TypeTextIntoTheTextBoxForResetPasswordForm(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on the Reset Password form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }
        [Given(@"I have clicked the ""Cancel"" button on the Reset Password form")]
        [When(@"I click the ""Cancel"" button on the Reset Password form")]
        [Then(@"I should click the ""Cancel"" button on the Reset Password form")]
        public void ClickTheCancelButtonForResetPasswordForm()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the cancel submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(".cancel-form")),
                    string.Format("Cancel submit button on the Reset Password form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }
        #endregion
    }
    // ReSharper restore UnusedMember.Global
}
