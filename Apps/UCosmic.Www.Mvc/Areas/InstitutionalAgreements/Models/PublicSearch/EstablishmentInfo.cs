using System;
using AutoMapper;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.PublicSearch
{
    public class EstablishmentInfo
    {
        public Guid EntityId { get; set; }
        public string OfficialName { get; set; }
        public string WebsiteUrl { get; set; }
    }

    public static class EstablishmentInfoProfiler
    {
        public class EntitiesTModelsProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Establishment, EstablishmentInfo>();
            }
        }
    }
}