using System.Linq;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class BrowserSteps : BaseStepDefinition
    {
        [Given(@"I am using the (.*) browser")]
        public void GivenIAmUsingTheChromeBrowser(string browserName)
        {
            ScenarioEvents.RemoveOthersBeforeScenario(Browsers.SingleOrDefault(
                b => b.Name() == browserName));
        }

        [Then(@"I should see a (.*) section")]
        [Then(@"I should see an (.*) section")]
        public void SeeContentSection(string sectionName)
        {
            Browsers.ForEach(browser =>
            {
                browser.WaitUntil(b => b.GetPage().GetField(sectionName, true).IsNotNull(),
                    "@Browser could not find the '{0}' section.".FormatWith(sectionName));
                var field = browser.GetPage().GetField(sectionName);
                browser.WaitUntil(b => field.Displayed,
                    "@Browser did not display the '{0}' section.".FormatWith(sectionName));
            });
        }

        [Then(@"I shouldn't see a (.*) section")]
        [Then(@"I shouldn't see an (.*) section")]
        [Then(@"I should not see a (.*) section")]
        [Then(@"I should not see an (.*) section")]
        public void DoNotSeeContentSection(string sectionName)
        {
            Browsers.ForEach(browser => browser.WaitUntil(b => 
                b.GetPage().GetField(sectionName, true).IsNull() ||
                !b.GetPage().GetField(sectionName).Displayed,
                "@Browser unexpectedly displayed the '{0}' section.".FormatWith(sectionName)));
        }


        [Then(@"I should see ""(.*)"" in the (.*) section")]
        public void SeeTextInSection(string expectedText, string sectionName)
        {
            SeeContentSection(sectionName);

            Browsers.ForEach(browser => browser.WaitUntil(b => b.GetPage().GetField(sectionName).Text.Equals(expectedText),
                "@Browser did not contain text '{0}' for the '{1}' section (actual content was '{2}')."
                    .FormatWith(expectedText, sectionName, browser.GetPage().GetField(sectionName).Text)));
        }
    }
}
