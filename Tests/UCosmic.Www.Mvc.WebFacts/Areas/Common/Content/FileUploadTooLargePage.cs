using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Common
{
    public class FileUploadTooLargePage : Page
    {
        public FileUploadTooLargePage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "File Upload Too Large"; } }
        public override string Path { get { return MVC.Common.Errors.FileUploadTooLarge().AsPath(); } }
    }
}
