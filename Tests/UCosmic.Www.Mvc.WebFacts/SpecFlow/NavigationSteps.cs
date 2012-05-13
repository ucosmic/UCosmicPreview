using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class NavigationSteps : BaseStepDefinition
    {
        [Given(@"I am starting from the (.*) page")]
        [When(@"I enter the URL for the (.*) page")]
        public void GoToPage(string title)
        {
            var page = title.GetPage();
            Browsers.ForEach(browser => browser.Navigate().GoToUrl(page.AbsoluteUrl));
        }

        [Then(@"I should see the (.*) page")]
        [Then(@"I should still see the (.*) page")]
        public void SeePage(string title)
        {
            var page = title.GetPage();
            Browsers.ForEach(browser => browser.WaitUntil(b => b.Url.MatchesUrl(page.AbsoluteUrl),
                "@Browser is at '{0}' instead of the expected '{1}' page."
                    .FormatWith(browser.Url, title)));
        }

        [Then(@"I should see the (.*) page within (.*) seconds")]
        public void SeePageWithinGivenTimeFrame(string title, int timeoutInSeconds)
        {
            var page = title.GetPage();
            Browsers.ForEach(browser => browser.WaitUntil(b => b.Url.MatchesUrl(page.AbsoluteUrl),
                "@Browser is at '{0}' instead of the expected '{1}' page (waited {2} seconds)."
                    .FormatWith(browser.Url, title, timeoutInSeconds), timeoutInSeconds));
        }
    }
}
