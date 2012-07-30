using System;
using System.Web.Mvc;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Www.Mvc.Models;
using AutoMapper;

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
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(InstitutionalAgreementParticipantFormProfiler));
        }

        internal class EntityToModelProfile : Profile
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