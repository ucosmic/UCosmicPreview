using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class AutoCompleteSteps : BaseStepDefinition
    {
        [When(@"I (.*) an autocomplete dropdown menu item ""(.*)"" for the (.*) text field")]
        [Then(@"I should (.*) an autocomplete dropdown menu item ""(.*)"" for the (.*) text field")]
        public void SeeAutoCompleteMenuItem(string seeOrNot, string expectedText, string fieldLabel)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "saw");
            //var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var menu = page.GetAutoCompleteMenu(fieldLabel);

                if (shouldSee)
                {
                    // wait for the menu to become visible with items before checking for the item
                    browser.WaitUntil(b => menu.Displayed && menu.FindElements(By.TagName("li")).Any(), string.Format(
                        "Autocomplete menu for the '{0}' field is not displayed or does not have any list items in @Browser.",
                            fieldLabel));
                    var items = browser.WaitUntil(b => menu.FindElements(By.TagName("li")),
                        string.Format("Autocomplete menu for the '{0}' field does not have any items in @Browser.", 
                            fieldLabel));
                    var item = items.FirstOrDefault(i => i.Text.Equals(expectedText));
                    browser.WaitUntil(b => item != null && item.Displayed, 
                        string.Format("Autocomplete menu for the '{0}' field does not have a (visible) list item with text '{1}' in @Browser.",
                            fieldLabel, expectedText));
                }
                else if (!string.IsNullOrWhiteSpace(expectedText))
                {
                    // autocomplete menu may be displayed, but should not contain the expected text
                    browser.WaitUntil(b => menu.Displayed, string.Format(
                        "Autocomplete menu for the '{0}' field is not displayed or does not have any list items in @Browser.",
                            fieldLabel));
                    //var menu = browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)), string.Format(
                    //    "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                    //        fieldId, expectedText));
                    //if (menu != null)
                    //{
                    //    var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                    //        "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                    //            fieldId, expectedText));
                    //    var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                    //    browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                    //        "Autocomplete dropdown menu item '{1}' (for element id '{0}') was unexpectedly displayed using @Browser.",
                    //            fieldId, expectedText));
                    //}
                }
                else
                {
                    // autocomplete menu should not displayed
                    browser.WaitUntil(b => menu == null || !menu.Displayed, string.Format(
                        "Autocomplete menu for the '{0}' field was unexpectedly displayed in @Browser.", fieldLabel));
                }
            });
        }

        [When(@"I (.*) the autocomplete dropdown menu item ""(.*)"" for the (.*) text field")]
        [Then(@"I should (.*) the autocomplete dropdown menu item ""(.*)"" for the (.*) text field")]
        public void ClickAutoCompleteMenuItem(string clickOrNot, string expectedText, string fieldLabel)
        {
            if (clickOrNot.Trim() != "click") return;
            //var cssSelector = string.Format(".{0}-field .autocomplete-menu ul", fieldId);

            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var menu = page.GetAutoCompleteMenu(fieldLabel);

                // make sure there is a visible dropdown
                //var menu = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)), string.Format(
                //    "Autocomplete dropdown menu for element id '{0}' does not exist using @Browser (searching for item '{1}').",
                //        fieldId, expectedText));

                // wait for the menu to become visible with items before checking for the item
                browser.WaitUntil(b => menu.Displayed && menu.FindElements(By.TagName("li")).Any(), string.Format(
                    "Autocomplete menu for the '{0}' field is not displayed or does not have any list items in @Browser.",
                        fieldLabel));
                //browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")).Count() > 0, string.Format(
                //    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                //        fieldId, expectedText));
                var items = browser.WaitUntil(b => menu.FindElements(By.TagName("li")),
                    string.Format("Autocomplete menu for the '{0}' field does not have any items in @Browser.",
                        fieldLabel));
                var item = items.FirstOrDefault(i => i.Text.Equals(expectedText));
                browser.WaitUntil(b => item != null && item.Displayed,
                    string.Format("Autocomplete menu for the '{0}' field does not have a (visible) list item with text '{1}' in @Browser.",
                        fieldLabel, expectedText));

                //var items = browser.WaitUntil(b => menu.FindElements(By.CssSelector("li")), string.Format(
                //    "Autocomplete dropdown menu for element id '{0}' does not have any list items using @Browser (searching for item '{1}').",
                //        fieldId, expectedText));
                //var item = items.FirstOrDefault(li => li.Text.Equals(expectedText));
                browser.WaitUntil(b => item != null && item.Displayed, string.Format(
                    "Autocomplete menu for the '{0}' field does not have visible list item '{1}' in @Browser.",
                        fieldLabel, expectedText));

                //click the item
                //item.ClickAutoCompleteItem();
                item.Click();
            });
        }

        [When(@"I click the autocomplete dropdown arrow button for the (.*) text field")]
        [Then(@"I should click the autocomplete dropdown arrow button for the (.*) text field")]
        public void ClickAutoCompleteDownButton(string fieldLabel)
        {
            //var cssSelector = string.Format(".{0}-field button", textBoxId);
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var button = page.GetDownArrowButton(fieldLabel);

                // make sure the element is visible
                browser.WaitUntil(b => button.Displayed, string.Format(
                    "Down arrow button for the '{0}' text box was not displayed using @Browser", fieldLabel));

                button.Click();
            });
        }

        [When(@"I dismiss all autocomplete dropdown menus")]
        public void DismissAllAutoCompleteDropDownMenus()
        {
            const string jQuery = "$('ul.ui-autocomplete').css({display:'none'});";
            Browsers.ForEach(browser => browser.ExecuteScript(jQuery));
        }
    }
}
