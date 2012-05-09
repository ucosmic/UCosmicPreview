using TechTalk.SpecFlow;
using UCosmic.Www.Mvc.Areas.Common.WebPages;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    public class FileUploadSteps : BaseStepDefinition
    {
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

        [When(@"I choose the file ""(.*)"" for the (.*) upload field")]
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
