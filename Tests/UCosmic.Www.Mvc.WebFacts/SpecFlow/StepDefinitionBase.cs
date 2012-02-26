using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    public class StepDefinitionBase
    {
        protected static List<IWebDriver> Browsers { get { return WebDriverContext.Browsers; } } 
    }
}