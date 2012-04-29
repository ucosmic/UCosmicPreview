using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class Saml2IntegrationInfo
    {
        public string OfficialName { get; set; }
        public EmailDomainInfo[] EmailDomains { get; set; }
        public class EmailDomainInfo
        {
            public string Value { get; set; }
        }
    }

    public static class Saml2IntegrationProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(Saml2IntegrationProfiler));
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
