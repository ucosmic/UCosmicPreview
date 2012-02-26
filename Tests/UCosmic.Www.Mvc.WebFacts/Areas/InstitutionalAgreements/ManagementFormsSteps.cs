using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Should;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class ManagementFormsSteps : StepDefinitionBase
    {
        #region Release / Preview 1: General Form

        [Given(@"I have (.*) ""(.*)"" into the ""(.*)"" text box on the Institutional Agreement (.*) form")]
        [When(@"I (.*) ""(.*)"" into the ""(.*)"" text box on the Institutional Agreement (.*) form")]
        [Then(@"I should (.*) ""(.*)"" into the ""(.*)"" text box on the Institutional Agreement (.*) form")]
        public void TypeIntoFormTextBox(string typeOrNot, string textToType, string fieldLabel, string addOrEdit)
        {
            // skip step if we're not typing into the text box
            if (typeOrNot != "type" && typeOrNot != "typed") return;

            // translate field name into textbox id
            var textBoxId = TranslateFormLabelToInputElementId(fieldLabel);

            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the text box was located
                                     var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                                                                     string.Format(
                                                                         "The '{0}' text box on the Institutional Agreement {1} form could not be found using @Browser.",
                                                                         fieldLabel, addOrEdit));

                                     // clear the text box and type the value
                                     textBox.ClearAndSendKeys(textToType);

                                     // trigger key events to enforce jQuery behavior in IE
                                     if (textBoxId != "Title") return;
                                     browser.ExecuteScript("$('#Title').keydown();");
                                     browser.ExecuteScript("$('#Title').keyup();");
                                     browser.ExecuteScript("$('#Title').keypress();");
                                 });
        }

        [Given(@"I have seen ""(.*)"" in the ""(.*)"" text box on the Institutional Agreements form")]
        [When(@"I see ""(.*)"" in the ""(.*)"" text box on the Institutional Agreements form")]
        [Then(@"I should see ""(.*)"" in the ""(.*)"" text box on the Institutional Agreements form")]
        public void SeeExpectedValueInFormTextBox(string expectedValue, string textBoxId)
        {
            var jQuery = string.Format("return $('#{0}').val();", textBoxId);

            Browsers.ForEach(browser =>
                                 {
                                     // use jQuery to get current value of the text box
                                     var actualValue = browser.ExecuteScript(jQuery).ToString();
                                     if (actualValue != expectedValue)
                                     {
                                         browser.WaitUntil(
                                             b => browser.ExecuteScript(jQuery).ToString().Equals(expectedValue),
                                             string.Format(
                                                 "The Institutional Agreements form '{0}' text box has value '{1}' but value '{2}' was expected in @Browser.",
                                                 textBoxId, browser.ExecuteScript(jQuery).ToString(), expectedValue));
                                     }
                                 });
        }

        [Given(
            @"I have \(or haven't\) seen ""(.*)"" in the ""(.*)"" text box because I (.*) it during the last Institutional Agreement form save"
            )]
        [When(
            @"I \(do or do not\) see ""(.*)"" in the ""(.*)"" text box because I (.*) it during the last Institutional Agreement form save"
            )]
        [Then(
            @"I should \(or shouldn't\) see ""(.*)"" in the ""(.*)"" text box because I (.*) it during the last Institutional Agreement form save"
            )]
        public void SeeExpectedValueInFormTextBoxBecauseOfLastSave(string expectedValue, string fieldLabel,
                                                                   string typeOrNot)
        {
            // skip step if didn't type into the text box
            if (typeOrNot != "type" && typeOrNot != "typed") return;

            // translate field name into textbox id
            var textBoxId = TranslateFormLabelToInputElementId(fieldLabel);
            var jQuery = string.Format("return $('#{0}').val();", textBoxId);

            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the text box was located
                                     var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                                                                     string.Format(
                                                                         "The '{0}' text box on the Institutional Agreement form could not be found using @Browser.",
                                                                         fieldLabel));

                                     // check the value
                                     browser.WaitUntil(
                                         b =>
                                         textBox.Displayed && b.ExecuteScript(jQuery).ToString().Equals(expectedValue),
                                         string.Format(
                                             "The '{0}' text box on the Institutional Agreement form did not contain expected value '{1}' using @Browser. " +
                                             "(Actual Value was '{2}')", textBoxId, expectedValue, textBox.Text));
                                 });
        }

        [Given(
            @"I have (.*) the error message ""(.*)"" for the ""(.*)"" text box on the Institutional Agreement (.*) form"
            )]
        [When(@"I (.*) the error message ""(.*)"" for the ""(.*)"" text box on the Institutional Agreement (.*) form")]
        [Then(
            @"I should (.*) the error message ""(.*)"" for the ""(.*)"" text box on the Institutional Agreement (.*) form"
            )]
        public void SeeFormErrorMessageForInputElement(string seeOrNot, string expectedErrorMessage, string fieldLabel,
                                                       string addOrEdit)
        {
            var inputElementId = TranslateFormLabelToInputElementId(fieldLabel);
            var cssSelector =
                string.Format("#institutional_agreement_editor span.field-validation-error span[for='{0}']",
                              inputElementId);
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         // ensure that the validation element was located
                                         var validationElement =
                                             browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                               string.Format(
                                                                   "Validation element for the '{0}' input on the Institutional Agreement {1} form could not be found using @Browser.",
                                                                   inputElementId, addOrEdit));

                                         browser.WaitUntil(
                                             b =>
                                             validationElement.Displayed &&
                                             validationElement.Text.Equals(expectedErrorMessage), string.Format(
                                                 "Expected error message '{0}' was not displayed for the '{1}' input on the Institutional Agreement {3} form using @Browser. " +
                                                 "(Actual error message was '{2}'.)", inputElementId,
                                                 expectedErrorMessage, validationElement.Text, addOrEdit));
                                     }
                                     else
                                     {
                                         var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                                         browser.WaitUntil(
                                             b => validationElement == null || !validationElement.Displayed,
                                             string.Format(
                                                 "Validation element for the '{0}' input on the Institutional Agreement {1} form was unexpectedly displayed using @Browser.",
                                                 inputElementId, addOrEdit));
                                     }
                                 });
        }

        [Given(@"I have (.*) the message ""(.*)"" included in (.*) error summaries")]
        [When(@"I (.*) the message ""(.*)"" included in (.*) error summaries")]
        [Then(@"I should (.*) the message ""(.*)"" included in (.*) error summaries")]
        public void SeeFormErrorMessageIncludedInSummaries(string seeOrNot, string expectedSummaryMessage,
                                                           int expectedSummaryCount)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
            {
                // ensure that there are the required number of validation summaries (1 at top, 1 at bottom)
                var errorSummaries = browser.WaitUntil(b => b.FindElements(By.CssSelector("div.validation-summary-errors")),
                    string.Format("Error summary or summaries do not exist using @Browser."));
                errorSummaries.Count.ShouldEqual(expectedSummaryCount, string.Format(
                    "The expected number of error summaries '{0}' were not displayed using {2} browser. " +
                    "(Actual number of summaries is '{1}')", expectedSummaryCount, errorSummaries.Count, browser.Name()));

                // loop over all summaries
                foreach (var errorSummary in errorSummaries)
                {
                    var summary = errorSummary;
                    browser.WaitUntil(b => summary.Displayed, string.Format(
                        "At least one error summary was not displayed using @Browser."));

                    var errorMessages = browser.WaitUntil(b => summary.FindElements(By.CssSelector("li")), string.Format(
                        "Error summary items could not be found in error summary using @Browser."));

                    // make sure the expected message is part of the summary
                    if (shouldSee && errorMessages.Any(errorMessage => errorMessage.Text.Equals(expectedSummaryMessage)))
                        continue;

                    if (!shouldSee && !errorMessages.Any(errorMessage => errorMessage.Text.Equals(expectedSummaryMessage)))
                        continue;

                    if (shouldSee)
                        Assert.Fail(string.Format("Error summary did not contain expected message '{0}' using {2} browser. " +
                            "(Actual content of validation summary was '{1}'.)",
                                expectedSummaryMessage, summary.Text, browser.Name()));

                    //if (!shouldSee)
                    Assert.Fail(string.Format("Error summary contained unexpected message '{0}' using {2} browser. " +
                        "(Actual content of validation summary was '{1}'.)",
                            expectedSummaryMessage, summary.Text, browser.Name()));
                }
            });
        }

        [Given(@"I have (.*)ed the ""Summary description"" automatic generation checkbox on the Institutional Agreement (.*) form")]
        [When( @"I (.*) the ""Summary description"" automatic generation checkbox on the Institutional Agreement (.*) form")]
        [Then(@"I should (.*) the ""Summary description"" automatic generation checkbox on the Institutional Agreement (.*) form")]
        public void CheckOrUncheckTheSummaryDescriptionAutomaticGenerationCheckBox(string checkOrUncheck, string addOrEdit)
        {
            var shouldCheck = (checkOrUncheck == "check");
            Browsers.ForEach(browser =>
            {
                var checkBox = browser.WaitUntil(b => b.FindElement(By.Id("IsTitleDerived")), string.Format(
                    "Summary description automatic generation check box does not exist on the Institutional Agreement {0} form using @Browser.",
                        addOrEdit));
    
                checkBox.CheckOrUncheckCheckBox(shouldCheck);
            });
        }

        [Given(@"I have seen the Summary description change to ""(.*)"" on the Institutional Agreement (.*) form")]
        [When(@"I see the Summary description change to ""(.*)"" on the Institutional Agreement (.*) form")]
        [Then(@"I should see the Summary description change to ""(.*)"" on the Institutional Agreement (.*) form")]
        public void SeeSummaryDescriptionChangeTo(string expectedSummaryDescription, string addOrEdit)
        {
            const string jQuery = "return $('#Title').val();";

            Browsers.ForEach(browser =>
                                 {
                                     // ReSharper disable ConvertToLambdaExpression
                                     //verify the summary description
                                     browser.WaitUntil(
                                         b =>
                                         browser.ExecuteScript(jQuery).ToString().Equals(expectedSummaryDescription),
                                         string.Format(
                                             "Summary description did not change to expected value '{0}' on the Institutional Agreement {2} form using @Browser. " +
                                             "(Actual value is '{1}'.)", expectedSummaryDescription,
                                             browser.ExecuteScript(jQuery).ToString(), addOrEdit));
                                     // ReSharper restore ConvertToLambdaExpression
                                 });
        }

        [Given(@"I have (.*) ""(.*)"" in the Participants list box on the Institutional Agreement (.*) form")]
        [When(@"I (.*) ""(.*)"" in the Participants list box on the Institutional Agreement (.*) form")]
        [Then(@"I should (.*) ""(.*)"" in the Participants list box on the Institutional Agreement (.*) form")]
        public void SeeParticipantInListBox(string seeOrNot, string expectedParticipant, string addOrEdit)
        {
            const string cssSelector = "ul#participants_list li";
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "seen");

            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         // make sure the target item exists in the list
                                         browser.WaitUntil(
                                             b =>
                                             b.FindElements(By.CssSelector(cssSelector)).SingleOrDefault(
                                                 li => li.Text.Equals(expectedParticipant)) != null,
                                             string.Format(
                                                 "Participants list box does not contain item with text '{0}' on the Institutional Agreement {1} form using {2} browser.",
                                                 expectedParticipant, addOrEdit, browser.Name()));
                                         var targetItem =
                                             browser.FindElements(By.CssSelector(cssSelector)).SingleOrDefault(
                                                 li => li.Text.Equals(expectedParticipant));

                                         // verify the participant name is displayed
                                         // ReSharper disable PossibleNullReferenceException
                                         browser.WaitUntil(
                                             b => targetItem.Displayed && targetItem.Text.Contains(expectedParticipant),
                                             string.Format(
                                             // ReSharper restore PossibleNullReferenceException
                                                 "Participants list box did not contain expected item '{0}' on the Institutional Agreement {2} form using @Browser. " +
                                             // ReSharper disable PossibleNullReferenceException
                                                 "(Actual value was '{1}'.)", expectedParticipant, targetItem.Text,
                                                 addOrEdit), 15);
                                         // ReSharper restore PossibleNullReferenceException
                                     }
                                     else
                                     {
                                         // select all LI items in the UL
                                         var participantItems =
                                             browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)),
                                                               string.Format(
                                                                   "Participants list box items do not exist on the Institutional Agreement {0} form using @Browser.",
                                                                   addOrEdit));

                                         var targetItem =
                                             participantItems.SingleOrDefault(li => li.Text.Equals(expectedParticipant));
                                         browser.WaitUntil(b => targetItem == null || !targetItem.Displayed,
                                                           string.Format(
                                                               "Participant list box item with text '{0}' was unexpectedly displayed on the Institutional Agreement {1} form using @Browser.",
                                                               expectedParticipant, addOrEdit));
                                     }
                                 });
        }

        [Given(@"I have clicked the ""(.*)"" submit button on the Institutional Agreement (.*) form")]
        [When(@"I click the ""(.*)"" submit button on the Institutional Agreement (.*) form")]
        [Then(@"I should click the ""(.*)"" submit button on the Institutional Agreement (.*) form")]
        public void ClickFormSubmitButton(string buttonLabel, string addOrEdit)
        {
            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the add agreement button was located
                                     var submitButton =
                                         browser.WaitUntil(
                                             b => b.FindElement(By.CssSelector("input#agreement_form_submit")),
                                             string.Format(
                                                 "Submit button labeled '{0}' does not exist on the institutional agreement {1} form using @Browser.",
                                                 buttonLabel, addOrEdit));

                                     // click the add agreement button
                                     submitButton.ClickButton();
                                 });
        }

        [Given(@"I have successfully submitted the Institutional Agreement (.*) form")]
        [When(@"I successfully submit the Institutional Agreement (.*) form")]
        [Then(@"I should successfully submit the Institutional Agreement (.*) form")]
        public void SubmitFormSuccessfully(string addOrEdit)
        {
            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the add agreement button was located
                                     var submitButton =
                                         browser.WaitUntil(
                                             b => b.FindElement(By.CssSelector("input#agreement_form_submit")),
                                             string.Format(
                                                 "Submit button does not exist on the institutional agreement {0} form using @Browser.",
                                                 addOrEdit));

                                     // click the add agreement button
                                     submitButton.ClickButton();

                                     //// wait for the next url
                                     //var expectedUrl = "my/institutional-agreements/v1".ToAbsoluteUrl();
                                     //browser.WaitUntil(b => b.Url.Equals(expectedUrl),
                                     //                  string.Format("@Browser did not arrive at the URL '{0}'.",
                                     //                                expectedUrl), 180); // wait extra time for uploads
                                 });
        }

        private static string TranslateFormLabelToInputElementId(string fieldLabel)
        {
            // map UI form field labels to actual textbox id's
            switch (fieldLabel)
            {
                case "Agreement type":
                    return "Type";

                case "Summary description":
                    return "Title";

                case "Start date":
                    return "StartsOn";

                case "Expiration date":
                    return "ExpiresOn";

                case "Current status":
                    return "Status";

                case "Participant search":
                    return "participant_search";

                default:
                    return fieldLabel;
            }
        }

        #endregion

        #region Release / Preview 2: File Attachments

        [Given(@"I have seen a File Attachment upload input on the Institutional Agreements (.*) form")]
        [When(@"I see a File Attachment upload input on the Institutional Agreements (.*) form")]
        [Then(@"I should see a File Attachment upload input on the Institutional Agreements (.*) form")]
        public void SeeFileAttachmentUploadInput(string addOrEdit)
        {
            const string cssSelector = "#institutional_agreement_editor ul#file_upload .file-chooser input[type='file']";

            Browsers.ForEach(browser =>
                                 {
                                     var fileUpload = browser.WaitUntil(
                                         b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                                             "A file upload element does not exist on the Institutional Agreements {0} form using @Browser.",
                                             addOrEdit));

                                     browser.WaitUntil(b => fileUpload.Displayed, string.Format(
                                         "The element at CssSelector '{0}' was not displayed in the Institutional Agreements {1} form using @Browser.",
                                         cssSelector, addOrEdit));
                                 });
        }

        [Given(@"I have selected ""(.*)"" as a File Attachment on the Institutional Agreements (.*) form")]
        [When(@"I select ""(.*)"" as a File Attachment on the Institutional Agreements (.*) form")]
        [Then(@"I should select ""(.*)"" as a File Attachment on the Institutional Agreements (.*) form")]
        public void SelectFileToAttach(string filePath, string addOrEdit)
        {
            const string cssSelector = "#institutional_agreement_editor ul#file_upload .file-chooser input[type='file']";

            Browsers.ForEach(browser =>
                                 {
                                     var fileUpload = browser.WaitUntil(
                                         b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                                             "A file upload element does not exist on the Institutional Agreements {0} form using @Browser.",
                                             addOrEdit));

                                     fileUpload.ChooseFile(filePath);
                                 });
        }

        [Given(
            @"I have (.*) an invalid extension error message for the File Attachment upload input on the Institutional Agreement (.*) form"
            )]
        [When(
            @"I (.*) an invalid extension error message for the File Attachment upload input on the Institutional Agreement (.*) form"
            )]
        [Then(
            @"I should (.*) an invalid extension error message for the File Attachment upload input on the Institutional Agreement (.*) form"
            )]
        public void SeeFileAttachmentInvalidExtensionErrorMessage(string seeOrNot, string addOrEdit)
        {
            const string expectedErrorMessage =
                "You may only upload PDF, Microsoft Office, and Open Document files with a " +
                "pdf, doc, docx, odt, xls, xlsx, ods, ppt, or pptx extension.";
            const string cssSelector =
                "#institutional_agreement_editor .file-chooser .file-ext-invalid span.field-validation-error";
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         // ensure that the validation element was located
                                         var validationElement =
                                             browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                               string.Format(
                                                                   "Extension validation element for the File Attachment upload input on the Institutional Agreement {0} form could not be found using @Browser.",
                                                                   addOrEdit));

                                         browser.WaitUntil(
                                             b =>
                                             validationElement.Displayed &&
                                             validationElement.Text.Equals(expectedErrorMessage), string.Format(
                                                 "Expected error message '{0}' was not displayed for the File Attachment upload input on the Institutional Agreement {2} form using @Browser. " +
                                                 "(Actual error message was '{1}'.)", expectedErrorMessage,
                                                 validationElement.Text, addOrEdit));
                                     }
                                     else
                                     {
                                         var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                                         browser.WaitUntil(
                                             b => validationElement == null || !validationElement.Displayed,
                                             string.Format(
                                                 "Extension validation element for the File Attachment upload input on the Institutional Agreement {0} form was unexpectedly displayed using @Browser.",
                                                 addOrEdit));
                                     }
                                 });
        }

        [Given(@"I have (.*) ""(.*)"" in the File Attachments list box on the Institutional Agreement (.*) form")]
        [When(@"I (.*) ""(.*)"" in the File Attachments list box on the Institutional Agreement (.*) form")]
        [Then(@"I should (.*) ""(.*)"" in the File Attachments list box on the Institutional Agreement (.*) form")]
        public void SeeFileAttachmentInListBox(string seeOrNot, string expectedFileName, string addOrEdit)
        {
            const string cssSelector = "ul#file_attachments li.file-attachment .file-chosen .file-name";
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
            {
                // select all LI items in the UL
                var fileItems = browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)), string.Format(
                    "File Attachments list box items do not exist on the Institutional Agreement {0} form using @Browser.",
                        addOrEdit));

                var targetItems = fileItems.Where(li => li.Text.Equals(expectedFileName));
                if (shouldSee)
                {
                    //targetItems.Count().ShouldNotEqual(0, string.Format(
                    //    "File Attachments list box does not contain item with text '{0}' on the Institutional Agreement {1} form using {2} browser.",
                    //    expectedFileName, addOrEdit, browser.Name()));
                    targetItems.Count().ShouldNotEqual(0);

                    foreach (var targetItem in targetItems)
                    {
                        targetItem.ShouldNotBeNull(string.Format(
                            "File Attachments list box does not contain item with text '{0}' on the Institutional Agreement {1} form using {2} browser.",
                                expectedFileName, addOrEdit, browser.Name()));

                        // verify the participant name is displayed
                        var item = targetItem;
                        browser.WaitUntil(b => item.Displayed && item.Text.Contains(expectedFileName), string.Format(
                            "File Attachments list box did not contain expected item '{0}' on the Institutional Agreement {2} form using @Browser. " +
                                "(Actual value was '{1}'.)", expectedFileName, targetItem.Text, addOrEdit));
                    }
                }
                else if (targetItems.Any())
                {
                    foreach (var targetItem in targetItems)
                    {
                        var item = targetItem;
                        browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                            "File Attachments list box item with text '{0}' was unexpectedly displayed on the Institutional Agreement {1} form using @Browser.",
                                expectedFileName, addOrEdit));
                    }
                }
            });
        }

        [Given(@"I have clicked the File Attachment remove icon for ""(.*)"" on the Institutional Agreements (.*) form")
        ]
        [When(@"I click the File Attachment remove icon for ""(.*)"" on the Institutional Agreements (.*) form")]
        [Then(@"I should click the File Attachment remove icon for ""(.*)"" on the Institutional Agreements (.*) form")]
        public void ClickFileAttachmentRemoveIcon(string fileName, string addOrEdit)
        {
            var cssSelector = string.Format(
                @"ul#file_attachments li.file-attachment .file-chosen a.remove-button[data-file-name=""{0}""]", fileName);
            Browsers.ForEach(browser =>
                                 {
                                     var link = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                                  string.Format(
                                                                      "File Attachment remove icon for the '{0}' file could not be found on the Institutional Agreement {1} form using @Browser",
                                                                      fileName, addOrEdit));

                                     browser.WaitUntil(b => link.Displayed, string.Format(
                                         "File Attachment remove icon for the '{0}' file was not displayed on the Institutional Agreement {1} form using @Browser",
                                         fileName, addOrEdit));

                                     link.ClickLink();
                                 });
        }

        #endregion

        #region Release / Preview 3: Add Contact Modal Dialog

        [Given(@"I have (.*) the Add Contact modal dialog on the institutional agreement (.*) form")]
        [When(@"I (.*) the Add Contact modal dialog on the institutional agreement (.*) form")]
        [Then(@"I should (.*) the Add Contact modal dialog on the institutional agreement (.*) form")]
        public void SeeAddContactModalDialog(string seeOrNot, string addOrEdit)
        {
            const string cssSelector = "#simplemodal-container";
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         var contactDiv =
                                             browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                               string.Format(
                                                                   "The Add Contact modal dialog on the Institutional Agreements {0} form could not be found using @Browser.",
                                                                   addOrEdit));

                                         browser.WaitUntil(b => contactDiv.Displayed, string.Format(
                                             "The Add Contact modal dialog was not displayed on the Institutional Agreements {0} form using @Browser.",
                                             addOrEdit));
                                     }
                                     else
                                     {
                                         browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)) == null,
                                                           string.Format(
                                                               "The Add Contact modal dialog was unexpectedly displayed on the Institutional Agreements {0} form using @Browser.",
                                                               addOrEdit));
                                     }
                                 });
        }

        [Given(@"I have (.*) a text box for ""(.*)"" on the Add Contact modal dialog")]
        [When(@"I (.*) a text box for ""(.*)"" on the Add Contact modal dialog")]
        [Then(@"I should (.*) a text box for ""(.*)"" on the Add Contact modal dialog")]
        public void SeeAddContactModalDialogTextBox(string seeOrNot, string fieldLabel)
        {
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var cssSelector = string.Format(".{0}-field input[type='text']", textBoxId);
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         // ensure that the element exists
                                         var textBox = browser.WaitUntil(
                                             b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                                                 "Text box '{0}' could not be found on the Add Contact modal dialog using @Browser.",
                                                 textBoxId));

                                         browser.WaitUntil(b => textBox.Displayed, string.Format(
                                             "Text box '{0}' was not displayed on the Add Contact modal dialog using @Browser. ",
                                             textBoxId));
                                     }
                                     else
                                     {
                                         var textBox = browser.TryFindElement(By.CssSelector(cssSelector));
                                         browser.WaitUntil(b => textBox == null || !textBox.Displayed, string.Format(
                                             "Text box '{0}' was unexpectedly displayed on the Add Contact modal dialog using @Browser.",
                                             textBoxId));
                                     }
                                 });
        }

        [Given(@"I have seen a read only text box for ""(.*)"" on the Add Contact modal dialog")]
        [When(@"I see a read only text box for ""(.*)"" on the Add Contact modal dialog")]
        [Then(@"I should see a read only text box for ""(.*)"" on the Add Contact modal dialog")]
        public void SeeAddContactModalDialogReadOnlyTextBox(string fieldLabel)
        {
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var cssSelector = string.Format("div.{0}-field input[type='text']", textBoxId);

            Browsers.ForEach(browser =>
                                 {
                                     var textBox = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                                     string.Format(
                                                                         "Text box '{0}' (element id '{1}') could not be found on the Add Contact modal dialog using @Browser.",
                                                                         fieldLabel, textBoxId));

                                     var readOnlyAttribute = textBox.GetAttribute("readonly");

                                     browser.WaitUntil(b => readOnlyAttribute.Equals("true"), string.Format(
                                         "Text box '{0}' (element id '{1}') was unexpectedly not in read only mode on the Add Contact modal dialog using @Browser.",
                                         fieldLabel, textBoxId));
                                 });
        }

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box on the Add Contact modal dialog")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Add Contact modal dialog")]
        [When(@"I should type ""(.*)"" into the ""(.*)"" text box on the Add Contact modal dialog")]
        public void TypeIntoAddContactModalDialogTextBox(string textToType, string fieldLabel)
        {
            // translate field name into textbox id
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var cssSelector = string.Format("#simplemodal-container div.{0}-field input[type='text']", textBoxId);

            Browsers.ForEach(browser =>
                                 {
                                     var textBox = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                                     string.Format(
                                                                         "The text box {0} DOM element could not be found using @Browser.",
                                                                         cssSelector));

                                     textBox.ClearAndSendKeys(textToType, "#simplemodal-container ");
                                 });
        }

        [Given(@"I have seen ""(.*)"" in the ""(.*)"" text box on the Add Contact modal dialog")]
        [When(@"I see ""(.*)"" in the ""(.*)"" text box on the Add Contact modal dialog")]
        [Then(@"I should see ""(.*)"" in the ""(.*)"" text box on the Add Contact modal dialog")]
        public void SeeExpectedValueInAddContactModalDialogTextBox(string expectedValue, string fieldLabel)
        {
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var jQuery = string.Format(@"return $("".{0}-field input[type='text']"").val();", textBoxId);

            Browsers.ForEach(browser =>
                                 {
                                     // use jQuery to get current value of the text box
                                     var actualValue = browser.ExecuteScript(jQuery).ToString();
                                     if (actualValue != expectedValue)
                                     {
                                         browser.WaitUntil(
                                             b => browser.ExecuteScript(jQuery).ToString().Equals(expectedValue),
                                             string.Format(
                                                 "The Add Contact modal dialog '{0}' text box has value '{1}' but value '{2}' was expected in @Browser.",
                                                 textBoxId, browser.ExecuteScript(jQuery).ToString(), expectedValue));
                                     }
                                 });
        }

        [Given(@"I have (.*) the error message ""(.*)"" for the ""(.*)"" text box on the Add Contact modal dialog")]
        [When(@"I (.*) the error message ""(.*)"" for the ""(.*)"" text box on the Add Contact modal dialog")]
        [Then(@"I should (.*) the error message ""(.*)"" for the ""(.*)"" text box on the Add Contact modal dialog")]
        public void SeeAddContactModalDialogErrorMessageForInputLabeled(string seeOrNot, string expectedErrorMessage,
                                                                        string fieldLabel)
        {

            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var cssSelector =
                string.Format("#simplemodal-container #modal_box div.{0}-field span.field-validation-error span",
                              textBoxId);

            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         // ensure that the validation DOM element was located
                                         var validationElement =
                                             browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                               string.Format(
                                                                   "Validation element for the '{0}' input on the Add Contact modal dialog could not be found using @Browser.",
                                                                   textBoxId));

                                         browser.WaitUntil(
                                             b =>
                                             validationElement.Displayed &&
                                             validationElement.Text.Equals(expectedErrorMessage), string.Format(
                                                 "Expected error message '{1}' was not displayed for the '{0}' input on the Add Contact modal dialog using @Browser. " +
                                                 "(Actual error message was '{2}'.)", textBoxId, expectedErrorMessage,
                                                 validationElement.Text));
                                     }
                                     else
                                     {
                                         var validationElement = browser.TryFindElement(By.CssSelector(cssSelector));
                                         browser.WaitUntil(
                                             b => validationElement == null || !validationElement.Displayed,
                                             string.Format(
                                                 "Validation element for the '{0}' input on the Add Contact modal dialog was unexpectedly displayed using @Browser." +
                                                 "(Actual error message was '{1}'.)", textBoxId, expectedErrorMessage));
                                     }
                                 });
        }

        [Given(
            @"I have clicked the autocomplete dropdown arrow button for the ""(.*)"" text box on the Add Contact modal dialog"
            )]
        [When(
            @"I click the autocomplete dropdown arrow button for the ""(.*)"" text box on the Add Contact modal dialog")
        ]
        [Then(
            @"I should click the autocomplete dropdown arrow button for the ""(.*)"" text box on the Add Contact modal dialog"
            )]
        public void ClickAddContactModalDialogAutocompleteDropdownArrowButton(string fieldLabel)
        {
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var commonSteps = new CommonSteps();
            commonSteps.ClickAutoCompleteDropDownArrowButton(textBoxId);
        }

        [Given(@"I have (.*) a ""(.*)"" autocomplete dropdown menu item ""(.*)"" on the Add Contact modal dialog")]
        [When(@"I (.*) a ""(.*)"" autocomplete dropdown menu item ""(.*)"" on the Add Contact modal dialog")]
        [Then(@"I should (.*) a ""(.*)"" autocomplete dropdown menu item ""(.*)"" on the Add Contact modal dialog")]
        public void SeeAddContactModalDialogAutoCompleteDropDownMenuItem(string seeOrNot, string fieldLabel,
                                                                         string expectedText)
        {
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var commonSteps = new CommonSteps();
            commonSteps.SeeAutoCompleteDropDownMenuItem(seeOrNot, textBoxId, expectedText);
        }

        [Given(@"I have (.*)ed the ""(.*)"" autocomplete dropdown menu item ""(.*)"" on the Add Contact modal dialog")]
        [When(@"I (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)"" on the Add Contact modal dialog")]
        [Then(@"I should (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)"" on the Add Contact modal dialog")]
        public void ClickAddContactModalDialogAutoCompleteMenuItem(string clickOrNot, string fieldLabel,
                                                                   string expectedText)
        {
            var textBoxId = TranslateAddContactModalDialogLabelToElementId(fieldLabel);
            var commonSteps = new CommonSteps();
            commonSteps.ClickAutoCompleteMenuItem(clickOrNot, textBoxId, expectedText);
        }

        [Given(@"I have clicked the ""(.*)"" button on the Add Contact modal dialog")]
        [When(@"I click the ""(.*)"" button on the Add Contact modal dialog")]
        [Then(@"I should click the ""(.*)"" button on the Add Contact modal dialog")]
        public void ClickAddContactModalDialogButton(string buttonLabel)
        {
            var buttonId = TranslateAddContactModalDialogLabelToElementId(buttonLabel);
            var cssSelector = string.Format("#simplemodal-container input#{0}", buttonId);
            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the button is located
                                     var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                                    string.Format(
                                                                        "Button labeled '{0}' (element id '{1}') does not exist on the Institutional Agreement form Add Contact modal dialog using @Browser.",
                                                                        buttonLabel, buttonId));

                                     // click the add agreement button
                                     button.ClickButton();
                                 });
        }

        private static string TranslateAddContactModalDialogLabelToElementId(string fieldLabel)
        {
            switch (fieldLabel)
            {
                case "Contact type":
                    return "ContactType";

                case "First name":
                    return "Person_FirstName";

                case "Last name":
                    return "Person_LastName";

                case "Email address":
                    return "Person_DefaultEmail";

                case "Salutation":
                    return "Person_Salutation";

                case "Middle name or initial":
                    return "Person_MiddleName";

                case "Suffix":
                    return "Person_Suffix";

                case "Add Contact":
                    return "contact_add_next";

                case "Cancel":
                    return "contact_add_cancel";

                default:
                    return fieldLabel;
            }
        }

        [Given(@"I have (.*) ""(.*)"" in the Contacts list box on the Institutional Agreement (.*) form")]
        [When(@" I (.*) ""(.*)"" in the Contacts list box on the Institutional Agreement (.*) form")]
        [Then(@"I should (.*) ""(.*)"" in the Contacts list box on the Institutional Agreement (.*) form")]
        public void SeeContactInListBox(string seeOrNot, string expectedContactName, string addOrEdit)
        {
            const string cssSelector = "ul#contacts_list li.agreement-contact .type-and-name";
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
            {
                // select all LI items in the UL
                var contactItems = browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)), string.Format(
                    "Contacts list box items do not exist on the Institutional Agreement {0} form using @Browser.",
                        addOrEdit));
                var targetItems = contactItems.Where(li => li.Text.Equals(expectedContactName));
                if (shouldSee)
                {
                    //targetItems.Count().ShouldNotEqual(0, string.Format(
                    //    "Contacts list box does not contain item with text '{0}' on the Institutional Agreement {1} form using {2} browser.",
                    //    expectedContactName, addOrEdit, browser.Name()));
                    targetItems.Count().ShouldNotEqual(0);

                    foreach (var targetItem in targetItems)
                    {
                        targetItem.ShouldNotBeNull(string.Format(
                            "Contacts list box does not contain item with text '{0}' on the Institutional Agreement {1} form using {2} browser.",
                                expectedContactName, addOrEdit, browser.Name()));

                        // verify the participant name is displayed
                        var item = targetItem;
                        browser.WaitUntil(b => item.Displayed && item.Text.Contains(expectedContactName), string.Format(
                            "Contacts list box did not contain expected item '{0}' on the Institutional Agreement {2} form using @Browser. " +
                                "(Actual value was '{1}'.)", expectedContactName, targetItem.Text, addOrEdit));
                    }
                }
                else if (targetItems.Any())
                {
                    foreach (var targetItem in targetItems)
                    {
                        var item = targetItem;
                        browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                            "Contacts list box item with text '{0}' was unexpectedly displayed on the Institutional Agreement {1} form using @Browser.",
                            expectedContactName, addOrEdit));
                    }
                }
            });
        }

        [Given(@"I have clicked the Contacts remove icon for ""(.*)"" on the Institutional Agreements (.*) form")]
        [When(@"I click the Contacts remove icon for ""(.*)"" on the Institutional Agreements (.*) form")]
        [Then(@"I should click the Contacts remove icon for ""(.*)"" on the Institutional Agreements (.*) form")]
        public void ClickContactsRemoveIcon(string contactName, string addOrEdit)
        {
            var cssSelector = string.Format(
                @"ul#contacts_list li.agreement-contact a.remove-button[data-contact-type-and-display-name=""{0}""]",
                contactName);

            Browsers.ForEach(browser =>
                                 {
                                     var link = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                                  string.Format(
                                                                      "Contacts remove icon for the '{0}' file could not be found on the Institutional Agreement {1} form using @Browser",
                                                                      contactName, addOrEdit));

                                     browser.WaitUntil(b => link.Displayed,
                                                       string.Format(
                                                           "Contacts remove icon for the '{0}' file was not displayed on the Institutional Agreement {1} form using @Browser",
                                                           contactName, addOrEdit));

                                     link.ClickLink();
                                 });
        }

        #endregion

        #region Release / Preview 4: Institutional Agreements Authorizations

        [Given(@"I have (.*) ""(.*)"" in the Members list box on the Edit Institutional Agreement Authorization form")]
        [When(@"I (.*) ""(.*)"" in the Members list box on the Edit Institutional Agreement Authorization form")]
        [Then(@"I should (.*) ""(.*)"" in the Members list box on the Edit Institutional Agreement Authorization form")]
        public void SeeMemberInListBox(string seeOrNot, string expectedMember)
        {
            const string cssSelector = "ul#members_list li";
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "seen");

            Browsers.ForEach(browser =>
                                 {
                                     if (shouldSee)
                                     {
                                         // make sure the target item exists in the list
                                         browser.WaitUntil(
                                             b =>
                                             b.FindElements(By.CssSelector(cssSelector)).SingleOrDefault(
                                                 li => li.Text.Equals(expectedMember)) != null,
                                             string.Format(
                                                 "Member list box does not contain item with text '{0}' on the Edit Institutional Agreement Authorization form using {1} browser.",
                                                 expectedMember, browser.Name()));
                                         var targetItem =
                                             browser.FindElements(By.CssSelector(cssSelector)).SingleOrDefault(
                                                 li => li.Text.Equals(expectedMember));

                                         // verify the member name is displayed
                                         // ReSharper disable PossibleNullReferenceException
                                         browser.WaitUntil(
                                             b => targetItem.Displayed && targetItem.Text.Contains(expectedMember),
                                             string.Format(
                                             // ReSharper restore PossibleNullReferenceException
                                                 "Members list box did not contain expected item '{0}' on the Edit Institutional Agreement Authorization form using @Browser. " +
                                             // ReSharper disable PossibleNullReferenceException
                                                 "(Actual value was '{1}'.)", expectedMember, targetItem.Text), 15);
                                         // ReSharper restore PossibleNullReferenceException
                                     }
                                     else
                                     {
                                         // select all LI items in the UL
                                         var memberItems =
                                             browser.WaitUntil(b => b.FindElements(By.CssSelector(cssSelector)),
                                                               string.Format(
                                                                   "Member list box items do not exist on the Edit Institutional Agreement Authorization form using @Browser."));

                                         var targetItem =
                                             memberItems.SingleOrDefault(li => li.Text.Equals(expectedMember));
                                         browser.WaitUntil(b => targetItem == null || !targetItem.Displayed,
                                                           string.Format(
                                                               "Member list box item with text '{0}' was unexpectedly displayed on the Edit Institutional Agreement Authorization form using @Browser.",
                                                               expectedMember));
                                     }
                                 });
        }

        [Given(
            @"I have typed ""(.*)"" into the ""(.*)"" text box on the Edit Institutional Agreement Authorization form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box on the Edit Institutional Agreement Authorization form")]
        [Then(
            @"I should have typed ""(.*)"" into the ""(.*)"" text box on the Edit Institutional Agreement Authorization form"
            )]
        public void TypeValueIntoTheNameTextBox(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the text box was located
                                     var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                                                                     string.Format(
                                                                         "The '{0}' text box on the Edit Institutional Agreement Authorization form could not be found using @Browser.",
                                                                         textBoxId));

                                     // clear the text box and type the value
                                     textBox.ClearAndSendKeys(textToType);
                                 });
        }

        [Given(@"I have clicked the ""(.*)"" submit button on the Edit Institutional Agreement Authorization form")]
        [When(@"I click the ""(.*)"" submit button on the Edit Institutional Agreement Authorization form")]
        [Then(@"I should click the ""(.*)"" submit button on the Edit Institutional Agreement Authorization form")]
        public void ClickTheCancelButton(string buttonLabel)
        {
            var buttonId = TranslateAddOrEditRoleLabelToElementId(buttonLabel);
            var cssSelector = string.Format("#role_form_manage input#{0}", buttonId);
            Browsers.ForEach(browser =>
                                 {
                                     // ensure that the save changes submit button was located
                                     var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                                                                    string.Format(
                                                                        "Submit button labeled '{0}' does not exist on the Edit Institutional Agreement Authorization form could not be found using @Browser.",
                                                                        buttonLabel));

                                     // click the sign in submit button
                                     button.ClickButton();
                                 });
        }

        private static string TranslateAddOrEditRoleLabelToElementId(string fieldLabel)
        {
            switch (fieldLabel)
            {
                case "Save Changes":
                    return "save_changes_submit_button";

                case "Cancel":
                    return "cancel_submit_button";

                default:
                    return fieldLabel;
            }
        }

        #endregion

        [Given(@"I have (.*) a bubble pop up in the Institutional Agreements (.*) form")]
        [When(@"I (.*) a bubble pop up in the Institutional Agreements (.*) form")]
        [Then(@"I should (.*) a bubble pop up in the Institutional Agreements (.*) form")]
        public void SeeABubblePopUpInTheInstitutionalAgreementsAddOrEditPage(string seeOrNot, string addOrEdit)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");

            Browsers.ForEach(browser =>
             {
                 if (shouldSee)
                 {
                     var bubblePopup =
                         browser.WaitUntil(b => b.FindElement(By.CssSelector(".jquerybubblepopup .help-bubblepop")),
                             string.Format("The bubble pop up for the Help link in the Institutional Agreements {0} form could not be found using @Browser.",
                                addOrEdit));

                     browser.WaitUntil(b => bubblePopup.Displayed,
                         string.Format("The bubble pop up for the Help link was not displayed on the Institutional Agreements {0} form using @Browser.",
                            addOrEdit));
                 }
                 else
                 {
                     var bubblePopup = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(".jquerybubblepopup .help-bubblepop")),
                        string.Format("The bubble pop up for the Help link was unexpectedly displayed on the Institutional Agreements {0} form using @Browser.",
                            addOrEdit));

                     browser.WaitUntil(b => bubblePopup == null || !bubblePopup.Displayed, string.Format(
                         "The bubble pop up for the Help link was unexpectedly displayed on the Institutional Agreements {0} form using @Browser.",
                            addOrEdit));
                 }
             });
        }
    }
    // ReSharper restore UnusedMember.Global
}






