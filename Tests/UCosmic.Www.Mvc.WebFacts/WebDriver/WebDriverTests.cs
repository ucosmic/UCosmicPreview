using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc
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
        public void WebDriverContext_IsFirefox_ExtensionMethodExists()
        {
            var firefox = WebDriverContext.Browsers.Firefox();
            if (firefox != null)
                firefox.IsFirefox().ShouldBeTrue(
                    "IWebDriver.IsFirefox should return true if browser is Firefox.");
        }
    }
}
