using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Should;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.SpecFlow;
using UCosmic.Www.Mvc.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Areas.Common.Steps
{
    // ReSharper disable UnusedMember.Global
    [Binding]
    public class SharedSteps : StepDefinitionBase
    {
        #region Page & Url Navigation

        [Given(@"I browsed to the (.*) page")]
        [When(@"I browse to the (.*) page")]
        [Then(@"I should browse to the (.*) page")]
        public void BrowseToPage(string title)
        {
            var url = RelativeUrl.For(title);
            url = url.ToAbsoluteUrl();

            Browsers.ForEach(browser =>
            {
                // navigate to the page
                browser.Navigate().GoToUrl(url);

                // ensure that browser has navigated to the page
                browser.WaitUntil(b => b.Url.Equals(url),
                    string.Format("@Browser did not navigate to the '{0}' page at the URL '{1}'.",
                        title, url));
            });
        }

        [Given(@"I have browsed to the ""(.*)"" url")]
        [When(@"I browse to the ""(.*)"" url")]
        [Then(@"I should browse to the ""(.*)"" url")]
        public void BrowseToPageAt(string url)
        {
            url = url.ToAbsoluteUrl();

            Browsers.ForEach(browser =>
            {
                browser.Navigate().GoToUrl(url);

                browser.WaitUntil(b => b.Url.Equals(url),
                    string.Format("@Browser did not navigate to the URL '{0}'.", url));
            });
        }

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

            Browsers.ForEach(browser =>
                browser.WaitUntil(b => b.Url.StartsWith(url), string.Format(
                    "@Browser did not arrive on the '{0}' page at the URL '{1}'.",
                        title, url)));
        }

        [Given(@"I have seen a page at the ""(.*)"" url")]
        [When(@"I see a page at the ""(.*)"" url")]
        [Then(@"I should see a page at the ""(.*)"" url")]
        public void SeePageAt(string url)
        {
            Browsers.ForEach(browser =>
                SeeThePageAt(browser, url));
        }

        [Given(@"I have seen a page at the ""(.*)"" url within (.*) seconds")]
        [When(@"I see a page at the ""(.*)"" url within (.*) seconds")]
        [Then(@"I should see a page at the ""(.*)"" url within (.*) seconds")]
        public void SeePageAt(string url, int timeoutSeconds)
        {
            Browsers.ForEach(browser =>
                SeeThePageAt(browser, url, timeoutSeconds));
        }

        private const string UrlPathVariableToken = "[PathVar]";
        public static void SeeThePageAt(IWebDriver browser, string url, int? maxSeconds = null)
        {
            url = url.ToAbsoluteUrl(); // convert to absolute URL

            // check for URL path variable token "[PathVar]"
            var isExact = !url.Contains(UrlPathVariableToken);

            if (isExact)
            {
                // do an exact comparison of the expected & actual URL's
                browser.WaitUntil(b => b.Url.Equals(url), string.Format(
                    "@Browser did not arrive at the URL '{0}'.", url), maxSeconds);
            }
            else
            {
                // if there is a path variable token, check that the actual URL starts with the 
                // part of the expected URL before the token, and ends with the part of the 
                // expected URL after the token
                var urlFront = url.Substring(0, url.IndexOf(UrlPathVariableToken, StringComparison.Ordinal));
                var urlBack = url.Substring(url.LastIndexOf(UrlPathVariableToken, StringComparison.Ordinal) 
                    + UrlPathVariableToken.Length);
                browser.WaitUntil(b => b.Url.StartsWith(urlFront, StringComparison.OrdinalIgnoreCase)
                    && b.Url.EndsWith(urlBack, StringComparison.OrdinalIgnoreCase), string.Format(
                        "@Browser did not arrive at the URL '{0}'.", url), maxSeconds);
            }
        }

        [Given(@"I saw a 404 Not Found page")]
        [When(@"I see a 404 Not Found page")]
        [Then(@"I should see a 404 Not Found page")]
        public void See404NotFoundPage()
        {
            Browsers.ForEach(browser => browser.WaitUntil(b => b.FindElement(By.Id("not_found_404")),
                "@Browser did not arrive at the HTTP 404 Not Found page."));
        }

        #endregion
        #region Hyperlinks

        [Given(@"I have seen a ""(.*)"" link")]
        [When(@"I see a ""(.*)"" link")]
        [Then(@"I should see a ""(.*)"" link")]
        public void SeeLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the hyperlink element is located
                var link = browser.WaitUntil(b => b.FindElement(By.LinkText(linkText)),
                    string.Format("A hyperlink with text '{0}' does not exist using @Browser.", linkText));

                // ensure the link is visible
                browser.WaitUntil(b => link.Displayed,
                    string.Format("A hyperlink with text '{0}' was not displayed using @Browser.", linkText));
            });
        }

        [Given(@"I did not see a ""(.*)"" link")]
        [When(@"I do not see a ""(.*)"" link")]
        [Then(@"I should not see a ""(.*)"" link")]
        public void DoNotSeeLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                // try to locate the element
                var link = browser.TryFindElement(By.LinkText(linkText));

                // ensure the link is not visible
                browser.WaitUntil(b => link == null || !link.Displayed,
                    string.Format("A hyperlink with text '{0}' was unexpectedly displayed using @Browser.", linkText));
            });
        }

        [Given(@"I have seen a ""(.*)"" partial link")]
        [When(@"I see a ""(.*)"" partial link")]
        [Then(@"I should see a ""(.*)"" partial link")]
        public void SeeLinkWithPartialText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the hyperlink element is located
                var link = browser.WaitUntil(b => b.FindElement(By.PartialLinkText(linkText)),
                    string.Format("A hyperlink with partial text '{0}' does not exist using @Browser.", linkText));

                // ensure the link is visible
                browser.WaitUntil(b => link.Displayed,
                    string.Format("A hyperlink with partial text '{0}' was not displayed using @Browser.", linkText));
            });
        }

        [Given(@"I have clicked the ""(.*)"" link")]
        [When(@"I click the ""(.*)"" link")]
        [Then(@"I should click the ""(.*)"" link")]
        public void ClickLinkWithText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the hyperlink element is located
                var link = browser.WaitUntil(b => b.FindElement(By.LinkText(linkText)),
                    string.Format("@Browser could not find a link with text '{0}'.", linkText));

                link.ClickLink();
            });
        }

        [Given(@"I have clicked the ""(.*)"" partial link")]
        [When(@"I click the ""(.*)"" partial link")]
        [Then(@"I should click the ""(.*)"" partial link")]
        public void ClickLinkWithPartialText(string linkText)
        {
            Browsers.ForEach(browser =>
            {
                // ensure that the hyperlink element is located
                var link = browser.WaitUntil(b => b.FindElement(By.PartialLinkText(linkText)),
                    string.Format("@Browser could not find a link with partial text '{0}'.", linkText));

                link.ClickLink();
            });
        }

        [Given(@"I have clicked either the ""(.*)"" or ""(.*)"" link")]
        [When(@"I click either the ""(.*)"" or ""(.*)"" link")]
        [Then(@"I should click either the ""(.*)"" or ""(.*)"" link")]
        public void ClickOneOfTwoLinksWithText(string linkText1, string linkText2)
        {
            Browsers.ForEach(browser =>
            {
                // try to find either of the 2 links
                var link = browser.TryFindElement(By.LinkText(linkText1)) ??
                           browser.TryFindElement(By.LinkText(linkText2));

                if (link != null)
                {
                    // if a link is found, click it
                    link.ClickLink();
                }
                else
                {
                    // if neither link was found, fail the step
                    Assert.Fail(string.Format(
                        "Expected link '{0}' or '{1}' but couldn't fine either using @Browser.",
                            linkText1, linkText2));
                }
            });
        }

        #endregion
        #region Input Fields & Error Messages

        [Given(@"I typed ""(.*)"" into the (.*) text box")]
        [When(@"I type ""(.*)"" into the (.*) text box")]
        [Then(@"I should type ""(.*)"" into the (.*) text box")]
        public void TypeIntoTextBox(string textToType, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var textBox = page.GetTextBox(fieldLabel);
                textBox.TypeText(textToType);
            });
        }

        [Given(@"I (.*) an error message ""(.*)"" under the (.*) text box")]
        [When(@"I (.*) an error message ""(.*)"" under the (.*) text box")]
        [Then(@"I should (.*) an error message ""(.*)"" under the (.*) text box")]
        [Given(@"I (.*) any error message(.*) under the (.*) text box")]
        [When(@"I (.*) any error message(.*) under the (.*) text box")]
        [Then(@"I should (.*) any error message(.*) under the (.*) text box")]
        public void SeeErrorMessage(string seeOrNot, string messageText, string fieldLabel)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "saw");
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                switch (shouldSee)
                {
                    case true:
                        var errorMessage = page.GetErrorMessage(fieldLabel);
                        browser.WaitUntil(b => errorMessage.Displayed == shouldSee, string.Format(
                            "An error message '{1}' should be displayed under the {0} field using @Browser.",
                                fieldLabel, messageText));
                        browser.WaitUntil(b => errorMessage.Text.Equals(messageText), string.Format(
                            "The error message under the {0} field was '{2}', but should have been '{1}' using @Browser.",
                                fieldLabel, messageText, errorMessage.Text));
                        break;

                    case false:
                        errorMessage = page.GetErrorMessage(fieldLabel, true);
                        browser.WaitUntil(b => errorMessage == null || !errorMessage.Displayed, string.Format(
                            "An error message '{1}' should not be displayed under the {0} field using @Browser.",
                                fieldLabel, messageText));
                        break;
                }
            });
        }

        #endregion
        #region Buttons

        [Given(@"I clicked the ""(.*)"" button")]
        [When(@"I click the ""(.*)"" button")]
        [Then(@"I should click the ""(.*)"" button")]
        public void ClickButton(string buttonLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var button = page.GetButton(buttonLabel);
                button.ClickButton();
            });
        }

        [Given(@"I submitted the ""(.*)"" button")]
        [When(@"I submit the ""(.*)"" button")]
        [Then(@"I should submit the ""(.*)"" button")]
        public void SubmitButton(string buttonLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var button = page.GetButton(buttonLabel);
                button.ClickSubmitButton();
            });
        }

        [Given(@"I double clicked the ""(.*)"" button")]
        [When(@"I double click the ""(.*)"" button")]
        [Then(@"I should double click the ""(.*)"" button")]
        public void DoubleClickButton(string buttonLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var button = page.GetButton(buttonLabel);
                button.ClickButton();
                button.ClickButton();
            });
        }

        [Given(@"I paused for a moment")]
        [When(@"I pause for a moment")]
        [Then(@"I should pause for a moment")]
        public void PauseForAMoment()
        {
            Browsers.ForEach(browser => 500.WaitThisManyMilleseconds());
        }

        #endregion
        #region Custom Messages & Content

        [Given(@"I saw a temporary ""(.*)"" feedback message")]
        [When(@"I see a temporary ""(.*)"" feedback message")]
        [Then(@"I should see a temporary ""(.*)"" feedback message")]
        public void SeeFeedbackMessage(string messageText)
        {
            Browsers.ForEach(browser =>
            {
                if (browser.IsInternetExplorer())
                    1000.WaitThisManyMilleseconds();

                // ensure that the element was located
                var element = browser.WaitUntil(b => b.FindElement(By.Id("feedback_flash")), string.Format(
                    "Feedback message element does not exist using @Browser."));

                // ensure the element is displayed
                browser.WaitUntil(b => element.Displayed, string.Format(
                    "Feedback message element was not displayed using @Browser."));

                // verify the success message
                browser.WaitUntil(b => element.Text.Equals(messageText), string.Format(
                    "The feedback message was '{1}', but should have been '{0}' using @Browser.",
                        messageText, element.Text));
            });
        }

        [Given(@"I (.*) a ""(.*)"" message")]
        [When(@"I (.*) a ""(.*)"" message")]
        [Then(@"I should (.*) a ""(.*)"" message")]
        public void SeeCustomMessage(string seeOrNot, string messageText)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "saw");
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                switch (shouldSee)
                {
                    case true:
                        var customMessage = page.GetCustomContent(messageText);
                        browser.WaitUntil(b => customMessage.Displayed == shouldSee, string.Format(
                            "A custom message '{0}' should be displayed using @Browser.",
                                messageText));
                        browser.WaitUntil(b => customMessage.Text.Equals(messageText), string.Format(
                            "The custom message was '{1}', but should have been '{0}' using @Browser.",
                                messageText, customMessage.Text));
                        break;

                    case false:
                        customMessage = page.GetCustomContent(messageText, true);
                        browser.WaitUntil(b => customMessage == null || !customMessage.Displayed, string.Format(
                            "A custom message '{0}' should not be displayed using @Browser.",
                                messageText));
                        break;
                }
            });
        }

        [Given(@"I (.*) \[(.*)] content")]
        [When(@"I (.*) \[(.*)] content")]
        [Then(@"I should (.*) \[(.*)] content")]
        public void SeeCustomContent(string seeOrNot, string contentDescription)
        {
            var shouldSee = (seeOrNot == "see" || seeOrNot == "saw");
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                switch (shouldSee)
                {
                    case true:
                        var customIcon = page.GetCustomContent(contentDescription);
                        browser.WaitUntil(b => customIcon.Displayed == shouldSee, string.Format(
                            "A custom '{0}' icon should be displayed using @Browser.",
                                contentDescription));
                        break;

                    case false:
                        customIcon = page.GetCustomContent(contentDescription, true);
                        browser.WaitUntil(b => customIcon == null || !customIcon.Displayed, string.Format(
                            "A custom '{0}' icon should not be displayed using @Browser.",
                                contentDescription));
                        break;
                }
            });
        }

        #endregion
        #region Email

        [Given(@"I received an email with subject ""(.*)""")]
        [When(@"I receive an email with subject ""(.*)""")]
        [Then(@"I should receive an email with subject ""(.*)""")]
        public void ReceiveAnEmail(string subject)
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

        [Given(@"I typed the emailed code into the (.*) text box")]
        [When(@"I type the emailed code into the (.*) text box")]
        [Then(@"I should type the emailed code into the (.*) text box")]
        public void TypeEmailedCodeIntoConfirmEmailTextBox(string fieldLabel)
        {
            var messages = ScenarioContext.Current[ScenarioContextKeys.EmailMessages] as List<string>;
            if (messages == null)
                Assert.Fail("This scenario expected emails, but there were none.");

            var browserCount = 0;
            Browsers.ForEach(browser =>
            {
                var message = messages[browserCount++];
                var page = WebPageFactory.GetPage(browser);
                var secretCode = page.GetEmailExcerpt(message);
                var textBox = page.GetTextBox(fieldLabel);
                textBox.TypeText(secretCode);
            });
        }

        #endregion
        #region Browser Targeting

        [Given(@"I am using the (.*) browser")]
        public void GivenIAmUsingTheChromeBrowser(string browserName)
        {
            ScenarioEvents.RemoveOthersBeforeScenario(Browsers.SingleOrDefault(
                b => b.Name() == browserName));
        }

        #endregion

    }
    // ReSharper restore UnusedMember.Global
}
