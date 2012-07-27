using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementMappedSearchResult
    {
        public InstitutionalAgreementMappedSearchResult()
        {
            EntityId = Guid.NewGuid();
        }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [Display(Name = "Summary description")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        public virtual double Latitude { get; set; }

        public virtual double Longitude { get; set; }

    }
}
