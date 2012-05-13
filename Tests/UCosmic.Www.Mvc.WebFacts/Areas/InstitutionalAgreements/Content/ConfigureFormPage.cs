using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    public abstract class ConfigureFormPage : Page
    {
        public ConfigureFormPage(IWebDriver browser) : base(browser) { }
        public const string AgreementTypesLabel = "Agreement Types";
        public const string AgreementTypeLabel = "Agreement Type";
        public const string AgreementTypeBehaviorLabel = "Agreement Type Behavior";
        public const string AgreementTypeExampleLabel = "Agreement Type Example";
        public const string CurrentStatusesLabel = "Current Statuses";
        public const string CurrentStatusLabel = "Current Status";
        public const string CurrentStatusBehaviorLabel = "Current Status Behavior";
        public const string CurrentStatusExampleLabel = "Current Status Example";
        public const string ContactTypesLabel = "Contact Types";
        public const string ContactTypeLabel = "Contact Type";
        public const string ContactTypeBehaviorLabel = "Contact Type Behavior";
        public const string ContactTypeExampleLabel = "Contact Type Example";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { AgreementTypesLabel.CollectionItemKey(CollectionItemToken), "ul#allowed_types_list li" },
                { AgreementTypesLabel.CollectionItemTextKey(), "ul#allowed_types_list li .reader .value" },
                { AgreementTypesLabel.CollectionItemClickKey("Add"), "ul#allowed_types_list .actions .add a" },

                { AgreementTypeLabel, "ul#allowed_types_list .editor .value input[type=text]" },
                { AgreementTypeLabel.ErrorKey(), "ul#allowed_types_list .editor .validate .field-validation-error[data-valmsg-for^='AllowedTypeValues['][data-valmsg-for$='].Text']" },
                { AgreementTypeLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementTypeValueForm.TextRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementTypeValueForm.TextDisplayName) },

                { AgreementTypeBehaviorLabel.RadioKey("Allow any text"), "input#IsCustomTypeAllowed_True" },
                { AgreementTypeBehaviorLabel.RadioKey("Use specific values"), "input#IsCustomTypeAllowed_False" },
                { AgreementTypeExampleLabel, "input#ExampleTypeInput" },

                { CurrentStatusesLabel.CollectionItemKey(CollectionItemToken), "ul#allowed_status_list li" },
                { CurrentStatusesLabel.CollectionItemTextKey(), "ul#allowed_status_list li .reader .value" },
                { CurrentStatusesLabel.CollectionItemClickKey("Add"), "ul#allowed_status_list .actions .add a" },

                { CurrentStatusLabel, "ul#allowed_status_list .editor .value input[type=text]" },
                { CurrentStatusLabel.ErrorKey(), "ul#allowed_status_list .editor .validate .field-validation-error[data-valmsg-for^='AllowedStatusValues['][data-valmsg-for$='].Text']" },
                { CurrentStatusLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementStatusValueForm.TextRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementStatusValueForm.TextDisplayName) },

                { CurrentStatusBehaviorLabel.RadioKey("Allow any text"), "input#IsCustomStatusAllowed_True" },
                { CurrentStatusBehaviorLabel.RadioKey("Use specific values"), "input#IsCustomStatusAllowed_False" },
                { CurrentStatusExampleLabel, "input#ExampleStatusInput" },

                { ContactTypesLabel.CollectionItemKey(CollectionItemToken), "ul#allowed_contacttype_list li" },
                { ContactTypesLabel.CollectionItemTextKey(), "ul#allowed_contacttype_list li .reader .value" },
                { ContactTypesLabel.CollectionItemClickKey("Add"), "ul#allowed_contacttype_list .actions .add a" },

                { ContactTypeLabel, "ul#allowed_contacttype_list .editor .value input[type=text]" },
                { ContactTypeLabel.ErrorKey(), "ul#allowed_contacttype_list .editor .validate .field-validation-error[data-valmsg-for^='AllowedContactTypeValues['][data-valmsg-for$='].Text']" },
                { ContactTypeLabel.ErrorTextKey("Required"),
                    InstitutionalAgreementContactTypeValueForm.TextRequiredErrorFormat
                        .FormatWith(InstitutionalAgreementContactTypeValueForm.TextDisplayName) },

                { ContactTypeBehaviorLabel.RadioKey("Allow any text"), "input#IsCustomContactTypeAllowed_True" },
                { ContactTypeBehaviorLabel.RadioKey("Use specific values"), "input#IsCustomContactTypeAllowed_False" },
                { ContactTypeExampleLabel, "input#ExampleContactTypeInput" },
            };

        public override IDictionary<string, string> Fields
        {
            get
            {
                var duplicateAgreementTypes = new[]
                {
                    "Activity Agreement",
                    "Institutional Collaboration Agreement",
                    "Memorandum of Understanding",
                };
                foreach (var duplicateAgreementType in duplicateAgreementTypes)
                {
                    var errorType = "'{0} is a Duplicate'".FormatWith(duplicateAgreementType);
                    var key = AgreementTypeLabel.ErrorTextKey(errorType);
                    if (!FieldCss.ContainsKey(key))
                        FieldCss.Add(key, InstitutionalAgreementConfigurationForm.DuplicateOptionErrorMessage
                            .FormatWith(duplicateAgreementType));
                }

                var duplicateAgreementStatuses = new[]
                {
                    "Active",
                    "Dead",
                    "Inactive",
                    "Unknown",
                };
                foreach (var duplicateAgreementStatus in duplicateAgreementStatuses)
                {
                    var errorType = "'{0} is a Duplicate'".FormatWith(duplicateAgreementStatus);
                    var key = CurrentStatusLabel.ErrorTextKey(errorType);
                    if (!FieldCss.ContainsKey(key))
                        FieldCss.Add(key, InstitutionalAgreementConfigurationForm.DuplicateOptionErrorMessage
                            .FormatWith(duplicateAgreementStatus));
                }

                var duplicateContactTypes = new[]
                {
                    "Home Principal",
                    "Home Secondary",
                    "Partner Principal",
                    "Partner Secondary",
                };
                foreach (var duplicateContactType in duplicateContactTypes)
                {
                    var errorType = "'{0} is a Duplicate'".FormatWith(duplicateContactType);
                    var key = ContactTypeLabel.ErrorTextKey(errorType);
                    if (!FieldCss.ContainsKey(key))
                        FieldCss.Add(key, InstitutionalAgreementConfigurationForm.DuplicateOptionErrorMessage
                            .FormatWith(duplicateContactType));
                }

                return FieldCss;
            }
        }
    }
}
