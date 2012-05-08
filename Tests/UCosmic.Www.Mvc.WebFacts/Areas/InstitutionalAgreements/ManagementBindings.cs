using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    [Binding]
    public class ManagementBindings : BaseStepDefinition
    {
        [Given(@"I did(.*) see  a modal dialog with an Add Institutional Agreement Contact form")]
        [Given(@"I saw(.*) a modal dialog with an Add Institutional Agreement Contact form")]
        [Then(@"I should(.*) see a modal dialog with an Add Institutional Agreement Contact form")]
        public void SeeAddContactFormInModalDialog(string not)
        {
            const string cssSelector = "#simplemodal-container";
            var shouldSee = string.IsNullOrWhiteSpace(not);

            Browsers.ForEach(browser =>
            {
                if (shouldSee)
                {
                    browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)) != null,
                        "The Add Institutional Agreement Contact modal dialog was not found by @Browser.");
                    var element = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                        "The Add Institutional Agreement Contact modal dialog was not found by @Browser.");

                    browser.WaitUntil(b => element.Displayed,
                        "The Add Institutional Agreement Contact modal dialog was not displayed in @Browser.");
                }
                else
                {
                    browser.WaitUntil(b => b.TryFindElement(By.CssSelector(cssSelector)) == null,
                        "The Add Institutional Agreement Contact modal dialog was unexpectedly displayed in @Browser.");
                }
            });
        }

        [When(@"I click the Add Institutional Agreement Contact modal dialog close icon")]
        public void ClickTheModalDialogCloseIcon()
        {
            const string cssSelector = "#simplemodal-container a.modalCloseImg";
            Browsers.ForEach(browser =>
            {
                var link = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
                    "The Add Institutional Agreement Contact modal dialog close icon could not be found using @Browser.");

                browser.WaitUntil(b => link.Displayed,
                    "The Add Institutional Agreement Contact modal dialog close icon was not displayed using @Browser.");

                link.Click();

                //link.Click();
                //if (!browser.IsInternetExplorer()) return;
                //try
                //{
                //    link.Click();
                //}
                //catch (WebDriverException)
                //{
                //}
            });
        }
    }
}
