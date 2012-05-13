using OpenQA.Selenium;
using System.Collections.Generic;
using UCosmic.Www.Mvc.Areas.Identity.Models;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class UpdateNameFormContent : Content
    {
        public UpdateNameFormContent(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Update My Name"; } }

        public const string DisplayNameLabel = "Display Name";
        public const string IsDisplayNameDerivedLabel = "Automatically Generate Display Name";
        public const string FirstNameLabel = "First Name";
        public const string LastNameLabel = "Last Name";
        public const string SalutationLabel = "Salutation";
        public const string MiddleNameLabel = "Middle Name Or Initial";
        public const string SuffixLabel = "Suffix";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { DisplayNameLabel, "input#DisplayName" },
                { DisplayNameLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=DisplayName]" },
                { DisplayNameLabel.ErrorTextKey("Required"), UpdateNameValidator.FailedBecauseDisplayNameWasEmpty },

                { IsDisplayNameDerivedLabel, "input#IsDisplayNameDerived" },

                { SalutationLabel, "input#Salutation" },
                { SalutationLabel.AutoCompleteMenuKey(), ".Salutation-field .autocomplete-menu ul" },
                { SalutationLabel.ComboBoxDownArrowKey(), ".Salutation-field .text-box.down-arrow" },

                { FirstNameLabel, "input#FirstName" },
                { MiddleNameLabel, "input#MiddleName" },
                { LastNameLabel, "input#LastName" },

                { SuffixLabel, "input#Suffix" },
                { SuffixLabel.AutoCompleteMenuKey(), ".Suffix-field .autocomplete-menu ul" },
                { SuffixLabel.ComboBoxDownArrowKey(), ".Suffix-field .text-box.down-arrow" },
            };

        public override IDictionary<string, string> Fields
        {
            get { return FieldCss; }
        }
    }
}
