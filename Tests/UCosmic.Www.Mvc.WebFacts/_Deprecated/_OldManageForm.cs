//using System.Collections.Generic;
//using OpenQA.Selenium;
//using UCosmic.Www.Mvc.Areas.Common.WebPages;

//namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
//{
//    public class ManageForm : WebPageBase
//    {
//        public ManageForm(IWebDriver driver)
//            : base(driver)
//        {
//            var testContacts = new[]
//            {
//                "Partner Principal Mitch Leventhal",
//                "Home Principal Mitch Leventhal",
//                "Partner Secondary Mitch Leventhal",
//                "Home Principal Ron Cushing",
//                "Partner Principal Mary Watkins",
//                "Partner Secondary Mary Watkins",
//                "Home Principal Brandon Lee",
//            };
//            foreach (var testContact in testContacts)
//            {
//                _specToWeb.Add(
//                    string.Format("Contacts[Collection='RemoveItem-{0}']", testContact),
//                    string.Format(@"ul#contacts_list li.agreement-contact a.remove-button[data-contact-type-and-display-name=""{0}""]",
//                        testContact)
//                );
//            }
//        }

//        private readonly Dictionary<string, string> _specToWeb =
//            new Dictionary<string, string>
//            {
//                { "Participant Search", "participant_search" },
//                { "Participant Search[AutoComplete]", ".ParticipantSearch-field" },
//                { "Participants[Collection='ItemText']", "ul#participants_list li.participant strong" },
//                { "Agreement Type", "Type" },
//                { "Agreement Type[ErrorText='Required']", "Agreement type is required." },
//                { "Agreement Type[DownArrow]", ".Type-field button" },
//                { "Agreement Type[AutoComplete]", ".Type-field" },
//                { "Summary Description", "Title" },
//                { "Summary Description[ErrorText='Required']", "Summary description is required." },
//                { "Automatically Generate Summary Description", "input#IsTitleDerived[type=checkbox][name=\"IsTitleDerived\"]" },
//                { "Start Date", "StartsOn" },
//                { "Start Date[ErrorText='Required']", "Start date is required." },
//                { "Expiration Date", "ExpiresOn" },
//                { "Expiration Date[ErrorText='Required']", "Expiration date is required." },
//                { "Current Status", "Status" },
//                { "Current Status[ErrorText='Required']", "Current status is required." },
//                { "Current Status[DownArrow]", ".Status-field button" },
//                { "Current Status[AutoComplete]", ".Status-field" },
//                { "File Attachments", "PostedFile" },
//                { "File Attachments[FileUpload]", ".file-chooser input[type='file']" },
//                { "File Attachments[Collection='ItemText']", "ul#file_attachments li.file-attachment .file-chosen .file-name" },
//                { "File Attachments[Collection='RemoveItem-LargePdf33.8.pdf']",
//                    string.Format(@"ul#file_attachments li.file-attachment .file-chosen a.remove-button[data-file-name=""{0}""]", "LargePdf33.8.pdf") },
//                { "File Attachments[ErrorText='Invalid']",
//                    "You may only upload PDF, Microsoft Office, and Open Document files with a pdf, doc, docx, odt, xls, xlsx, ods, ppt, or pptx extension." },

//                { "Contacts[Collection='ItemText']", "ul#contacts_list li.agreement-contact .type-and-name" },
//                { "Contact Type", "ContactType" },
//                { "Contact Type[ErrorText='Required']", "Contact type is required." },
//                { "Contact Type[DownArrow]", ".ContactType-field button" },
//                { "Contact Type[AutoComplete]", ".ContactType-field" },

//                { "Email Address", "Person_DefaultEmail" },
//                { "Email Address[AutoComplete]", ".Person_DefaultEmail-field" },

//                { "First Name", "Person_FirstName" },
//                { "First Name[ErrorText='Required']", "Contact first name is required." },
//                { "First Name[AutoComplete]", ".Person_FirstName-field" },

//                { "Last Name", "Person_LastName" },
//                { "Last Name[ErrorText='Required']", "Contact last name is required." },
//                { "Last Name[AutoComplete]", ".Person_LastName-field" },

//                { "Salutation", "Person_Salutation" },
//                { "Middle Name Or Initial", "Person_MiddleName" },
//                { "Suffix", "Person_Suffix" },
//            };

//        protected override Dictionary<string, string> SpecToWeb
//        {
//            get { return _specToWeb; }
//        }
//    }
//}
