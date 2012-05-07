using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Common.WebPages;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class InstitutionalAgreementForm : WebPageBase
    {
        public InstitutionalAgreementForm(IWebDriver driver) : base(driver) { }

        protected override Dictionary<string, string> SpecToWeb
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "Participant Search", "participant_search" },
                    { "Participant Search[AutoComplete]", ".ParticipantSearch-field" },
                    { "Agreement Type", "Type" },
                    { "Agreement Type[ErrorText='Required']", "Agreement type is required." },
                    { "Agreement Type[DownArrow]", ".Type-field button" },
                    { "Agreement Type[AutoComplete]", ".Type-field" },
                    { "Summary Description", "Title" },
                    { "Summary Description[ErrorText='Required']", "Summary description is required." },
                    { "Start Date", "StartsOn" },
                    { "Start Date[ErrorText='Required']", "Start date is required." },
                    { "Expiration Date", "ExpiresOn" },
                    { "Expiration Date[ErrorText='Required']", "Expiration date is required." },
                    { "Current Status", "Status" },
                    { "Current Status[ErrorText='Required']", "Current status is required." },
                    { "Current Status[DownArrow]", ".Status-field button" },
                    { "Current Status[AutoComplete]", ".Status-field" },
                    { "File Attachments", "PostedFile" },
                    { "File Attachments[FileUpload]", ".file-chooser input[type='file']" },
                    { "File Attachments[Collection='ItemText']", "ul#file_attachments li.file-attachment .file-chosen .file-name" },
                    { "File Attachments[Collection='RemoveItem-LargePdf33.8.pdf']",
                        string.Format(@"ul#file_attachments li.file-attachment .file-chosen a.remove-button[data-file-name=""{0}""]", "LargePdf33.8.pdf") },
                    { "File Attachments[ErrorText='Invalid']",
                        "You may only upload PDF, Microsoft Office, and Open Document files with a pdf, doc, docx, odt, xls, xlsx, ods, ppt, or pptx extension." },
                };
            }
        }

    }
}
