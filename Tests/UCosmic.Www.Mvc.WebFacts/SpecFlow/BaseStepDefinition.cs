using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc
{
    public class BaseStepDefinition
    {
        protected static List<IWebDriver> Browsers { get { return WebDriverContext.Browsers; } }

        protected static readonly By ByTagNameLi = By.TagName("li");

        protected static Func<IWebElement, bool> ElementTextEquals(string expectedText)
        {
            Func<IWebElement, bool> expectedItemText = li => li.Text.Equals(expectedText);
            return expectedItemText;
        }

    }
}