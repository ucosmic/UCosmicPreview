using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms
{
    public class EstablishmentSearchResult
    {
        public EstablishmentSearchResult()
        {
            EntityId = Guid.NewGuid();
        }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [Display(Name = "Official Name")]
        [DataType(DataType.MultilineText)]
        public string OfficialName { get; set; }

        [Display(Name = "WebsiteUrl")]
        [DataType(DataType.Text)]
        public string WebsiteUrl { get; set; }

    }
}