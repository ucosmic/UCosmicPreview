using System.Linq;
using Should;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class CollectionSteps : BaseStepDefinition
    {
        [Given(@"I (.*) an item for ""(.*)"" in the (.*) list")]
        [When(@"I (.*) an item for ""(.*)"" in the (.*) list")]
        [Then(@"I should (.*) an item for ""(.*)"" in the (.*) list")]
        public void SeeItemInListBox(string seeOrNot, string expectedValue, string fieldLabel)
        {
            var shouldSee = (seeOrNot.Trim() == "see" || seeOrNot.Trim() == "saw");

            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var fileItems = page.GetCollectionItems(fieldLabel);

                var targetItems = fileItems.Where(li => li.Text.Equals(expectedValue));
                if (shouldSee)
                {
                    targetItems.Count().ShouldBeInRange(1, int.MaxValue);

                    foreach (var targetItem in targetItems)
                    {
                        targetItem.ShouldNotBeNull(string.Format(
                            "The '{0}' list does not contain item with text '{1}' in {2}.",
                                fieldLabel, expectedValue, browser.Name()));

                        // verify the participant name is displayed
                        var item = targetItem;
                        browser.WaitUntil(b => item.Displayed && item.Text.Contains(expectedValue), string.Format(
                            "The expected item '{0}' was not displayed in the '{1}' list on @Browser. " +
                                "(Actual value was '{2}'.)", expectedValue, fieldLabel, item.Text));
                    }
                }
                else if (targetItems.Any())
                {
                    foreach (var targetItem in targetItems)
                    {
                        var item = targetItem;
                        browser.WaitUntil(b => item == null || !item.Displayed, string.Format(
                            "An item with text '{0}' was unexpectedly displayed for the '{1}' field using @Browser.",
                                expectedValue, fieldLabel));
                    }
                }
            });
        }

        [Given(@"I clicked the remove icon for ""(.*)"" in the (.*) list")]
        [When(@"I click the remove icon for ""(.*)"" in the (.*) list")]
        [Then(@"I should click the remove icon for ""(.*)"" in the (.*) list")]
        public void ClickRemoveItem(string fileName, string fieldLabel)
        {
            var key = string.Format("RemoveItem-{0}", fileName);

            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var fileItem = page.GetCollectionItem(fieldLabel, key);
                fileItem.ClickLink();
            });
        }
    }
}
