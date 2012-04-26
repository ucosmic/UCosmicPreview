using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class Saml2IntegrationProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(Saml2IntegrationProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                // entities to models
                CreateMap<Establishment, Saml2IntegrationInfo>();
                CreateMap<EstablishmentEmailDomain, Saml2IntegrationInfo.EmailDomainInfo>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}