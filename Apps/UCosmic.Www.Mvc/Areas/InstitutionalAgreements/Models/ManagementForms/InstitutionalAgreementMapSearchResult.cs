using System;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementMapSearchResult
    {
        public Guid EntityId { get; set; }
        public string Title { get; set; }
    }

    public static class InstitutionalAgreementMapSearchResultProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreement, InstitutionalAgreementMapSearchResult>();
            }
        }
    }
}