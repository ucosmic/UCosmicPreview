using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.Common
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class CommonSteps : StepDefinitionBase
    {
        #region AutoComplete & ComboBox

        [Given(@"I have clicked the autocomplete dropdown arrow for the ""(.*)"" text box")]
        [When(@"I click the autocomplete dropdown arrow for the ""(.*)"" text box")]
        [Then(@"I should click the autocomplete dropdown arrow for the ""(.*)"" text box")]
        public void ClickAutoCompleteDropDownArrow(string textBoxId)
        {
            var linkSelector = string.Format(".{0}-field .combobox a.text-box.down-arrow", textBoxId);
            var ulSelector = string.Format(".{0}-field .combobox .autocomplete-menu ul", textBoxId);
            Browsers.ForEach(browser =>
            {
                // make sure the element exists
                var link = browser.WaitUntil(b => b.FindElement(By.CssSelector(linkSelector)), string.Format(
                    "Autocomplete dropdown button for the '{0}' text box does not exist using @Browser", textBoxId));

                // make sure the element is visible
                browser.WaitUntil(b => link.Displayed, string.Format(
                    "Autocomplete dropdown button for the '{0}' text box was not displayed using @Browser", textBoxId));

                var ulIsHidden = true;
                var retryCount = 0;
                while (ulIsHidden)
                {
                    link.Click();
                    200.WaitThisManyMilleseconds();
                    var ul = browser.TryFindElement(By.CssSelector(ulSelector));
                    if (ul != null && ul.Displayed) ulIsHidden = false;
                    else if (retryCount++ <= 3) break;
                }
            });
        }

        [Given(@"I have clicked the autocomplete dropdown arrow button for the ""(.*)"" text box")]
        [When(@"I click the autocomplete dropdown arrow button for the ""(.*)"" text box")]
        [Then(@"I should click the autocomplete dropdown arrow button for the ""(.*)"" text box")]
        public void ClickAutoCompleteDropDownArrowButton(string textBoxId)
        {
            var cssSelector = string.Format(".{0}-field button", textBoxId);
            Browsers.ForEach(browser =>
            {
                // make sure the element exists
                var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "Autocomplete dropdown button for the '{0}' text box does not exist using @Browser", textBoxId));

                // make sure the element is visible
                browser.WaitUntil(b => button.Displayed, string.Format(
                    "Autocomplete dropdown button for the '{0}' text box was not displayed using @Browser", textBoxId));

                button.ClickButton();
            });
        }

        [Given(@"I have (.*) a ""(.*)"" autocomplete dropdown menu item ""(.*)""")]
        [When(@"I (.*) a ""(.*)"" autocomplete dropdown menu item ""(.*)""")]
        [Then(@"I should (.*) a ""(.*)"" autocomplete dropdown menu item ""(.*)""")]
        public void SeeAutoCompleteDropDownMenuItem(string seeOrNot, string fieldId, string expectedText)
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

        [Given(@"I have (.*)ed the ""(.*)"" autocomplete dropdown menu item ""(.*)""")]
        [When(@"I (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)""")]
        [Then(@"I should (.*) the ""(.*)"" autocomplete dropdown menu item ""(.*)""")]
        public void ClickAutoCompleteMenuItem(string clickOrNot, string fieldId, string expectedText)
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

        [Given(@"I have dismissed all autocomplete dropdowns")]
        [When(@"I dismiss all autocomplete dropdowns")]
        [Then(@"I should dismiss all autocomplete dropdowns")]
        public void DismissAllAutoCompleteDropDowns()
        {
            const string jQuery = "$('ul.ui-autocomplete').css({display:'none'});";
            Browsers.ForEach(browser => browser.ExecuteScript(jQuery));
        }

        #endregion
        #region Modal Dialog (simplemodal)

        [Given(@"I have clicked the (.*) modal dialog close icon")]
        [When(@"I click the (.*) modal dialog close icon")]
        [Then(@"I should click the (.*) modal dialog close icon")]
        public void ClickTheModalDialogCloseIcon(string whichModalDialog)
        {
            const string cssSelector = "#simplemodal-container a.modalCloseImg";
            Browsers.ForEach(browser =>
            {
                var link = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                    "The {0} modal dialog close icon could not be found using @Browser.", whichModalDialog));

                browser.WaitUntil(b => link.Displayed, string.Format(
                    "The {0} modal dialog close icon was not displayed using @Browser.", whichModalDialog));

                link.Click();
                if (!browser.IsInternetExplorer()) return;
                try
                {
                    link.Click();
                }
                catch(WebDriverException)
                {
                }
            });
        }

        #endregion
        #region Working with Forms

        [Given(@"I have seen top feedback message ""(.*)""")]
        [When(@"I see top feedback message ""(.*)""")]
        [Then(@"I should see top feedback message ""(.*)""")]
        public void SeeSuccessFeedbackMessage(string topMessage)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the element was located
                var validationElement = browser.WaitUntil(b => b.FindElement(By.Id("feedback_flash")),
                    string.Format("Top success message element does not exist using @Browser."));

                // ensure the element is displayed
                browser.WaitUntil(b => validationElement.Displayed,
                    string.Format("Top success message element was not displayed using @Browser."));

                // verify the success message
                browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(topMessage),
                    string.Format("Top success message '{0}' was not displayed using @Browser. " +
                        "(Actual message was '{1}'.)", topMessage, validationElement.Text));
            });
        }

        //public static void SeeSuccessFeedbackMessage(IWebDriver browser, string topMessage)

        #endregion
    }
    // ReSharper restore UnusedMember.Global
}
