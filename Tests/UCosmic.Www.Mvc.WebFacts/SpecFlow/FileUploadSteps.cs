using System;
using System.Linq;
using TechTalk.SpecFlow;
using System.IO;

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
                fileUpload.ChooseFile(ResolveRelativeFilePath(filePath));
            });
        }

        private static string ResolveRelativeFilePath(string filePath)
        {
            if (filePath == null || !filePath.StartsWith("~/")) return filePath;

            var relativePath = filePath.Substring(2);
            var directoryName = relativePath.Substring(0, relativePath.IndexOf("/", StringComparison.Ordinal));
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while(directory != null && !directory.GetDirectories(directoryName).Any())
            {
                directory = directory.Parent;
            }
            var resolvedPath = directory != null
                ? Path.Combine(directory.FullName, relativePath) : relativePath;
            resolvedPath = resolvedPath.Replace("/", "\\");
            return resolvedPath;
        }
    }
}
