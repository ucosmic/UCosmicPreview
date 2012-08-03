using System.Linq;
using OpenQA.Selenium;
using Should;
using TechTalk.SpecFlow;
using UCosmic.Impl;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class FormSteps : BaseStepDefinition
    {
        //[When(@"I click the ""(.*)"" button")]
        //public void ClickLabeledButton(string label)
        //{
        //    var cssSelector = string.Format("form input[type=button][value='{0}']", label);

        //    Browsers.ForEach(browser =>
        //    {
        //        var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
        //            string.Format("Button labeled '{0}' was not found by @Browser (using CSS selector {1})",
        //                label, cssSelector));
        //        button.ClickButton();
        //    });
        //}

        //[When(@"I click the ""(.*)"" submit button")]
        //public void ClickLabeledSubmitButton(string label)
        //{
        //    var cssSelector = string.Format("form input[type=submit][value='{0}']", label);

        //    Browsers.ForEach(browser =>
        //    {
        //        var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
        //            string.Format("Submit button labeled '{0}' was not found by @Browser (using CSS selector {1})",
        //                label, cssSelector));
        //        button.ClickButton();
        //    });
        //}

        //[When(@"I click the submit button")]
        //public void ClickSubmitButton()
        //{
        //    const string cssSelector = "form input[type=submit]";

        //    Browsers.ForEach(browser =>
        //    {
        //        var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
        //            string.Format("Submit button was not found by @Browser (using CSS selector {0})",
        //                cssSelector));
        //        button.ClickButton();
        //    });
        //}

        //[Then(@"the ""(.*)"" submit button should be disabled")]
        //public void EnsureLabeledSubmitButtonIsDisabled(string label)
        //{
        //    var cssSelector = string.Format("form input[type=submit][value='{0}']", label);

        //    Browsers.ForEach(browser =>
        //    {
        //        var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
        //            string.Format("Submit button labeled '{0}' was not found by @Browser (using CSS selector {1})",
        //                label, cssSelector));

        //        browser.WaitUntil(b => "true".Equals(button.GetAttribute("disabled")),
        //            "Submit button labeled '{0}' was not disabled in @Browser."
        //                .FormatWith(label));
        //    });
        //}

        //[Then(@"the ""(.*)"" submit button should not be disabled")]
        //[Then(@"the ""(.*)"" submit button shouldn't be disabled")]
        //public void EnsureLabeledSubmitButtonIsNotDisabled(string label)
        //{
        //    var cssSelector = string.Format("form input[type=submit][value='{0}']", label);

        //    Browsers.ForEach(browser =>
        //    {
        //        var button = browser.WaitUntil(b => b.FindElement(By.CssSelector(cssSelector)),
        //            string.Format("Submit button labeled '{0}' was not found by @Browser (using CSS selector {1})",
        //                label, cssSelector));

        //        browser.WaitUntil(b => "false".Equals(button.GetAttribute("disabled")),
        //            "Submit button labeled '{0}' was not disabled in @Browser."
        //                .FormatWith(label));
        //    });
        //}

        //[Given(@"I saw the flash feedback message ""(.*)""")]
        //[When(@"I see the flash feedback message ""(.*)""")]
        [Then(@"I should see the flash feedback message ""(.*)""")]
        public void SeeFlashFeedbackMessage(string topMessage)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the element was located
                var validationElement = browser.WaitUntil(b => b.GetElement(By.Id("feedback_flash")),
                    string.Format("Flash feedback message element does not exist using @Browser."));

                // ensure the element is displayed
                browser.WaitUntil(b => validationElement.Displayed,
                    string.Format("Flash feedback message element was not displayed using @Browser."));

                // verify the success message
                browser.WaitUntil(b => validationElement.Displayed && validationElement.Text.Equals(topMessage),
                    string.Format("Flash feedback message '{0}' was not displayed using @Browser. " +
                        "(Actual message was '{1}'.)", topMessage, validationElement.Text));
            });
        }

        [When(@"I receive mail with the subject ""(.*)""")]
        public void ReceiveMailBySubject(string subject)
        {
            var httpClient = new WebRequestHttpConsumer();
            var deliveryUrl = AppConfig.TestMailDelivery.ToAbsoluteUrl();
            var inboxUrl = AppConfig.TestMailInbox.ToAbsoluteUrl();
            var emailFileNames = httpClient.Get(deliveryUrl).Split('\n')
                .Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var messages = new List<string>();

            var browserCount = 0;
            Browsers.ForEach(browser =>
            {
                ++browserCount;
                emailFileNames.Count.ShouldBeInRange(browserCount, int.MaxValue);
                var emailFileName = emailFileNames[browserCount - 1];
                var messagePath = string.Format("{0}/{1}", inboxUrl, emailFileName);
                var message = httpClient.Get(messagePath);
                message = message.Replace("=\r\n", string.Empty).Replace("=0D=0A", "\r\n");
                message.ShouldContain(string.Format("Subject: {0}", subject));
                messages.Add(message);
            });
            ScenarioContext.Current.Add(ScenarioContextKeys.EmailMessages, messages);
        }

        [When(@"I type the mailed (.*) into the (.*) text field")]
        public void TypeMailExcerptIntoTextField(string excerptDetail, string fieldLabel)
        {
            var messages = ScenarioContext.Current[ScenarioContextKeys.EmailMessages] as IEnumerable<string>;
            if (messages == null)
                Assert.Fail("This scenario expected emails, but there were none.");

            var browserCount = 0;
            Browsers.ForEach(browser =>
            {
                var message = messages.Skip(browserCount++).Take(1).Single();
                var page = browser.GetPage();
                var mailExcerpt = page.GetMailExcerpt(message);
                var textBox = page.GetField(fieldLabel);
                textBox.Clear();
                textBox.SendKeys(mailExcerpt);
            });
        }

        [When(@"I wait for (.*) seconds")]
        public void WaitForANumberOfSeconds(int secondsToWait)
        {
            var millisecondsToWait = secondsToWait*1000;
            millisecondsToWait.WaitThisManyMilleseconds();
        }
    }
}
