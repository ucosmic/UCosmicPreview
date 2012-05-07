using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class SelfSteps : BaseStepDefinition
    {
        #region My Name form

        [Given(@"I have (.*)ed the DisplayName automatic generation checkbox in the My Name form")]
        [When(@"I (.*) the DisplayName automatic generation checkbox in the My Name form")]
        public void CheckOrUncheckTheDisplayNameAutoGenerationCheckbox(string checkOrUncheck)
        {
            const string checkBoxId = "IsDisplayNameDerived";
            var shouldBeChecked = (checkOrUncheck == "check"); // decide whether the box should be checked or unchecked

            Browsers.ForEach(browser =>
            {
                // ensure we can find the checkbox input element
                var checkBox = browser.WaitUntil(b => b.FindElement(By.Id(checkBoxId)), string.Format(
                    "Display Name automatic generation checkbox (id '{0}') was not found on the My Name form using @Browser.",
                        checkBoxId));

                // check or uncheck the checkbox
                checkBox.CheckOrUncheckCheckBox(shouldBeChecked);
            });
        }

        [Given(@"I typed ""(.*)"" into the ""(.*)"" text box on the My Name form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the My Name form")]
        [Then(@"I type ""(.*)"" into the ""(.*)"" text box on the My Name form")]
        public void TypeIntoTheTextBox(string textToType, string textBoxId)
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

        [Given(@"I clicked the ""Save Changes"" button on the My Name form")]
        [When(@"I click the ""Save Changes"" button on the My Name form")]
        public void ClickTheMyNameSaveChangesButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.Id("person_name_submit_button")),
                    string.Format("Save changes submit button on the My Name form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }

        [Given(@"I have successfully submitted the My Name form")]
        [When(@"I successfully submit the My Name form")]
        [Then(@"I should successfully submit the My Name form")]
        public void SubmitTheMyNameForm()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.Id("person_name_submit_button")),
                    string.Format("Save changes submit button on the My Name form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();

                if (browser.IsInternetExplorer())
                    2000.WaitThisManyMilleseconds();
            });
        }

        [When(@"I see ""(.*)"" in the ""(.*)"" text box on the My Name form")]
        [Then(@"I should see ""(.*)"" in the ""(.*)"" text box on the My Name form")]
        public void SeeExpectedValueInTheTextBox(string expectedValue, string textBoxId)
        {
            var jQuery = string.Format("return $('#{0}').val();", textBoxId);

            Browsers.ForEach(browser =>
            {
                // use jQuery to get current value of the text box
                var actualValue = browser.ExecuteScript(jQuery).ToString();
                if (actualValue != expectedValue)
                {
                    browser.WaitUntil(b => browser.ExecuteScript(jQuery).ToString().Equals(expectedValue),
                        string.Format("The My Name form '{0}' text box has value '{1}' but value '{2}' was expected in @Browser.",
                            textBoxId, browser.ExecuteScript(jQuery).ToString(), expectedValue));
                }
            });
        }

        [Then(@"I should see the error message ""(.*)"" for the ""(.*)"" text box on the My Name form")]
        public void SeeErrorMessageForTheInputElement(string expectedErrorMessage, string inputElementId)
        {
            var cssSelector = string.Format("#person_self_editor span.field-validation-error span[for='{0}']", inputElementId);

            Browsers.ForEach(browser =>
            {
                // ensure that the validation DOM element was located
                var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Validation element for the '{0}' input on the My Name form could not be found using @Browser.",
                        inputElementId));

                // verify the validation error message
                browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage),
                     string.Format("Expected error message '{0}' was not displayed for the '{1}' input on the My Name form using @Browser. " +
                     "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text));
            });
        }

        #endregion
        #region Affiliation Info form

        [Given(@"I have clicked the radio button for ""(.*)"" on the Affiliation Info form")]
        [When(@"I click the radio button for ""(.*)"" on the Affiliation Info form")]
        [Then(@"I should click the radio button for ""(.*)"" on the Affiliation Info form")]
        public void ClickTheRadioButtonLabeled(string radioButtonLabel)
        {
            var radioButtonId = TranslateRadioButtonLabelToRadioButtonId(radioButtonLabel);

            Browsers.ForEach(browser =>
            {
                // ensure that the radio button was located
                var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioButtonId)), string.Format(
                    "A radio button labeled '{0}' (element id '{1}') does not exist on the Affiliation Info form using @Browser.",
                        radioButtonLabel, radioButtonId));

                // click the radio button
                radioButton.ClickRadioButton();
            });
        }

        [Given(@"I have clicked the ""Save Changes"" button on the Affiliation Info form")]
        [When(@"I click the ""Save Changes"" button on the Affiliation Info form")]
        [Then(@"I should click the ""Save Changes"" button on the Affiliation Info form")]
        public void ClickTheAffiliationInfoSaveChangesButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var submitButton = browser.WaitUntil(b => b.FindElement(By.Id("save_changes_submit_button")), string.Format(
                    "A Save Changes submit button could not be found on the Affiliation Info form using @Browser."));

                // click the sign in submit button
                submitButton.ClickSubmitButton();
                1000.WaitThisManyMilleseconds();
            });
        }

        [Given(@"I have (.*) additional employee fields on the Affiliation Info form")]
        [When(@"I (.*) additional employee fields on the Affiliation Info form")]
        [Then(@"I should (.*) additional employee fields on the Affiliation Info form")]
        public void SeeAnEmployeeSectionOnAffiliationInfoForm(string seeOrNot)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    //ensure the employee section is located
                    var employeeDiv = browser.WaitUntil(b => b.FindElement(By.Id("employee_form")), string.Format(
                        "Employee section on the Affiliation Info form could not be found in @Browser."));

                    //verify that the employee section is found
                    browser.WaitUntil(b => employeeDiv.Displayed, string.Format(
                        "Employee section on the Affiliation Info form was not displayed in @Browser."));
                }
                else
                {
                    // make sure either the employee section does not exist or that it is not displayed
                    var employeeDiv = browser.TryFindElement(By.Id("employee_form"));
                    browser.WaitUntil(b => employeeDiv == null || !employeeDiv.Displayed, string.Format(
                        "Employee form for the '{0}' input on the Affiliation Info form was unexpectedly displayed using @Browser.",
                           employeeDiv));
                }
            });
        }

        [Given(@"I have (.*)ed the ""(.*)"" checkbox on the Affiliation Info form")]
        [When(@"I (.*) the ""(.*)"" checkbox on the Affiliation Info form")]
        [Then(@"I should (.*) the ""(.*)"" checkbox on the Affiliation Info form")]
        public void CheckOrUncheckTheAffiliationInfoCheckboxLabeled(string checkOrUncheck, string checkboxLabel)
        {
            var checkBoxId = TranslateCheckBoxLabelToCheckBoxId(checkboxLabel); // translate the label to element id
            var shouldBeChecked = (checkOrUncheck == "check"); // decide whether the box should be checked or unchecked

            Browsers.ForEach(browser =>
            {
                // ensure we can find the checkbox input element
                var checkBox = browser.WaitUntil(b => b.FindElement(By.Id(checkBoxId)), string.Format(
                    "The checkbox labeled '{0}' (element id '{1}') does not exist on the Affiliation Info form using @Browser.",
                        checkboxLabel, checkBoxId));

                // check or uncheck the checkbox
                checkBox.CheckOrUncheckCheckBox(shouldBeChecked);
            });
        }

        [Given(@"I have seen the ""(.*)"" checkbox is (.*) on the Affiliation Info form")]
        [When(@"I see the ""(.*)"" checkbox is (.*) on the Affiliation Info form")]
        [Then(@"I should see the ""(.*)"" checkbox is (.*) on the Affiliation Info form")]
        public void EnsureEmployeeCategoryCheckBoxIs(string checkboxLabel, string checkedOrUnchecked)
        {
            var checkBoxId = TranslateCheckBoxLabelToCheckBoxId(checkboxLabel);
            var shouldBeChecked = (checkedOrUnchecked.Trim() == "checked");

            Browsers.ForEach(browser =>
            {
                // ensure we can find the checkbox input element
                var checkbox = browser.WaitUntil(b => b.FindElement(By.Id(checkBoxId)), string.Format(
                    "The checkbox with id '{0}' does not exist on the Affiliation Info form using @Browser.", checkBoxId));

                // ensure the checkbox is in the expected state
                browser.WaitUntil(b => checkbox.Selected == shouldBeChecked, string.Format(
                    "The checkbox labeled '{0}' was expected to be '{1}' but it was not on the Affiliation Info form using @Browser.",
                        checkboxLabel, checkedOrUnchecked));
            });
        }

        [Given(@"I have (.*) ""(.*)"" into the ""Job Titles & Departments"" text box on the Affiliation Info form")]
        [When(@"I (.*) ""(.*)"" into the ""Job Titles & Departments"" text box on the Affiliation Info form")]
        [Then(@"I should (.*) ""(.*)"" into the ""Job Titles & Departments"" text box on the Affiliation Info form")]
        public void TypeIntoTheJobTitlesDepartmentsTextBox(string typeOrNot, string textToType)
        {
            if (typeOrNot != "type" && typeOrNot != "typed") return;

            Browsers.ForEach(browser =>
            {
                // ensure that text box is located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id("JobTitles")),
                    string.Format("The Job Title & Departments text box does not exist on the Affiliation Info form using @Browser."));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        [Given(@"I have seen ""(.*)"" Affiliation Info text on the My Affiliations section of the About Me url")]
        [When(@"I see ""(.*)"" Affiliation Info text on the My Affiliations section of the About Me url")]
        [Then(@"I should see ""(.*)"" Affiliation Info text on the My Affiliations section of the About Me url")]
        public void SeeAffiliationTypeInfoText(string affiliationInfo)
        {
            const string cssSelector = "div.affiliation div.affiliation-type-info";

            Browsers.ForEach(browser =>
            {
                // ensure that the element exists
                var affiliationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    string.Format("Affiliation info message could not found using @Browser."));

                // verify the element content
                browser.WaitUntil(b => affiliationElement.Text.Equals(affiliationInfo),
                    string.Format("Expected affiliation info message '{0}' was not displayed in @Browser.",
                        affiliationInfo));
            });
        }

        private static string TranslateRadioButtonLabelToRadioButtonId(string affiliationType)
        {
            var radioButtonId = "EmployeeOrStudentAffiliation_{0}";
            switch (affiliationType)
            {
                case "I am an employee.":
                    radioButtonId = string.Format(radioButtonId, EmployeeOrStudentAffiliate.EmployeeOnly);
                    break;

                case "I am a student.":
                    radioButtonId = string.Format(radioButtonId, EmployeeOrStudentAffiliate.StudentOnly);
                    break;

                case "I am both an employee and a student.":
                    radioButtonId = string.Format(radioButtonId, EmployeeOrStudentAffiliate.Both);
                    break;

                case "I am neither an employee nor a student.":
                    radioButtonId = string.Format(radioButtonId, EmployeeOrStudentAffiliate.Neither);
                    break;
            }
            return radioButtonId;
        }

        private static string TranslateCheckBoxLabelToCheckBoxId(string affiliationInfo)
        {
            var checkBoxId = "IsClaiming{0}";
            switch (affiliationInfo)
            {
                case "I am employed in the international affairs office.":
                    checkBoxId = string.Format(checkBoxId, "InternationalOffice");
                    break;

                case "I am an administrator.":
                    checkBoxId = string.Format(checkBoxId, "Administrator");
                    break;

                case "I am a faculty member.":
                    checkBoxId = string.Format(checkBoxId, "Faculty");
                    break;

                case "I am a staff employee.":
                    checkBoxId = string.Format(checkBoxId, "Staff");
                    break;
            }
            return checkBoxId;
        }

        #endregion
        #region Change Email Spelling

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box on the Change Email Spelling form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Change Email Spelling form")]
        [Then(@"I should type ""(.*)"" into the ""(.*)"" text box on the Change Email Spelling form")]
        public void TypeTextIntoTheTextBox(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on the Change Email Spelling form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        [Given(@"I have clicked the ""Cancel"" button on the Change Email Spelling form")]
        [When(@"I click the ""Cancel"" button on the Change Email Spelling form")]
        [Then(@"I should click the ""Cancel"" button on the Change Email Spelling form")]
        public void ClickTheCancelButton()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(".cancel-form")),
                    string.Format("Cancel button on the Change Email Spelling form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }

        [Given(@"I have clicked the ""Save Changes"" button on the Change Email Spelling form")]
        [When(@"I click the ""Save Changes"" button on the Change Email Spelling form")]
        [Then(@"I should click the ""Save Changes"" button on the Change Email Spelling form")]
        public void ClickTheSaveChangesButtonOnTheChangeEmailSpellingForm()
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the save changes submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.Id("email_respell_submit_button")),
                    string.Format("Email Respell submit button on the Change Email Spelling form could not be found using @Browser."));

                // click the sign in submit button
                button.ClickButton();
            });
        }

        [Given(@"I have seen the error message ""(.*)"" for the ""(.*)"" text box on the Change Email Spelling form")]
        [When(@"I see the error message ""(.*)"" for the ""(.*)"" text box on the Change Email Spelling form")]
        [Then(@"I should see the error message ""(.*)"" for the ""(.*)"" text box on the Change Email Spelling form")]
        public void SeeTheErrorMessageForTheInputElement(string expectedErrorMessage, string inputElementId)
        {
            // if there is no expectedErrorMessage, we do not expect to see an error for this input element
            var shouldSee = (!string.IsNullOrWhiteSpace(expectedErrorMessage));

            // define the CSS Selector
            const string cssSelector = ".email-respell-editor span.field-validation-error";

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the validation DOM element is located
                    var validationElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                        string.Format("Validation element for the '{0}' input on the Change Email Spelling form could not be found using @Browser.",
                            inputElementId));

                    // verify the validation error message
                    browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(expectedErrorMessage), string.Format(
                        "Expected error message '{1}' was not displayed for the '{0}' input on the Change Email Spelling form using @Browser. " +
                            "(Actual error message was '{2}'.)", inputElementId, expectedErrorMessage, validationElement.Text));
                }
                else
                {
                    // ensure that the validation DOM element is not located, not visible, or contains empty text
                    var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                    if (validationElement != null)
                    {
                        browser.WaitUntil(b => !validationElement.Displayed || !validationElement.Text.Trim().Equals(string.Empty), string.Format(
                            "Validation element for the '{0}' element on the Change Email Spelling form was unexpectedly displayed using @Browser. "
                                + "(Actual message was '{0}', but nothing was expected.)", inputElementId));
                    }
                }
            });
        }

        [Given(@"I have seen the email address ""(.*)"" in the My Email Addresses form")]
        [When(@"I see the email address ""(.*)"" in the My Email Addresses form")]
        [Then(@"I should see the email address ""(.*)"" in the My Email Addresses form")]
        public void SeeEmailAddressInTheMyEmailAddressesForm(string emailAddress)
        {
            const string cssSelector = ".email-address .email-value";
            Browsers.ForEach(browser =>
            {
                // ensure the element exists
                var emailElement = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Email address text could not be found using @Browser."));

                // ensure the element is visible
                browser.WaitUntil(b => emailElement.Displayed, string.Format(
                    "Email address text was not displayed using @Browser."));

                // ensure the text matches what we expect
                browser.WaitUntil(b => emailAddress.Equals(emailElement.Text), string.Format(
                    "Email address text '{0}' was expected, but '{1}' was displayed instead using @Browser.", 
                        emailAddress, emailElement));
            });
        }

        #endregion
    }
    // ReSharper restore UnusedMember.Global
}

