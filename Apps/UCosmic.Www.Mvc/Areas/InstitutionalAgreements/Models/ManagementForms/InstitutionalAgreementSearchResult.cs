using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementSearchResult
    {
        public InstitutionalAgreementSearchResult()
        {
            EntityId = Guid.NewGuid();
        }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [Display(Name = "Summary description")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Display(Name = "Starts on")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? StartsOn { get; set; }

        [Display(Name = "Expires on")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? ExpiresOn { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Files")]
        public IList<InstitutionalAgreementFileInfo> Files { get; set; }
    }
}
