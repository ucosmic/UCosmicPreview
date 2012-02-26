using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class AuthorizationSteps : StepDefinitionBase
    {
        #region Role Authorization Form

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box on the Role Authorization (.*) form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Role Authorization (.*) form")]
        [Then(@"I should have typed ""(.*)"" into the ""(.*)"" text box on the Role Authorization (.*) form")]
        public void TypeValueIntoTheNameTextBox(string textToType, string textBoxId, string addOrEdit)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on the Role Authorization '{1}' form could not be found using @Browser.",
                        textBoxId, addOrEdit));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        [Given(@"I have clicked the cancel link on the Role Authorization (.*) form")]
        [When(@"I click the cancel link on the Role Authorization (.*) form")]
        [Then(@"I should click the cancel link on the Role Authorization (.*) form")]
        public void ClickTheCancelLink(string addOrEdit)
        {
            var cssSelector = string.Format("#role_form_{0} a#cancel_submit_button", addOrEdit);
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Cancel link does not exist on the Role Authorization '{0}' form could not be found using @Browser.", addOrEdit));

                // click the sign in submit button
                button.ClickLink();

            });
        }

        [Given(@"I have clicked the ""(.*)"" submit button on the Role Authorization (.*) form")]
        [When(@"I click the ""(.*)"" submit button on the Role Authorization (.*) form")]
        [Then(@"I should click the ""(.*)"" submit button on the Role Authorization (.*) form")]
        public void ClickTheCancelButton(string buttonLabel, string addOrEdit)
        {
            var buttonId = TranslateAddOrEditRoleLabelToElementId(buttonLabel);
            var cssSelector = string.Format("#role_form_{0} input#{1}", addOrEdit, buttonId);
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Submit button labeled '{0}' does not exist on the Role Authorization '{1}' form could not be found using @Browser.", buttonLabel, addOrEdit));

                // click the sign in submit button
                button.ClickButton();

            });
        }

        private static string TranslateAddOrEditRoleLabelToElementId(string fieldLabel)
        {
            switch (fieldLabel)
            {
                case "Add Role Authorization":
                    return "add_role_submit_button";

                case "Save Changes":
                    return "save_changes_submit_button";

                case "Cancel":
                    return "cancel_submit_button";

                default:
                    return fieldLabel;
            }
        }

        [Given(@"I have see the error message ""(.*)"" for the ""(.*)"" text box on the Role Authorization (.*) form")]
        [When(@"I see the error message ""(.*)"" for the ""(.*)"" text box on the Role Authorization (.*) form")]
        [Then(@"I should see the error message ""(.*)"" for the ""(.*)"" text box on the Role Authorization (.*) form")]
        public void SeeTheErrorMessageForTheInputElement(string expectedErrorMessage, string inputElementId, string addOrEdit)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            var shouldSee = (!string.IsNullOrWhiteSpace(expectedErrorMessage));

            // define the CSS Selector
            var cssSelector = string.Format("#role_form_editor span.field-validation-error[data-valmsg-for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the validation DOM element is located
                    var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                        "Validation element for the '{0}' input on the Role Authorization '{1}' form could not be found using @Browser.",
                            inputElementId, addOrEdit));

                    // verify the validation error message
                    browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage), string.Format(
                        "Expected error message '{1}' was not displayed for the '{0}' input on the Role Authorization '{3}' form using @Browser. " +
                            "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text, addOrEdit));
                }
                else
                {
                    // ensure that the validation DOM element is not located, not visible, or contains empty text
                    var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                    if (validationElement != null)
                    {
                        browser.WaitUntil(b => !validationElement.Displayed || !validationElement.Text.Trim().Equals(string.Empty), string.Format(
                            "Validation element for the '{0}' element on the Role Authorization '{1}'form was unexpectedly displayed using @Browser. "
                                + "(Actual message was '{0}', but nothing was expected.)", inputElementId, addOrEdit));
                    }
                }
            });
        }

        [Given(@"I have (.*) ""(.*)"" in the Members list box on the Role Authorization (.*) form")]
        [When(@"I (.*) ""(.*)"" in the Members list box on the Role Authorization (.*) form")]
        [Then(@"I should (.*) ""(.*)"" in the Members list box on the Role Authorization (.*) form")]
        public void SeeMemberInListBox(string seeOrNot, string expectedMember, string addOrEdit)
        {
            const string cssSelector = "ul#members_list li";
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "seen");

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // make sure the target item exists in the list
                    browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)).SingleOrDefault(li => li.Text.Equals(expectedMember)) != null,
                        string.Format("Member list box does not contain item with text '{0}' on the Role Authorization {1} form using {2} browser.",
                            expectedMember, addOrEdit, browser.Name()));
                    var targetItem = browser.FindElements(By.CssSelector(cssSelector)).SingleOrDefault(li => li.Text.Equals(expectedMember));

                    // verify the member name is displayed
                    // ReSharper disable PossibleNullReferenceException
                    browser.WaitUntil(b => targetItem.Displayed && targetItem.Text.Contains(expectedMember), string.Format(
                        // ReSharper restore PossibleNullReferenceException
                        "Members list box did not contain expected item '{0}' on the Role Authorization {2} form using @Browser. " +
                        // ReSharper disable PossibleNullReferenceException
                            "(Actual value was '{1}'.)", expectedMember, targetItem.Text, addOrEdit));
                    // ReSharper restore PossibleNullReferenceException
                }
                else
                {
                    // select all LI items in the UL
                    var memberItems = browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)), string.Format(
                        "Member list box items do not exist on the Role Authorization {0} form using @Browser.",
                            addOrEdit));

                    var targetItem = memberItems.SingleOrDefault(li => li.Text.Equals(expectedMember));
                    browser.WaitUntil(b => targetItem == null || !targetItem.Displayed, string.Format(
                        "Member list box item with text '{0}' was unexpectedly displayed on the Role Authorization {1} form using @Browser.",
                            expectedMember, addOrEdit));
                }
            });
        }

        [Given(@"I have seen a readonly hidden textbox for field ""(.*)""  on the Role Authorization Edit form")]
        [When(@"I see a readonly hidden textbox for field ""(.*)"" on the Role Authorization Edit form")]
        [Then(@"I should see a readonly hidden textbox for field ""(.*)"" on the Role Authorization Edit form")]
        public void SeeAReadonlyLabelForFieldName(string fieldId)
        {
            Browsers.ForEach(browser =>
            {
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id("Name")), string.Format(
                    "Label id '{0}' could not be found on the Add Contact modal dialog using @Browser.",
                        fieldId));

                var readOnlyAttribute = textBox.GetAttribute("hidden");

                browser.WaitUntil(b => readOnlyAttribute.Equals("false"), string.Format(
                    "Label id '{0}' was unexpectedly not in read only mode on the Add Contact modal dialog using @Browser.",
                        fieldId));
            });
        }
        #endregion
    }
    // ReSharper restore UnusedMember.Global
}
