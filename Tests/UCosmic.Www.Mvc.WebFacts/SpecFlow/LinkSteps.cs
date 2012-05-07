using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class LinkSteps : BaseStepDefinition
    {
        [Given(@"I saw a ""(.*)"" link")]
        [Given(@"I saw an ""(.*)"" link")]
        [When(@"I see a ""(.*)"" link")]
        [When(@"I see an ""(.*)"" link")]
        [Then(@"I should see a ""(.*)"" link")]
        [Then(@"I should see an ""(.*)"" link")]
        public void SeeLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                var link = browser.WaitUntil(b => b.FindElement(By.LinkText(linkText)),
                    string.Format("Link with text '{0}' could not be found by @Browser.", 
                        linkText));
                browser.WaitUntil(b => link.Displayed,
                    string.Format("Link with text '{0}' was found but is not displayed by @Browser.", 
                        linkText));
            });
        }

        [Given(@"I clicked the ""(.*)"" link")]
        [When(@"I click the ""(.*)"" link")]
        [Then(@"I should click the ""(.*)"" link")]
        public void ClickLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                var link = browser.WaitUntil(b => b.FindElement(By.LinkText(linkText)),
                    string.Format("Link with text '{0}' could not be found by @Browser.",
                        linkText));
                link.Click();
            });
        }
    }
}
