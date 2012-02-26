using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    [Binding]
    // ReSharper disable UnusedMember.Global
    public class ConfigurationFormsSteps : StepDefinitionBase
    {
        [Given(@"I have clicked the ""(.*)"" submit button in the Institutional Agreement Module Configuration form")]
        [When(@"I click the ""(.*)"" submit button in the Institutional Agreement Module Configuration form")]
        [Then(@"I should click the ""(.*)"" submit button in the Institutional Agreement Module Configuration form")]
        public void ClickTheSubmitButton(string buttonId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the submit button was located
                var button = browser.WaitUntil(b => b.FindElement(By.Id(buttonId)),
                    string.Format("'{0}' Submit button on the Institutional Agreement Module Configuration form could not be found using @Browser.", buttonId));

                // click the submit button
                button.ClickButton();
            });
        }

        #region Agreement types listbox

        [Given(@"I have (.*) an add link for option 1 in the Agreement types listbox")]
        [When(@"I (.*) an add link for option 1 in the Agreement types listbox")]
        [Then(@"I should (.*) an add link for option 1 in the Agreement types listbox")]
        public void SeeAnAddLinkForOption1InTheAgreementTypesListbox(string seeOrNot)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
             {
                 if (shouldSee)
                 {
                     browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")).Count > 0, 
                         "There were no items in the institutional agreement configuration agreement types listbox.");

                     // find all of the list items in the listbox
                     var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                         string.Format("The add link for option 1 could not be found on the Institutional Agreement Configuration form using @Browser."));

                     // convert the item number into an integer index
                     var itemIdex = Convert.ToInt32(1) - 1;

                     // get the expected option out of the listbox item collection
                     if (itemIdex >= allItems.Count)
                         Assert.Fail(string.Format("Failure trying to access index '{0}' when there are only '{1}' nodes.", 
                             itemIdex, allItems.Count));

                     var liElement = allItems.ToList()[itemIdex];

                     // ensure that the link is located
                     var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .add a")),
                         string.Format("The add link could not be found using the @Browser."));

                     // display the link 
                     browser.WaitUntil(b => linkElement.Displayed, string.Format("Listbox did not contain the add link"));
                 }
                 else
                 {
                     // ensure that the link element is not located, not visible, or contains empty text
                     var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions .add a"));
                     browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                         "The add link for the option 1 was unexpectedly displayed using @Browser."));
                 }
             });
        }

        [Given(@"I have clicked the add link for option 1 in the Agreement types listbox")]
        [When(@"I click the add link for option 1 in the Agreement types listbox")]
        [Then(@"I should click the add link for option 1 in the Agreement types listbox")]
        public void ClickTheAddLinkForOption1InTheAgreementTypesListbox()
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                    string.Format("The add link for option 1 could not be found on the Institutional Agreement Configuration form using @Browser."));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(1) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .add a")),
                    string.Format("The add link could not be found using the @Browser."));

                // click the add link
                linkElement.ClickLink();
            });
        }

        // For Edit 
        [Given(@"I have (.*) an Edit link for option ""(.*)"" info in the Agreement types listbox")]
        [When(@"I (.*) an Edit link for option ""(.*)"" info in the Agreement types listbox")]
        [Then(@"I should (.*) an Edit link for option ""(.*)"" info in the Agreement types listbox")]
        public void SeeAnEditLinkInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
             {
                 if (shouldSee)
                 {
                     // find all of the list items in the listbox
                     var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                         string.Format("The Edit link for the option '{0}' on the Institutional Agreement Configuration form was not displayed using @Browser.", itemNumber));

                     // convert the item number into an integer index
                     var itemIdex = Convert.ToInt32(itemNumber) - 1;

                     // get the expected option out of the listbox item collection
                     var liElement = allItems.ToList()[itemIdex];

                     // ensure that the link is located
                     var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .edit a")),
                         string.Format("The Edit link for the option '{0}' was not found using @Browser.", itemNumber));

                     // display the link
                     browser.WaitUntil(b => linkElement.Displayed, string.Format("Listbox did not contain the edit link"));
                 }
                 else
                 {
                     // ensure that the link element is not located, not visible, or contains empty text
                     var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .reader .actions"));
                     browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                         "The Edit link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                 }
             });
        }

        // for Edit 
        [Given(@"I have clicked the Edit link for option ""(.*)"" info in the Agreement types listbox")]
        [When(@"I click the Edit link for option ""(.*)"" info in the Agreement types listbox")]
        [Then(@"I should click the Edit link for option ""(.*)"" info in the Agreement types listbox")]
        public void ClickTheEditLinkForTheAgreementTypesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                    string.Format("The Edit link for the option '{0}' on the Institutional Agreement Configuration form was not displayed using @Browser.", itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .edit a")),
                    string.Format("The Edit link for the option '{0}'could not be found using the @Browser.", itemNumber));

                linkElement.ClickLink();
            });
        }

        //for Remove 
        [Given(@"I have (.*) a Remove link for option ""(.*)"" info in the Agreement types listbox")]
        [When(@"I (.*) a Remove link for option ""(.*)"" info in the Agreement types listbox")]
        [Then(@"I should (.*) a Remove link for option ""(.*)"" info in the Agreement types listbox")]
        public void SeeARemoveLinkForOptionsInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    if (itemIdex >= allItems.Count)
                        Assert.Fail(string.Format("Failure trying to access index '{0}' when there are only '{1}' nodes.",
                            itemIdex, allItems.Count));

                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .remove a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for remove
        [Given(@"I have clicked the Remove link for option ""(.*)"" info in the Agreement types listbox")]
        [When(@"I click the Remove link for option ""(.*)"" info in the Agreement types listbox")]
        [Then(@"I should click the Remove link for option ""(.*)"" info in the Agreement types listbox")]
        public void ClickTheRemoveLinkInTheAgreementTypesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .remove a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        //for Save 
        [Given(@"I have (.*) a Save link for option ""(.*)"" editor in the Agreement types listbox")]
        [When(@"I (.*) a Save link for option ""(.*)"" editor in the Agreement types listbox")]
        [Then(@"I should (.*) a Save link for option ""(.*)"" editor in the Agreement types listbox")]
        public void SeeTheSaveLinkForOptionsInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
             {
                 if (shouldSee)
                 {
                     // find all of the list items in the listbox
                     var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                         string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                            , itemNumber));

                     // convert the item number into an integer index
                     var itemIdex = Convert.ToInt32(itemNumber) - 1;

                     // get the expected option out of the listbox item collection
                     var liElement = allItems.ToList()[itemIdex];

                     // ensure that the link is located
                     var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .edit a")), string.Format(
                             "The Save link for the option could not be found uing the @Browser."));

                     // display the link
                     browser.WaitUntil(b => linkElement.Displayed,
                         string.Format("Listbox did not contain the expected link"));
                 }
                 else
                 {
                     // ensure that the link element is not located, not visible, or contains empty text
                     var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                     browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                         "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                 }
             });
        }

        //for Save
        [Given(@"I have clicked the Save link for option ""(.*)"" editor in the Agreement types listbox")]
        [When(@"I click the Save link for option ""(.*)"" editor in the Agreement types listbox")]
        [Then(@"I should click the Save link for option ""(.*)"" editor in the Agreement types listbox")]
        public void ClickASaveLinkInTheAgreementTypesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .edit a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        //for Cancel
        [Given(@"I have (.*) a Cancel link for option ""(.*)"" editor in the Agreement types listbox")]
        [When(@"I (.*) a Cancel link for option ""(.*)"" editor in the Agreement types listbox")]
        [Then(@"I should (.*) a Cancel link for option ""(.*)"" editor in the Agreement types listbox")]
        public void SeeTheCancelLinkForOptionsInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .cancel a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for cancel
        [Given(@"I have clicked the Cancel link for option ""(.*)"" editor in the Agreement types listbox")]
        [When(@"I click the Cancel link for option ""(.*)"" editor in the Agreement types listbox")]
        [Then(@"I should click the Cancel link for option ""(.*)"" editor in the Agreement types listbox")]
        public void ClickACancelLinkInTheAgreementTypesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .cancel a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        [Given(@"I have (.*) a textbox for option ""(.*)"" info in the Agreement types listbox")]
        [When(@"I (.*) a textbox for option ""(.*)"" info in the Agreement types listbox")]
        [Then(@"I should (.*) a textbox for option ""(.*)"" info in the Agreement types listbox")]
        public void SeeATextboxForOptionInfoInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader")), string.Format(
                        "A text box could not be found for item '{0}' of the Institutional Agreement Configuration forms allowed types list box.", itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed, string.Format(
                        "A text box was not displayed for item '{0}' of the Institutional Agreement Configuration form using @Browser", itemNumber));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .reader"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox for the input on the was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have (.*) a textbox for option ""(.*)"" editor in the Agreement types listbox")]
        [When(@"I (.*) a textbox for option ""(.*)"" editor in the Agreement types listbox")]
        [Then(@"I should (.*) a textbox for option ""(.*)"" editor in the Agreement types listbox")]
        public void SeeATextboxForOptionEditorInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
             {
                 if (shouldSee)
                 {
                     // find all of the list items in the listbox
                     var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")), string.Format(
                         "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                     // convert the item number into an integer index
                     var itemIdex = Convert.ToInt32(itemNumber) - 1;

                     // get the expected option out of the listbox item collection
                     var liElement = allItems.ToList()[itemIdex];

                     // find the text box withing the list item element
                     var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")), string.Format(
                         "A text box could not be found for item '{0}' of the Institutional Agreement Configuration forms allowed types list box.", itemNumber));

                     browser.WaitUntil(b => textBoxElement.Displayed, string.Format(
                         "A text box was not displayed for item '{0}' of the Institutional Agreement Configuration form using @Browser", itemNumber));
                 }
                 else
                 {
                     // ensure that the link element is not located, not visible, or contains empty text
                     var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .value input[type='text']"));
                     browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                         "Textbox for the input on the was unexpectedly displayed using @Browser."));
                 }
             });
        }

        [Given(@"I have typed ""(.*)"" into the textbox for option ""(.*)"" in the Agreement types listbox")]
        [When(@"I type ""(.*)"" into the textbox for option ""(.*)"" in the Agreement types listbox")]
        [Then(@"I should type ""(.*)"" into the textbox for option ""(.*)"" in the Agreement types listbox")]
        public void TypeIntoATextboxForOptionInTheAgreementTypesListbox(string textToType, string itemNumber)
        {
            Browsers.ForEach(browser =>
             {
                 // find all of the list items in the listbox
                 var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")), string.Format(
                     "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                 // convert the item number into an integer index
                 var itemIdex = Convert.ToInt32(itemNumber) - 1;

                 // get the expected option out of the listbox item collection
                 var liElement = allItems.ToList()[itemIdex];

                 // find the text box withing the list item element
                 var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")),
                     string.Format("A text box could not be found for option '{0}' of the Institutional Agreement Configuration forms allowed types list box", itemNumber));

                 // clear the text box and type the value
                 textBoxElement.ClearAndSendKeys(textToType);
             });
        }

        [Given(@"I have (.*) a text ""(.*)"" for option ""(.*)"" info in the Agreement types listbox")]
        [When(@"I (.*) a text ""(.*)"" for option ""(.*)"" info in the Agreement types listbox")]
        [Then(@"I should (.*) a text ""(.*)"" for option ""(.*)"" info in the Agreement types listbox")]
        public void ThenIShouldSeeTheTextTextForOptionInfoInTheAgreementTypesListbox(string seeOrNot, string expectedText, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader")),
                        string.Format("A text '{0}' could not be found for option '{1}' of the Institutional Agreement Configuration forms allowed types list box", expectedText, itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed,
                        string.Format("textbox element did not contain the expected text '{0}'", expectedText));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .reader"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) a text ""(.*)"" for option ""(.*)"" editor in the Agreement types listbox")]
        [When(@"I (.*) a text ""(.*)"" for option ""(.*)"" editor in the Agreement types listbox")]
        [Then(@"I should (.*) a text ""(.*)"" for option ""(.*)"" editor in the Agreement types listbox")]
        public void ThenIShouldSeeTheTextTextForOptionEditorInTheAgreementTypesListbox(string seeOrNot, string expectedText, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")),
                        string.Format("A text '{0}' could not be found for option '{1}' of the Institutional Agreement Configuration forms allowed types list box", expectedText, itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed,
                        string.Format("textbox element did not contain the expected text '{0}'", expectedText));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .value input[type='text']"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) the error message ""(.*)"" for option ""(.*)"" in the Agreement types listbox")]
        [When(@"I (.*) the error message ""(.*)"" for option ""(.*)"" in the Agreement types listbox")]
        [Then(@"I should (.*) the error message ""(.*)"" for option ""(.*)"" in the Agreement types listbox")]
        public void SeeTheErrorMessageTextBoxInTheAgreementTypesForm(string seeOrNot, string expectedMessage, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_types_list li")),
                        string.Format("No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the error message withing the list item element
                    var errorElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".validate .field-validation-error")),
                            string.Format("The expected message '{0}' could not be found using the @Browser.", expectedMessage));

                    browser.WaitUntil(b => errorElement.Displayed && errorElement.Text.Contains(expectedMessage),
                         string.Format("Listbox did not contain the expected message '{0}'", expectedMessage));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var errorElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .validate .field-validation-error"));
                    browser.WaitUntil(b => errorElement == null || !errorElement.Displayed, string.Format(
                        "Error element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Agreement type form")]
        [When(@"I (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Agreement type form")]
        [Then(@"I should (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Agreement type form")]
        public void SeeARadioButtonToDecideAccessToTheUsersToEnterAnyTextForAgreementType(string seeOrNot, string radioId)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the radio button was located
                    var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioId)), string.Format(
                        "A radio button labeled '{0}'does not exist on the Institutional Agreement Configuration form using @Browser.", radioId));

                    browser.WaitUntil(b => radioButton.Displayed,
                           string.Format("Agreement types listbox did not contain the expected radio button '{0}'", radioId));
                }
                else
                {
                    var radioButton = browser.TryFindElement(By.Id(radioId));
                    browser.WaitUntil(b => radioButton == null || !radioButton.Displayed, string.Format(
                      "A radio button labeled '{0}' was unexpectedly displayed on the Institutional Agreement Configuration form using @Browser.", radioId));
                }
            });
        }

        [Given(@"I have clicked the radio button ""(.*)"" to decide access to the users to enter any text for Agreement type form")]
        [When(@"I click the radio button ""(.*)"" to decide access to the users to enter any text for Agreement type form")]
        public void ClickTheRadioButtonToDecideAccessToTheUsersToEnterAnyTextForAgreementType(string radioId)
        {
            Browsers.ForEach(browser =>
             {
                 // ensure that the radio button was located
                 var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioId)), string.Format(
                     "A radio button labeled '{0}'does not exist on the Institutional Agreement Configuration form using @Browser.", radioId));

                 // click the radio button
                 radioButton.ClickRadioButton();
             });
        }

        [Given(@"I have (.*) ""(.*)"" an autocomplete dropdown menu item ""(.*)"" in the Agreement types form")]
        public void SeeAnAutoCompleteDropDownMenuItems(string seeOrNot, string fieldId, string expectedText)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "seen");
            var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find dropdown menu
                    var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));

                    browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                    browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have visible list item '{1}' using @Browser.",
                            fieldId, expectedText));
                }
                else if (!string.IsNullOrWhiteSpace(expectedText))
                {
                    // autocomplete menu may be displayed, but should not contain the expected text
                    var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    if (menu != null)
                    {
                        var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                            "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                                fieldId, expectedText));
                        var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                        browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                            "Autocomplete dropdown menu item '{1}' (for element id '{0}') was unexpectedly displayed using @Browser.",
                                fieldId, expectedText));
                    }
                }
                else
                {
                    // make sure there are no autocompletes displayed
                    var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    browser.WaitUntil(b => menu == null || !menu.Displayed, string.Format(
                        "Autocomplete dropdown for element id '{0}' was unexpectedly displayed using @Browser.", fieldId));
                }

            });
        }

        [Given(@"I have (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)"" in the Agreement types form")]
        public void ClickTheAutoCompleteMenuItem(string clickOrNot, string fieldId, string expectedText)
        {
            if (clickOrNot.Trim() != "click") return;
            var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                // make sure there is a visible dropdown
                var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                        fieldId, expectedText));

                browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                        fieldId, expectedText));
                var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                        fieldId, expectedText));
                var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have visible list item '{1}' using @Browser.",
                        fieldId, expectedText));

                //click the item
                item.ClickAutoCompleteItem();
            });
        }

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box in the Agreement types form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box in the Agreement types form")]
        public void TypeValueInTheNameTextBox(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on theInstitutional Agreement Module Configuration form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        [Then(@"I should see a read-only text box ""(.*)"" for Agreement type form")]
        public void ThenIShouldNotBeAbleToTypeAnythingOnTheTextBoxExampleTypeInput(string textBoxId)
        {
            var cssSelector = string.Format("div.{0}-field input[type='text']", textBoxId);

            Browsers.ForEach(browser =>
            {
                var textBox = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Text box '{0}'could not be found on the Agreement types listbox using @Browser.",
                       textBoxId));

                var readOnlyAttribute = textBox.GetAttribute("readonly");

                browser.WaitUntil(b => readOnlyAttribute.Equals("true"), string.Format(
                    "Text box '{0}' was unexpectedly not in read only mode on the Agreement types listbox using @Browser.",
                       textBoxId));
            });
        }

        #endregion
        #region Current statuses listbox

        [Given(@"I have (.*) an add link for option 1 in the Current statuses listbox")]
        [When(@"I (.*) an add link for option 1 in the Current statuses listbox")]
        [Then(@"I should (.*) an add link for option 1 in the Current statuses listbox")]
        public void SeeAnAddLinkForOption1InTheCurrentStatusesListbox(string seeOrNot)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                        string.Format("The add link for option 1 could not be found on the Institutional Agreement Configuration form using @Browser."));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(1) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .add a")),
                        string.Format("The add link could not be found using the @Browser."));

                    // display the link 
                    browser.WaitUntil(b => linkElement.Displayed, string.Format("Listbox did not contain the add link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_status_list li .editor .actions .add a"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The add link for the option 1 was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have clicked the add link for option 1 in the Current statuses listbox")]
        [When(@"I click the add link for option 1 in the Current statuses listbox")]
        [Then(@"I should click the add link for option 1 in the Current statuses listbox")]
        public void ClickTheAddLinkForOption1InTheCurrentStatusestypesListbox()
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                    string.Format("The add link for option 1 could not be found on the Institutional Agreement Configuration form using @Browser."));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(1) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .add a")),
                    string.Format("The add link could not be found using the @Browser."));

                // click the add link
                linkElement.ClickLink();
            });
        }

        // For Edit 
        [Given(@"I have (.*) an Edit link for option ""(.*)"" info in the Current statuses listbox")]
        [When(@"I (.*) an Edit link for option ""(.*)"" info in the Current statuses listbox")]
        [Then(@"I should (.*) an Edit link for option ""(.*)"" info in the Current statuses listbox")]
        public void SeeAnEditLinkInTheCurrentStatusesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                        string.Format("The Edit link for the option '{0}' on the Institutional Agreement Configuration form was not displayed using @Browser.", itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .edit a")),
                        string.Format("The Edit link for the option '{0}' was not found using @Browser.", itemNumber));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed, string.Format("Listbox did not contain the edit link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .reader .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Edit link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        // for Edit 
        [Given(@"I have clicked the Edit link for option ""(.*)"" info in the Current statuses listbox")]
        [When(@"I click the Edit link for option ""(.*)"" info in the Current statuses listbox")]
        [Then(@"I should click the Edit link for option ""(.*)"" info in the Current statuses listbox")]
        public void ClickTheEditLinkForTheOptionInTheCurrentStatusesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                    string.Format("The Edit link for the option '{0}' on the Institutional Agreement Configuration form was not displayed using @Browser.", itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .edit a")),
                    string.Format("The Edit link for the option '{0}'could not be found using the @Browser.", itemNumber));

                linkElement.ClickLink();
            });
        }

        //for Remove 
        [Given(@"I have (.*) a Remove link for option ""(.*)"" info in the Current statuses listbox")]
        [When(@"I (.*) a Remove link for option ""(.*)"" info in the Current statuses listbox")]
        [Then(@"I should (.*) a Remove link for option ""(.*)"" info in the Current statuses listbox")]
        public void SeeARemoveLinkForOptionsInTheCurrentStatusesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .remove a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for remove
        [Given(@"I have clicked the Remove link for option ""(.*)"" info in the Current statuses listbox")]
        [When(@"I click the Remove link for option ""(.*)"" info in the Current statuses listbox")]
        [Then(@"I should click the Remove link for option ""(.*)"" info in the Current statuses listbox")]
        public void ClickTheRemoveLinkForOptionInTheCurrentStatusesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .remove a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        //for Save
        [Given(@"I have (.*) a Save link for option ""(.*)"" editor in the Current statuses listbox")]
        [When(@"I (.*) a Save link for option ""(.*)"" editor in the Current statuses listbox")]
        [Then(@"I should (.*) a Save link for option ""(.*)"" editor in the Current statuses listbox")]
        public void SeeTheSaveLinkForOptionsInTheCurrentStatusesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .edit a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for Save
        [Given(@"I have clicked the Save link for option ""(.*)"" editor in the Current statuses listbox")]
        [When(@"I click the Save link for option ""(.*)"" editor in the Current statuses listbox")]
        [Then(@"I should click the Save link for option ""(.*)"" editor in the Current statuses listbox")]
        public void ClickASaveLinkInTheCurrentStatusesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .edit a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        //for Cancel
        [Given(@"I have (.*) a Cancel link for option ""(.*)"" editor in the Current statuses listbox")]
        [When(@"I (.*) a Cancel link for option ""(.*)"" editor in the Current statuses listbox")]
        [Then(@"I should (.*) a Cancel link for option ""(.*)"" editor in the Current statuses listbox")]
        public void SeeACancelLinkForOptionsInTheCurrentStatusesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .cancel a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions .cancel a"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for cancel
        [Given(@"I have clicked the Cancel link for option ""(.*)"" editor in the Current statuses listbox")]
        [When(@"I click the Cancel link for option ""(.*)"" editor in the Current statuses listbox")]
        [Then(@"I should click the Cancel link for option ""(.*)"" editor in the Current statuses listbox")]
        public void ClickTheCancelLinkInTheCurrentStatusesListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .cancel a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        [Given(@"I have (.*) a textbox for option ""(.*)"" editor in the Current statuses listbox")]
        [When(@"I (.*) a textbox for option ""(.*)"" editor in the Current statuses listbox")]
        [Then(@"I should (.*) a textbox for option ""(.*)"" editor in the Current statuses listbox")]
        public void SeeATextboxForOptionEditorInTheCurrentStatusesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")), string.Format(
                        "A text box could not be found for item '{0}' of the Institutional Agreement Configuration forms allowed types list box.", itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed, string.Format(
                        "A text box was not displayed for item '{0}' of the Institutional Agreement Configuration form using @Browser", itemNumber));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_status_list li .editor .value input[type='text']"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox for the input on the was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have (.*) a textbox for option ""(.*)"" info in the Current statuses listbox")]
        [When(@"I (.*) a textbox for option ""(.*)"" info in the Current statuses listbox")]
        [Then(@"I should (.*) a textbox for option ""(.*)"" info in the Current statuses listbox")]
        public void SeeATextboxForOptionInfoInTheCurrentStatusesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader")), string.Format(
                        "A text box could not be found for item '{0}' of the Institutional Agreement Configuration forms allowed types list box.", itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed, string.Format(
                        "A text box was not displayed for item '{0}' of the Institutional Agreement Configuration form using @Browser", itemNumber));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_status_list li .editor .value input[type='text']"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox for the input on the was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have typed ""(.*)"" into the textbox for option ""(.*)"" in the Current statuses listbox")]
        [When(@"I type ""(.*)"" into the textbox for option ""(.*)"" in the Current statuses listbox")]
        [Then(@"I should type ""(.*)"" into the textbox for option ""(.*)"" in the Current statuses listbox")]
        public void TypeIntoATextboxForOptionInTheCurrentStatusesListbox(string textToType, string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")), string.Format(
                    "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // find the text box withing the list item element
                var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")),
                    string.Format("A text box could not be found for option '{0}' of the Institutional Agreement Configuration forms allowed types list box", itemNumber));

                // clear the text box and type the value
                textBoxElement.ClearAndSendKeys(textToType);
            });
        }

        [Given(@"I have (.*) a text ""(.*)"" for option ""(.*)"" info in the Current statuses listbox")]
        [When(@"I (.*) a text ""(.*)"" for option ""(.*)"" info in the Current statuses listbox")]
        [Then(@"I should (.*) a text ""(.*)"" for option ""(.*)"" info in the Current statuses listbox")]
        public void ThenIShouldSeeTheTextTextForOptionInfoInTheCurrentStatusesListbox(string seeOrNot, string expectedText, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader")),
                        string.Format("A text '{0}' could not be found for option '{1}' of the Institutional Agreement Configuration forms allowed types list box", expectedText, itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed,
                        string.Format("textbox element did not contain the expected text '{0}'", expectedText));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_status_list li .reader"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) a text ""(.*)"" for option ""(.*)"" editor in the Current statuses listbox")]
        [When(@"I (.*) a text ""(.*)"" for option ""(.*)"" editor in the Current statuses listbox")]
        [Then(@"I should (.*) a text ""(.*)"" for option ""(.*)"" editor in the Current statuses listbox")]
        public void ThenIShouldSeeTheTextTextForOptionEditorInTheCurrentStatusesListbox(string seeOrNot, string expectedText, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")),
                        string.Format("A text '{0}' could not be found for option '{1}' of the Institutional Agreement Configuration forms allowed types list box", expectedText, itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed,
                        string.Format("textbox element did not contain the expected text '{0}'", expectedText));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_status_list li .reader"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) the error message ""(.*)"" for option ""(.*)"" in the Current statuses listbox")]
        [When(@"I (.*) the error message ""(.*)"" for option ""(.*)"" in the Current statuses listbox")]
        [Then(@"I should (.*) the error message ""(.*)"" for option ""(.*)"" in the Current statuses listbox")]
        public void SeeTheErrorMessageTextBoxInTheCurrentStatusesForm(string seeOrNot, string expectedMessage, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_status_list li")),
                        string.Format("No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the error message withing the list item element
                    var errorElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".validate .field-validation-error")),
                            string.Format("The expected message '{0}' could not be found using the @Browser.", expectedMessage));

                    browser.WaitUntil(b => errorElement.Displayed && errorElement.Text.Contains(expectedMessage),
                         string.Format("Listbox did not contain the expected message '{0}'", expectedMessage));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var errorElement = browser.TryFindElement(By.CssSelector("ul#allowed_status_list li .validate .field-validation-error"));
                    browser.WaitUntil(b => errorElement == null || !errorElement.Displayed, string.Format(
                        "Error element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Current statuses form")]
        [When(@"I (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Current statuses form")]
        [Then(@"I should (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Current statuses form")]
        public void SeeARadioButtonToDecideAccessToTheUsersToEnterAnyTextForCurrentStatuses(string seeOrNot, string radioId)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the radio button was located
                    var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioId)), string.Format(
                        "A radio button labeled '{0}'does not exist on the Institutional Agreement Configuration form using @Browser.", radioId));

                    browser.WaitUntil(b => radioButton.Displayed,
                           string.Format("Current statuses listbox did not contain the expected radio button '{0}'", radioId));
                }
                else
                {
                    var radioButton = browser.TryFindElement(By.Id(radioId));
                    browser.WaitUntil(b => radioButton == null || !radioButton.Displayed, string.Format(
                      "A radio button labeled '{0}' was unexpectedly displayed on the Institutional Agreement Configuration form using @Browser.", radioId));
                }
            });
        }

        [Given(@"I have clicked the radio button ""(.*)"" to decide access to the users to enter any text for Current statuses form")]
        [When(@"I click the radio button ""(.*)"" to decide access to the users to enter any text for Current statuses form")]
        public void ClickTheRadioButtonToDecideAccessToTheUsersToEnterAnyTextForCurrentstatuses(string radioId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the radio button was located
                var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioId)), string.Format(
                    "A radio button labeled '{0}'does not exist on the Institutional Agreement Configuration form using @Browser.", radioId));

                // click the radio button
                radioButton.ClickRadioButton();
            });
        }

        [Given(@"I have (.*) ""(.*)"" an autocomplete dropdown menu item ""(.*)"" in the Current statuses form")]
        public void SeeAnAutoCompleteDropDownMenuItemInTheCurrentStatusesForm(string seeOrNot, string fieldId, string expectedText)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "seen");
            var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find dropdown menu
                    var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));

                    browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                    browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have visible list item '{1}' using @Browser.",
                            fieldId, expectedText));
                }
                else if (!string.IsNullOrWhiteSpace(expectedText))
                {
                    // autocomplete menu may be displayed, but should not contain the expected text
                    var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    if (menu != null)
                    {
                        var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                            "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                                fieldId, expectedText));
                        var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                        browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                            "Autocomplete dropdown menu item '{1}' (for element id '{0}') was unexpectedly displayed using @Browser.",
                                fieldId, expectedText));
                    }
                }
                else
                {
                    // make sure there are no autocompletes displayed
                    var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    browser.WaitUntil(b => menu == null || !menu.Displayed, string.Format(
                        "Autocomplete dropdown for element id '{0}' was unexpectedly displayed using @Browser.", fieldId));
                }

            });
        }

        [Given(@"I have (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)"" in the Current statuses form")]
        public void ClickTheAutoCompleteMenuItemInTheCurrentStatusForm(string clickOrNot, string fieldId, string expectedText)
        {
            if (clickOrNot.Trim() != "click") return;
            var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                // make sure there is a visible dropdown
                var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                        fieldId, expectedText));

                browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                        fieldId, expectedText));
                var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                        fieldId, expectedText));
                var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have visible list item '{1}' using @Browser.",
                        fieldId, expectedText));

                //click the item
                item.ClickAutoCompleteItem();
            });
        }

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box in the Current statuses form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box in the Current statuses form")]
        public void TypeTheValueInTheNameTextBoxInTheCurrentStatuesForm(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on the Institutional Agreement Module Configuration form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        [Then(@"I should see a read-only text box ""(.*)"" for Current statuses form")]
        public void ShouldNotBeAbleToTypeAnythingOnTheTextBoxExampleTypeInputsForCurrentStatusesForm(string textBoxId)
        {
            var cssSelector = string.Format("div.{0}-field input[type='text']", textBoxId);

            Browsers.ForEach(browser =>
            {
                var textBox = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Text box '{0}'could not be found on the Current statuses listbox using @Browser.",
                       textBoxId));

                var readOnlyAttribute = textBox.GetAttribute("readonly");

                browser.WaitUntil(b => readOnlyAttribute.Equals("true"), string.Format(
                    "Text box '{0}' was unexpectedly not in read only mode on the Current statuses listbox using @Browser.",
                       textBoxId));
            });
        }

        #endregion
        #region Agreement Contact type listbox

        [Given(@"I have (.*) an add link for option 1 in the Agreement Contact type listbox")]
        [When(@"I (.*) an add link for option 1 in the Agreement Contact type listbox")]
        [Then(@"I should (.*) an add link for option 1 in the Agreement Contact type listbox")]
        public void SeeAnAddLinkForOptionInTheAgreementTypesListbox(string seeOrNot)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                        string.Format("The add link for option 1 could not be found on the Institutional Agreement Configuration form using @Browser."));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(1) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .add a")),
                        string.Format("The add link could not be found using the @Browser."));

                    // display the link 
                    browser.WaitUntil(b => linkElement.Displayed, string.Format("Listbox did not contain the add link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_contacttype_list li .editor .actions .add a"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The add link for the option 1 was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have clicked the add link for option 1 in the Agreement Contact type listbox")]
        [When(@"I click the add link for option 1 in the Agreement Contact type listbox")]
        [Then(@"I should click the add link for option 1 in the Agreement Contact type listbox")]
        public void ClickTheAddLinkForOptionInTheAgreementTypesListbox()
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                    string.Format("The add link for option 1 could not be found on the Institutional Agreement Configuration form using @Browser."));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(1) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .add a")),
                    string.Format("The add link could not be found using the @Browser."));

                // click the add link
                linkElement.ClickLink();
            });
        }

        // For Edit 
        [Given(@"I have (.*) an Edit link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [When(@"I (.*) an Edit link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [Then(@"I should (.*) an Edit link for option ""(.*)"" info in the Agreement Contact type listbox")]
        public void SeeAnEditLinkInTheAgreementContactTypeListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                        string.Format("The Edit link for the option '{0}' on the Institutional Agreement Configuration form was not displayed using @Browser.", itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .edit a")),
                        string.Format("The Edit link for the option '{0}' was not found using @Browser.", itemNumber));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed, string.Format("Listbox did not contain the edit link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .reader .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Edit link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        // for Edit 
        [Given(@"I have clicked the Edit link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [When(@"I click the Edit link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [Then(@"I should click the Edit link for option ""(.*)"" info in the Agreement Contact type listbox")]
        public void ClickTheEditLinkForTheAgreementContactTypeListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                    string.Format("The Edit link for the option '{0}' on the Institutional Agreement Configuration form was not displayed using @Browser.", itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .edit a")),
                    string.Format("The Edit link for the option '{0}'could not be found using the @Browser.", itemNumber));

                linkElement.ClickLink();
            });
        }

        //for Remove 
        [Given(@"I have (.*) a Remove link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [When(@"I (.*) a Remove link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a Remove link for option ""(.*)"" info in the Agreement Contact type listbox")]
        public void SeeARemoveLinkForOptionsInTheAgreementContactTypeListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .remove a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for remove
        [Given(@"I have clicked the Remove link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [When(@"I click the Remove link for option ""(.*)"" info in the Agreement Contact type listbox")]
        [Then(@"I should click the Remove link for option ""(.*)"" info in the Agreement Contact type listbox")]
        public void ClickTheRemoveLinkInTheAgreementContactTypeListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader .actions .remove a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        //for Save 
        [Given(@"I have (.*) a Save link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [When(@"I (.*) a Save link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a Save link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        public void SeeTheSaveLinkForOptionsInTheAgreementContactTypeListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .edit a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for Save
        [Given(@"I have clicked the Save link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [When(@"I click the Save link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [Then(@"I should click the Save link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        public void ClickASaveLinkInTheAgreementContactTypeListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .edit a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        //for Cancel
        [Given(@"I have (.*) a Cancel link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [When(@"I (.*) a Cancel link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a Cancel link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        public void SeeTheCancelLinkForOptionsInTheAgreementContactTypeListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                        string.Format("The Save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser."
                           , itemNumber));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // ensure that the link is located
                    var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .cancel a")), string.Format(
                            "The Save link for the option could not be found uing the @Browser."));

                    // display the link
                    browser.WaitUntil(b => linkElement.Displayed,
                        string.Format("Listbox did not contain the expected link"));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var linkElement = browser.TryFindElement(By.CssSelector("ul#allowed_types_list li .editor .actions .cancel a"));
                    browser.WaitUntil(b => linkElement == null || !linkElement.Displayed, string.Format(
                        "The Save link for the option '{0}' was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        //for cancel
        [Given(@"I have clicked the Cancel link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [When(@"I click the Cancel link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [Then(@"I should click the Cancel link for option ""(.*)"" editor in the Agreement Contact type listbox")]
        public void ClickACancelLinkInTheAgreementContactTypeListbox(string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                    string.Format("The save link for the option '{0}' on the Institutional Agreement Configuration form  was not displayed using @Browser.",
                       itemNumber));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // ensure that the link is located
                var linkElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .actions .cancel a")),
                    string.Format("The 'Save link could not be found using the @Browser."));

                //click the link
                linkElement.ClickLink();
            });
        }

        [Given(@"I have (.*) a textbox for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [When(@"I (.*) a textbox for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a textbox for option ""(.*)"" editor in the Agreement Contact type listbox")]
        public void SeeTextboxForOptionEditorInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")), string.Format(
                        "A text box could not be found for item '{0}' of the Institutional Agreement Configuration forms allowed types list box.", itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed, string.Format(
                        "A text box was not displayed for item '{0}' of the Institutional Agreement Configuration form using @Browser", itemNumber));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_contacttype_list li .editor .value input[type='text']"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox for the input on the was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have (.*) a textbox for option ""(.*)"" info in the Agreement Contact type listbox")]
        [When(@"I (.*) a textbox for option ""(.*)"" info in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a textbox for option ""(.*)"" info in the Agreement Contact type listbox")]
        public void SeeTextboxForOptionInfoInTheAgreementTypesListbox(string seeOrNot, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")), string.Format(
                        "A text box could not be found for item '{0}' of the Institutional Agreement Configuration forms allowed types list box.", itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed, string.Format(
                        "A text box was not displayed for item '{0}' of the Institutional Agreement Configuration form using @Browser", itemNumber));
                }
                else
                {
                    // ensure that the link element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_contacttype_list li .editor .value input[type='text']"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox for the input on the was unexpectedly displayed using @Browser."));
                }
            });
        }

        [Given(@"I have typed ""(.*)"" into the textbox for option ""(.*)"" in the Agreement Contact type listbox")]
        [When(@"I type ""(.*)"" into the textbox for option ""(.*)"" in the Agreement Contact type listbox")]
        [Then(@"I should type ""(.*)"" into the textbox for option ""(.*)"" in the Agreement Contact type listbox")]
        public void TypeIntoTextboxForOptionInTheAgreementTypesListbox(string textToType, string itemNumber)
        {
            Browsers.ForEach(browser =>
            {
                // find all of the list items in the listbox
                var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")), string.Format(
                    "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                // convert the item number into an integer index
                var itemIdex = Convert.ToInt32(itemNumber) - 1;

                // get the expected option out of the listbox item collection
                var liElement = allItems.ToList()[itemIdex];

                // find the text box withing the list item element
                var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")),
                    string.Format("A text box could not be found for option '{0}' of the Institutional Agreement Configuration forms allowed types list box", itemNumber));

                // clear the text box and type the value
                textBoxElement.ClearAndSendKeys(textToType);
            });
        }

        [Given(@"I have (.*) a text ""(.*)"" for option ""(.*)"" info in the Agreement Contact type listbox")]
        [When(@"I (.*) a text ""(.*)"" for option ""(.*)"" info in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a text ""(.*)"" for option ""(.*)"" info in the Agreement Contact type listbox")]
        public void ThenIShouldSeeTextTextForOptionInfoInTheAgreementTypesListbox(string seeOrNot, string expectedText, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".reader")),
                        string.Format("A text '{0}' could not be found for option '{1}' of the Institutional Agreement Configuration forms allowed types list box", expectedText, itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed,
                        string.Format("textbox element did not contain the expected text '{0}'", expectedText));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_contacttype_list li .reader"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) a text ""(.*)"" for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [When(@"I (.*) a text ""(.*)"" for option ""(.*)"" editor in the Agreement Contact type listbox")]
        [Then(@"I should (.*) a text ""(.*)"" for option ""(.*)"" editor in the Agreement Contact type listbox")]
        public void ThenIShouldSeeTextTextForOptionEditorInTheAgreementTypesListbox(string seeOrNot, string expectedText, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")), string.Format(
                        "No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the text box withing the list item element
                    var textBoxElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".editor .value input[type='text']")),
                        string.Format("A text '{0}' could not be found for option '{1}' of the Institutional Agreement Configuration forms allowed types list box", expectedText, itemNumber));

                    browser.WaitUntil(b => textBoxElement.Displayed,
                        string.Format("textbox element did not contain the expected text '{0}'", expectedText));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var textBoxElement = browser.TryFindElement(By.CssSelector("ul#allowed_contacttype_list li .editor .value input[type='text']"));
                    browser.WaitUntil(b => textBoxElement == null || !textBoxElement.Displayed, string.Format(
                        "Textbox element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) the error message ""(.*)"" for option ""(.*)"" in the Agreement Contact type listbox")]
        [When(@"I (.*) the error message ""(.*)"" for option ""(.*)"" in the Agreement Contact type listbox")]
        [Then(@"I should (.*) the error message ""(.*)"" for option ""(.*)"" in the Agreement Contact type listbox")]
        public void SeeErrorMessageTextBoxInTheAgreementTypesForm(string seeOrNot, string expectedMessage, string itemNumber)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find all of the list items in the listbox
                    var allItems = browser.WaitUntil(b => b.FindElements(By.CssSelector("ul#allowed_contacttype_list li")),
                        string.Format("No list items were found for the allowed types listbox on the Institutional Agreement Configuration form using @Browser"));

                    // convert the item number into an integer index
                    var itemIdex = Convert.ToInt32(itemNumber) - 1;

                    // get the expected option out of the listbox item collection
                    var liElement = allItems.ToList()[itemIdex];

                    // find the error message withing the list item element
                    var errorElement = browser.WaitUntil(b => liElement.FindElement(By.CssSelector(".validate .field-validation-error")),
                            string.Format("The expected message '{0}' could not be found using the @Browser.", expectedMessage));

                    browser.WaitUntil(b => errorElement.Displayed && errorElement.Text.Contains(expectedMessage),
                         string.Format("Listbox did not contain the expected message '{0}'", expectedMessage));
                }
                else
                {
                    // ensure that the text element is not located, not visible, or contains empty text
                    var errorElement = browser.TryFindElement(By.CssSelector("ul#allowed_contacttype_list li .validate .field-validation-error"));
                    browser.WaitUntil(b => errorElement == null || !errorElement.Displayed, string.Format(
                        "Error element for the '{0}' input of the allowed types list box was unexpectedly displayed using @Browser.", itemNumber));
                }
            });
        }

        [Given(@"I have (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Agreement Contact type form")]
        [When(@"I (.*) a radio button ""(.*)"" to decide access to the users to enter any text forAgreement Contact type form")]
        [Then(@"I should (.*) a radio button ""(.*)"" to decide access to the users to enter any text for Agreement Contact type form")]
        public void SeeARadioButtonToDecideAccessToTheUsersToEnterAnyTextForAgreementContactType(string seeOrNot, string radioId)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "seen");
            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // ensure that the radio button was located
                    var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioId)), string.Format(
                        "A radio button labeled '{0}'does not exist on the Institutional Agreement Configuration form using @Browser.", radioId));

                    browser.WaitUntil(b => radioButton.Displayed,
                           string.Format("Agreement types listbox did not contain the expected radio button '{0}'", radioId));
                }
                else
                {
                    var radioButton = browser.TryFindElement(By.Id(radioId));
                    browser.WaitUntil(b => radioButton == null || !radioButton.Displayed, string.Format(
                      "A radio button labeled '{0}' was unexpectedly displayed on the Institutional Agreement Configuration form using @Browser.", radioId));
                }
            });
        }

        [Given(@"I have clicked the radio button ""(.*)"" to decide access to the users to enter any text for Agreement Contact type form")]
        [When(@"I click the radio button ""(.*)"" to decide access to the users to enter any text for Agreement Contact type form")]
        public void ClickTheRadioButtonToDecideAccessToTheUsersToEnterAnyTextForAgreementContactType(string radioId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the radio button was located
                var radioButton = browser.WaitUntil(b => b.FindElement(By.Id(radioId)), string.Format(
                    "A radio button labeled '{0}'does not exist on the Institutional Agreement Configuration form using @Browser.", radioId));

                // click the radio button
                radioButton.ClickRadioButton();
            });
        }

        [Given(@"I have (.*) ""(.*)"" an autocomplete dropdown menu item ""(.*)"" in the Agreement Contact type form")]
        public void SeeAnAutoCompleteDropDownMenuItemsInTheAgreementContactTypeForm(string seeOrNot, string fieldId, string expectedText)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "seen");
            var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    // find dropdown menu
                    var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));

                    browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                    browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not have visible list item '{1}' using @Browser.",
                            fieldId, expectedText));
                }
                else if (!string.IsNullOrWhiteSpace(expectedText))
                {
                    // autocomplete menu may be displayed, but should not contain the expected text
                    var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    if (menu != null)
                    {
                        var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                            "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                                fieldId, expectedText));
                        var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                        browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                            "Autocomplete dropdown menu item '{1}' (for element id '{0}') was unexpectedly displayed using @Browser.",
                                fieldId, expectedText));
                    }
                }
                else
                {
                    // make sure there are no autocompletes displayed
                    var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                        "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                            fieldId, expectedText));
                    browser.WaitUntil(b => menu == null || !menu.Displayed, string.Format(
                        "Autocomplete dropdown for element id '{0}' was unexpectedly displayed using @Browser.", fieldId));
                }

            });
        }

        [Given(@"I have (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)"" in the Agreement Contact type form")]
        public void ClickAutoCompleteMenuItemInTheAgreementContactTypeForm(string clickOrNot, string fieldId, string expectedText)
        {
            if (clickOrNot.Trim() != "click") return;
            var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                // make sure there is a visible dropdown
                var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                        fieldId, expectedText));

                browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                        fieldId, expectedText));
                var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                        fieldId, expectedText));
                var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                    "Autocomplete dropdown menu for element id '{0}' does not have visible list item '{1}' using @Browser.",
                        fieldId, expectedText));

                //click the item
                item.ClickAutoCompleteItem();
            });
        }

        [Given(@"I have typed ""(.*)"" into the ""(.*)"" text box in the Agreement Contact type form")]
        [When(@"I type ""(.*)"" into the ""(.*)"" text box in the Agreement Contact type form")]
        public void TypeValuesInTheNameTextBoxInTheAgreementContactTypeForm(string textToType, string textBoxId)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the text box was located
                var textBox = browser.WaitUntil(b => b.FindElement(By.Id(textBoxId)),
                    string.Format("The '{0}' text box on theInstitutional Agreement Module Configuration form could not be found using @Browser.",
                        textBoxId));

                // clear the text box and type the value
                textBox.ClearAndSendKeys(textToType);
            });
        }

        [Then(@"I should see a read-only text box ""(.*)"" for Agreement Contact type form")]
        public void ThenIShouldNotBeAbleToTypeAnythingOnTheTextBoxExampleTypeInputsInTheAgreementContactTypeForm(string textBoxId)
        {
            var cssSelector = string.Format("div.{0}-field input[type='text']", textBoxId);

            Browsers.ForEach(browser =>
            {
                var textBox = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Text box '{0}'could not be found on the Agreement types listbox using @Browser.",
                       textBoxId));

                var readOnlyAttribute = textBox.GetAttribute("readonly");

                browser.WaitUntil(b => readOnlyAttribute.Equals("true"), string.Format(
                    "Text box '{0}' was unexpectedly not in read only mode on the Agreement Contacts type listbox using @Browser.",
                       textBoxId));
            });
        }

        #endregion
    }
    // ReSharper restore UnusedMember.Global

}

