using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementForm : IReturnUrl
    {
        public InstitutionalAgreementForm()
        {
            Participants = new List<InstitutionalAgreementParticipantForm>();
            Contacts = new List<InstitutionalAgreementContactForm>();
            Files = new List<InstitutionalAgreementFileForm>();
            Umbrella = new UmbrellaForm();
        }

        [ScaffoldColumn(false)]
        public bool IsNew { get { return RevisionId == 0; } }

        public string ReturnUrl { get; set; }

        #region Identification

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        #endregion
        #region Umbrella Agreement

        //public int? UmbrellaRevisionId { get; set; }

        //public SelectList UmbrellaOptions { get; set; }

        public UmbrellaForm Umbrella { get; set; }
        public class UmbrellaForm
        {
            [Display(Name = "Umbrella agreement")]
            [DisplayFormat(NullDisplayText = "[None - this is a top-level or standalone agreement]")]
            public Guid? EntityId { get; set; }

            [ScaffoldColumn(false)]
            public IEnumerable<SelectListItem> Options { get; set; } 
        }

        #endregion
        #region Type and Status

        [UIHint("TypeComboBox")]
        [Display(Name = "Agreement type")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(150, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        [AllowedType(ErrorMessage = "Agreement type '{0}' is not allowed. Please select an Agreement type from the list provided.")]
        public string Type { get; set; }

        [UIHint("StatusComboBox")]
        [Display(Name = "Current status")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        [AllowedStatus(ErrorMessage = "Current status '{0}' is not allowed. Please select a Current status from the list provided.")]
        public string Status { get; set; }

        #endregion
        #region Titles & Description

        [Display(Name = "Check this box to automatically generate the summary description based on the agreement type, participants, and status.")]
        public bool IsTitleDerived { get; set; }

        [Display(Name = "Summary description")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(500, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Display(Name = "Additional notes")]
        [StringLength(4000, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        #endregion
        #region Lifetime

        [Display(Name = "Start date")]
        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.Text)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? StartsOn { get; set; }

        [Display(Name = "Expiration date")]
        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.Text)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? ExpiresOn { get; set; }

        [Display(Name = "This expiration date is estimated (agreement should be reviewed by this date).")]
        public bool IsExpirationEstimated { get; set; }

        [Display(Name = "Auto renew")]
        public bool? IsAutoRenew { get; set; }

        #endregion
        #region Collections

        [ScaffoldColumn(false)]
        [AtLeastOneOwner(ErrorMessage = "You must be affiliated with at least one agreement participant.")]
        public IList<InstitutionalAgreementParticipantForm> Participants { get; set; }

        [ScaffoldColumn(false)]
        public IList<InstitutionalAgreementContactForm> Contacts { get; set; }

        [ScaffoldColumn(false)]
        public IList<InstitutionalAgreementFileForm> Files { get; set; }

        #endregion
    }
}