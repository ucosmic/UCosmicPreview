using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.WebDriver
{
    [TestClass]
    public class WebDriverTests
    {
        [TestMethod]
        public void WebDriverContext_RemoteBrowsers_ShouldNotBeNull()
        {
            WebDriverContext.Browsers.ShouldNotBeNull(
                "WebDriverContext.Browsers was unexpectedly null.");
        }

        [TestMethod]
        public void WebDriverContext_AuthenticationStepMethod_ShouldBePublic()
        {
            var browsers = WebDriverContext.Browsers.ToList();
            WebDriverContext.Browsers.Clear();

            Assert.Inconclusive();

            //var steps = new AuthenticationSteps();
            //steps.TypeIntoTextBox("something", "someId");
            //steps.ClickSignInButton();
            //steps.SeeATopIdentityAreaWithPartialGreeting("inOrOut", "partialGreeting");

            WebDriverContext.Browsers.AddRange(browsers);
        }

        [TestMethod]
        public void WebDriverContext_IsFirefox_ExtensionMethodExists()
        {
            var firefox = WebDriverContext.Browsers.Firefox();
            if (firefox != null)
            {
                firefox.IsFirefox().ShouldBeTrue("IWebDriver.IsFirefox should return true if browser is firefox.");
            }
        }
    }
}
