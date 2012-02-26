using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    public class InstitutionalAgreementConfigurationForm
    {
        private const string DuplicateOptionErrorMessage = "The option '{0}' already exists. Please do not add any duplicate options.";

        public InstitutionalAgreementConfigurationForm()
        {
            EntityId = Guid.NewGuid();
            IsCustomTypeAllowed = true;
            IsCustomStatusAllowed = true;
            IsCustomContactTypeAllowed = true;
            AllowedTypeValues = new List<InstitutionalAgreementTypeValueForm>();
            AllowedStatusValues = new List<InstitutionalAgreementStatusValueForm>();
            AllowedContactTypeValues = new List<InstitutionalAgreementContactTypeValueForm>();
        }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        public int? ForEstablishmentId { get; set; }
        public string ForEstablishmentOfficialName { get; set; }

        public bool IsCustomTypeAllowed { get; set; }
        public bool IsCustomStatusAllowed { get; set; }
        public bool IsCustomContactTypeAllowed { get; set; }

        [Display(Name = "Agreement types")]
        [NoDuplicateValues(IgnoreCase = true, ErrorMessage = DuplicateOptionErrorMessage)]
        public List<InstitutionalAgreementTypeValueForm> AllowedTypeValues { get; set; }

        [UIHint("TypeComboBox")]
        public string ExampleTypeInput { get; set; }

        [UIHint("StatusComboBox")]
        public string ExampleStatusInput { get; set; }

        [UIHint("ContactTypeComboBox")]
        public string ExampleContactTypeInput { get; set; }

        [Display(Name = "Agreement statuses")]
        [NoDuplicateValues(IgnoreCase = true, ErrorMessage = DuplicateOptionErrorMessage)]
        public List<InstitutionalAgreementStatusValueForm> AllowedStatusValues { get; set; }

        [Display(Name = "Contact types")]
        [NoDuplicateValues(IgnoreCase = true, ErrorMessage = DuplicateOptionErrorMessage)]
        public List<InstitutionalAgreementContactTypeValueForm> AllowedContactTypeValues { get; set; }
    }
}