using System.Linq;
using OpenQA.Selenium;
using Should;
using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class FileUploadSteps : BaseStepDefinition
    {
        [Given(@"I saw a (.*) upload field")]
        [When(@"I see a (.*) upload field")]
        [Then(@"I should see a (.*) upload field")]
        public void SeeFileAttachmentUploadInput(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var fileUpload = page.GetFileUploadField(fieldLabel);

                browser.WaitUntil(b => fileUpload.Displayed, string.Format(
                    "A file upload element for the '{0}' field was not displayed in @Browser.",
                        fieldLabel));
            });
        }

        [Given(@"I chose the file ""(.*)"" for the (.*) upload field")]
        [When(@"I choose the file ""(.*)"" for the (.*) upload field")]
        [Then(@"I should choose the ""(.*)"" for the (.*) upload field")]
        public void ChooseFileToUpload(string filePath, string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = WebPageFactory.GetPage(browser);
                var fileUpload = page.GetFileUploadField(fieldLabel);
                fileUpload.ChooseFile(filePath);
            });
        }
    }
}
