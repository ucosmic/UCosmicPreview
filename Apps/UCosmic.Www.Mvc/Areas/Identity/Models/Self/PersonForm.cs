using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Self
{
    [DisplayColumn("DisplayName", "DisplayName", false)]
    public class PersonForm
    {
        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [Display(Name = "Display name", Prompt = "Please enter a Display name.")]
        [Required(ErrorMessage = "You must have a {0}.")]
        public string DisplayName { get; set; }

        [Display(Name = "Automatically generate my display name based on the fields below.")]
        public bool IsDisplayNameDerived { get; set; }

        [Display(Name = "Salutation")]
        [DisplayFormat(NullDisplayText = "[None]")]
        public string Salutation { get; set; }

        [Display(Name = "First name")]
        [DisplayFormat(NullDisplayText = "[Unknown]")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name or initial")]
        [DisplayFormat(NullDisplayText = "[None]")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        [DisplayFormat(NullDisplayText = "[Unknown]")]
        public string LastName { get; set; }

        [Display(Name = "Suffix")]
        [DisplayFormat(NullDisplayText = "[None]")]
        public string Suffix { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? UserId { get; set; }

        public IList<EmailInfo> Emails { get; set; }

        public IList<AffiliationInfo> Affiliations { get; set; }
        public class AffiliationInfo
        {
            public Guid EntityId { get; set; }

            [DisplayFormat(NullDisplayText = "[Job Title(s) Unknown]")]
            public string JobTitles { get; set; }

            public EstablishmentInfo Establishment { get; set; }
            public class EstablishmentInfo
            {
                public string OfficialName { get; set; }
                public bool IsInstitution { get; set; }
            }

            public bool IsAcknowledged { get; set; }
            public bool IsClaimingStudent { get; set; }
            public bool IsClaimingEmployee { get; set; }
        }
    }
}