using System;
using System.Collections.Generic;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementDeriveTitleInput
    {
        public bool IsTitleDerived { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public List<Guid> ParticipantEstablishmentIds { get; set; }
    }

    public static class InstitutionalAgreementDeriveTitleProfiler
    {
        public class ModelToQueryProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementDeriveTitleInput, GenerateTitleQuery>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.ParticipantGuids, o => o.MapFrom(s => s.ParticipantEstablishmentIds))
                ;
            }
        }
    }
}