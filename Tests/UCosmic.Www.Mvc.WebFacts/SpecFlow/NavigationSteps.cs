using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class NavigationSteps : BaseStepDefinition
    {
        [Given(@"I saw the (.*) page")]
        [When(@"I see the (.*) page")]
        [Then(@"I should see the (.*) page")]
        [Given(@"I still saw the (.*) page")]
        [When(@"I still see the (.*) page")]
        [Then(@"I should still see the (.*) page")]
        public void SeePage(string title)
        {
            var url = RelativeUrl.For(title);
            url = url.ToAbsoluteUrl();

            Browsers.ForEach(browser => browser.WaitUntil(b => b.Url.MatchesUrl(url),
                string.Format("@Browser is at '{0}' instead of the expected '{1}' URL.",
                            browser.Url, url)));
        }

        [Given(@"I saw the (.*) page within (.*) seconds")]
        [When(@"I see the (.*) page within (.*) seconds")]
        [Then(@"I should see the (.*) page within (.*) seconds")]
        public void SeePageWithinGivenTimeFrame(string title, int timeoutInSeconds)
        {
            var url = RelativeUrl.For(title);
            url = url.ToAbsoluteUrl();

            Browsers.ForEach(browser => browser.WaitUntil(b => b.Url.MatchesUrl(url),
                string.Format("@Browser is at '{0}' instead of the expected '{1}' URL.",
                            browser.Url, url), timeoutInSeconds));
        }

        [Given(@"I am starting from the (.*) page")]
        public void GoToPage(string title)
        {
            var url = RelativeUrl.For(title);
            url = url.ToAbsoluteUrl();
            Browsers.ForEach(browser => browser.Navigate().GoToUrl(url));
        }
    }
}
