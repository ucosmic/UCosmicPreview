using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class FileUploadSteps : BaseStepDefinition
    {
        [Then(@"I should see a (.*) upload field")]
        public void SeeFileAttachmentUploadInput(string fieldLabel)
        {
            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var fileUpload = page.GetField(fieldLabel);
                browser.WaitUntil(b => fileUpload.Displayed, string.Format(
                    "A file upload element for the '{0}' field was not displayed by @Browser.",
                        fieldLabel));
            });
        }

        [When(@"I choose the file ""(.*)"" for the (.*) upload field")]
        public void ChooseFileToUpload(string filePath, string fieldLabel)
        {
            SeeFileAttachmentUploadInput(fieldLabel);

            Browsers.ForEach(browser =>
            {
                var page = browser.GetPage();
                var fileUpload = page.GetField(fieldLabel);
                fileUpload.ChooseFile(filePath);
            });
        }
    }
}
