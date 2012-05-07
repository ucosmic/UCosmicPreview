using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
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
    }
}
