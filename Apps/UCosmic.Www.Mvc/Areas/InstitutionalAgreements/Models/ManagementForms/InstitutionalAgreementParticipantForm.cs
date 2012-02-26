using System;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementParticipantForm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public int AgreementId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EstablishmentEntityId { get; set; }
        //public int EstablishmentId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        public string EstablishmentOfficialName { get; set; }

        public string EstablishmentTranslatedNameText { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsOwner { get; set; }

    }
}