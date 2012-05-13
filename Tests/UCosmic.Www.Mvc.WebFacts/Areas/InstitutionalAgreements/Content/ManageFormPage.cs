using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public abstract class ManageFormPage : Page
    {
        public ManageFormPage(IWebDriver browser) : base(browser) { }

        public override IEnumerable<Content> NestedContents
        {
            get { return new[] { new ManageAddContactContent(Browser), }; }
        }

        public const string ParticipantsLabel = "Participants";
        public const string ParticipantSearchLabel = "Participant Search";
        public const string AgreementTypeLabel = "Agreement Type";
        public const string TitleLabel = "Summary Description";
        public const string IsTitleDerivedLabel = "Automatically Generate Summary Description";
        public const string StartDateLabel = "Start Date";
        public const string ExpirationDateLabel = "Expiration Date";
        public const string CurrentStatusLabel = "Current Status";
        public const string ContactsLabel = "Contacts";
        public const string FileAttachmentsLabel = "File Attachments";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { ParticipantsLabel.CollectionItemTextKey(), "ul#participants_list li.participant strong" },
                { ParticipantSearchLabel, "textarea#participant_search" },
                { ParticipantSearchLabel.AutoCompleteMenuKey(), ".ParticipantSearch-field .autocomplete-menu ul" },

                { AgreementTypeLabel, "input#Type" },
                { AgreementTypeLabel.AutoCompleteMenuKey(), ".Type-field .autocomplete-menu ul" },
                { AgreementTypeLabel.ComboBoxDownArrowKey(), ".Type-field button" },
                { AgreementTypeLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Type]" },
                { AgreementTypeLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementForm.TypeRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementForm.TypeDisplayName) },

                { TitleLabel, "textarea#Title" },
                { TitleLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Title]" },
                { TitleLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementForm.TitleRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementForm.TitleDisplayName) },
                { IsTitleDerivedLabel, "input#IsTitleDerived[type=checkbox][name=\"IsTitleDerived\"]" },

                { StartDateLabel, "input#StartsOn" },
                { StartDateLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=StartsOn]" },
                { StartDateLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementForm.StartsOnRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementForm.StartsOnDisplayName) },

                { ExpirationDateLabel, "input#ExpiresOn" },
                { ExpirationDateLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=ExpiresOn]" },
                { ExpirationDateLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementForm.ExpiresOnRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementForm.ExpiresOnDisplayName) },

                { CurrentStatusLabel, "input#Status" },
                { CurrentStatusLabel.AutoCompleteMenuKey(), ".Status-field .autocomplete-menu ul" },
                { CurrentStatusLabel.ComboBoxDownArrowKey(), ".Status-field button" },
                { CurrentStatusLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Status]" },
                { CurrentStatusLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementForm.StatusRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementForm.StatusDisplayName) },

                { ContactsLabel.CollectionItemTextKey(), "ul#contacts_list li.agreement-contact .type-and-name" },

                { FileAttachmentsLabel, "ul#file_upload .file-chooser input[type='file']" },
                { FileAttachmentsLabel.CollectionItemTextKey(), "ul#file_attachments li.file-attachment .file-chosen .file-name" },
                { FileAttachmentsLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=PostedFile]" },
                { FileAttachmentsLabel.ErrorTextKey("Invalid"), InstitutionalAgreementFileForm.InvalidExtensionErrorText },

                { ErrorSummaryKey, "div[data-valmsg-summary=true]" },
            };

        public override IDictionary<string, string> Fields
        {
            get
            {
                var fileAttachmentRemoveIcons = new[]
                {
                    "LargePdf33.8.pdf"
                };
                foreach (var fileAttachmentRemoveIcon in fileAttachmentRemoveIcons)
                {
                    var key = FileAttachmentsLabel.CollectionItemRemoveKey(fileAttachmentRemoveIcon);
                    if (!FieldCss.ContainsKey(key))
                        FieldCss.Add(key, @"ul#file_attachments li.file-attachment .file-chosen a.remove-button[data-file-name=""{0}""]"
                            .FormatWith(fileAttachmentRemoveIcon));
                }

                var contactRemoveIcons = new[]
                {
                    "Partner Principal Mitch Leventhal",
                    "Home Principal Mitch Leventhal",
                    "Partner Secondary Mitch Leventhal",
                    "Home Principal Ron Cushing",
                    "Partner Principal Mary Watkins",
                    "Partner Secondary Mary Watkins",
                    "Home Principal Brandon Lee",
                };
                foreach (var contactRemoveIcon in contactRemoveIcons)
                {
                    var key = ContactsLabel.CollectionItemRemoveKey(contactRemoveIcon);
                    if (!FieldCss.ContainsKey(key))
                        FieldCss.Add(key, @"ul#contacts_list li.agreement-contact a.remove-button[data-contact-type-and-display-name=""{0}""]"
                            .FormatWith(contactRemoveIcon));
                }

                return FieldCss;
            }
        }
    }
}
