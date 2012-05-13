using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc
{
    public class NullWebDriver : IWebDriver
    {
        public IWebElement FindElement(By @by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        public INavigation Navigate()
        {
            throw new NotImplementedException();
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }

        public string Url
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public string PageSource
        {
            get { throw new NotImplementedException(); }
        }

        public string CurrentWindowHandle
        {
            get { throw new NotImplementedException(); }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { throw new NotImplementedException(); }
        }
    }
}