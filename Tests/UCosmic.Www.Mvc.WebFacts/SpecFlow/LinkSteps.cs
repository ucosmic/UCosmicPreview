using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class LinkSteps : BaseStepDefinition
    {
        [Given(@"I see a ""(.*)"" link")]
        [Given(@"I should see an ""(.*)"" link")]
        [Then(@"I should see a ""(.*)"" link")]
        [Then(@"I should see an ""(.*)"" link")]
        public void SeeLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                browser.WaitUntil(b => b.TryFindElement(By.LinkText(linkText)) != null,
                    "Link with text '{0}' could not be found by @Browser."
                        .FormatWith(linkText));
                var link = browser.WaitUntil(b => b.GetElement(By.LinkText(linkText)), null);
                browser.WaitUntil(b => link.Displayed,
                    string.Format("Link with text '{0}' was found but is not displayed by @Browser.",
                        linkText));
            });
        }

        [When(@"I click the ""(.*)"" link")]
        public void ClickLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                var link = browser.WaitUntil(b => b.GetElement(By.LinkText(linkText)),
                    string.Format("Link with text '{0}' could not be found by @Browser.",
                        linkText));
                link.ClickIt();
            });
        }

        [Given(@"I see a link titled ""(.*)""")]
        [Then(@"I should see a link titled ""(.*)""")]
        public void SeeTitledLink(string linkTitle)
        {
            var cssSelector = string.Format("a[title='{0}']", linkTitle);
            Browsers.ForEach(browser =>
            {
                browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)) != null,
                    "Link with title '{0}' could not be found by @Browser."
                        .FormatWith(linkTitle));
                var link = browser.FindElement(By.CssSelector(cssSelector));
                browser.WaitUntil(b => link.Displayed,
                    string.Format("Link with title '{0}' was found but is not displayed by @Browser.",
                        linkTitle));
            });
        }

        [When(@"I click the link titled ""(.*)""")]
        public void ClickTitledLink(string linkTitle)
        {
            SeeTitledLink(linkTitle);
            var cssSelector = string.Format("a[title='{0}']", linkTitle);
            Browsers.ForEach(browser =>
            {
                var link = browser.GetElement(By.CssSelector(cssSelector));
                link.ClickIt();
            });
        }
    }
}
