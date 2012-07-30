using System;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementParticipantForm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EstablishmentEntityId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        public string EstablishmentOfficialName { get; set; }

        public string EstablishmentTranslatedNameText { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsOwner { get; set; }

    }

    public static class InstitutionalAgreementParticipantFormProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementParticipant, InstitutionalAgreementParticipantForm>()
                    .ForMember(target => target.IsDeleted, o => o.Ignore())
                ;
            }
        }
    }
}