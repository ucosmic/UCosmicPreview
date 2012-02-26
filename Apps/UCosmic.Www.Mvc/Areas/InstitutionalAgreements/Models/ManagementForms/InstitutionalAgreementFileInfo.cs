using System;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementFileInfo
    {
        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public int? AgreementId { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }
        public int Length { get; set; }

        public Guid EntityId { get; set; }
        public Guid AgreementEntityId { get; set; }
    }
}