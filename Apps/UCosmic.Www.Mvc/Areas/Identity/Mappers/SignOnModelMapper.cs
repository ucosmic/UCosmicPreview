using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignOn;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class SignOnModelMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(SignOnModelMapper));
        }

        // ReSharper disable UnusedMember.Local

        private class Saml2Integration : Profile
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