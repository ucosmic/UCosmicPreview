using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    // ReSharper disable UnusedMember.Global
    public class StepEvents : TestRunEvents
    // ReSharper restore UnusedMember.Global
    {
        [AfterScenario]
        //[BeforeStep]
        //[DebuggerStepThrough]
        // ReSharper disable UnusedMember.Global
        public void DismissJavascriptAlertDialog()
        // ReSharper restore UnusedMember.Global
        {
            Browsers.ForEach(browser =>
            {
                try
                {
                    // Firefox is slow at fishing for alerts
                    // uncomment this if running BeforeStep
                    if (browser.IsFirefox()) return;

                    var alert = browser.SwitchTo().Alert();
                    if (alert != null)
                        alert.Dismiss();
                }
                catch (WebDriverException)
                {
                    // alert was not present in IE or FF
                }
                catch (InvalidOperationException)
                {
                    // alert was not present in Chrome
                }
            });
        }

        //[BeforeStep]
        //[DebuggerStepThrough]
        //public void ThinkForTenMilliseconds()
        //{
        //    10.WaitThisManyMilleseconds();
        //}
    }
}
