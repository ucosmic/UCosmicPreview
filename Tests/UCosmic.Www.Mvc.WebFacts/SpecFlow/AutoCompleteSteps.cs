using System.Linq;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class AutoCompleteSteps : BaseStepDefinition
    {
        [When(@"I see an autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [Then(@"I should see an autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        public void SeeDropDownMenuItem(string expectedText, string fieldLabel)
        {
            var failMessage = "@Browser did not display the expected item '{0}' in the '{1}' autocomplete menu."
                .FormatWith(expectedText, fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();

                browser.WaitUntil(b => page.GetAutoCompleteMenu(fieldLabel) != null &&
                    page.GetAutoCompleteMenu(fieldLabel).Displayed &&
                    page.GetAutoCompleteMenu(fieldLabel).GetElements(ByTagNameLi).Any(ElementTextEquals(expectedText)),
                    failMessage);

                var menu = page.GetAutoCompleteMenu(fieldLabel, false);
                var item = menu.GetElements(ByTagNameLi).First(ElementTextEquals(expectedText));
                browser.WaitUntil(b => item.Displayed,
                    failMessage);
            });
        }

        [When(@"I don't see an autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [When(@"I do not see an autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [Then(@"I shouldn't see an autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [Then(@"I should not see an autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        public void DoNotSeeDropDownMenuItem(string expectedText, string fieldLabel)
        {
            var failMessage = "@Browser unexpectedly displayed item '{0}' in the '{1}' autocomplete menu."
                .FormatWith(expectedText, fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();

                browser.WaitUntil(b => page.GetAutoCompleteMenu(fieldLabel).IsNull() ||
                    !page.GetAutoCompleteMenu(fieldLabel).Displayed ||
                    !page.GetAutoCompleteMenu(fieldLabel).FindElements(ByTagNameLi).Any(ElementTextEquals(expectedText)),
                    failMessage);
            });
        }

        [When(@"I click the autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [Then(@"I should click the autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        public void ClickDropDownMenuItem(string expectedText, string fieldLabel)
        {
            SeeDropDownMenuItem(expectedText, fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var menu = page.GetAutoCompleteMenu(fieldLabel);
                var item = menu.FindElements(ByTagNameLi).First(ElementTextEquals(expectedText));
                item.ClickIt();
            });
        }

        [When(@"I don't click the autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [When(@"I do not click the autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [Then(@"I shouldn't click the autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        [Then(@"I should not click the autocomplete dropdown menu item ""(.*)"" for the (.*) combo field")]
        public void DoNotClickDropDownMenuItem(string expectedText, string fieldLabel)
        {
            // place marker step for skipping drop down menu item clicking steps in exampled scenario outlines
        }

        [When(@"I click the autocomplete dropdown arrow button for the (.*) combo field")]
        [Then(@"I should click the autocomplete dropdown arrow button for the (.*) combo field")]
        public void ClickAutoCompleteDownButton(string fieldLabel)
        {

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var button = page.GetComboBoxDownArrowButton(fieldLabel);

                // make sure the element is visible
                browser.WaitUntil(b => button.Displayed, string.Format(
                    "Down arrow button for the '{0}' field was not displayed by @Browser", fieldLabel));

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
