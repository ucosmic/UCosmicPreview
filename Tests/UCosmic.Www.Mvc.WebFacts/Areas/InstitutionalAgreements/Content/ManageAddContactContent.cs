using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public class ManageAddContactContent : Content
    {
        public ManageAddContactContent(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Add Institutional Agreement Contact"; } }

        public const string ContactTypeLabel = "Contact Type";
        public const string EmailAddressLabel = "Email Address";
        public const string FirstNameLabel = "First Name";
        public const string LastNameLabel = "Last Name";
        public const string SalutationLabel = "Salutation";
        public const string MiddleNameLabel = "Middle Name Or Initial";
        public const string SuffixLabel = "Suffix";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { ContactTypeLabel, "input#ContactType" },
                { ContactTypeLabel.AutoCompleteMenuKey(), ".ContactType-field .autocomplete-menu ul" },
                { ContactTypeLabel.ComboBoxDownArrowKey(), ".ContactType-field button" },
                { ContactTypeLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=ContactType]" },
                { ContactTypeLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementContactForm.ContactTypeRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementContactForm.ContactTypeDisplayName) },

                { EmailAddressLabel, "input#Person_DefaultEmail" },
                { EmailAddressLabel.AutoCompleteMenuKey(), ".Person_DefaultEmail-field .autocomplete-menu ul" },

                { FirstNameLabel, "input#Person_FirstName" },
                { FirstNameLabel.AutoCompleteMenuKey(), ".Person_FirstName-field .autocomplete-menu ul" },
                { FirstNameLabel.ErrorKey(), ".field-validation-error[data-valmsg-for='Person.FirstName']" },
                { FirstNameLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementContactForm.PersonForm.FirstNameRequiredErrorText },

                { LastNameLabel, "input#Person_LastName" },
                { LastNameLabel.AutoCompleteMenuKey(), ".Person_LastName-field .autocomplete-menu ul" },
                { LastNameLabel.ErrorKey(), ".field-validation-error[data-valmsg-for='Person.LastName']" },
                { LastNameLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementContactForm.PersonForm.LastNameRequiredErrorText },

                { SalutationLabel, "input#Person_Salutation" },
                { MiddleNameLabel, "input#Person_MiddleName" },
                { SuffixLabel, "input#Person_Suffix" },
            };

        public override IDictionary<string, string> Fields
        {
            get { return FieldCss; }
        }
    }
}
