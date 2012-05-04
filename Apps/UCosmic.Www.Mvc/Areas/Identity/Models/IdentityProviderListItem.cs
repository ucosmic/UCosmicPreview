using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class IdentityProviderListItem
    {
        public string OfficialName { get; set; }
        public EmailDomain[] EmailDomains { get; set; }
        public class EmailDomain
        {
            public string Value { get; set; }
        }
    }

    public static class IdentityProviderListItemModelProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(IdentityProviderListItemModelProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                // entities to models
                CreateMap<Establishment, IdentityProviderListItem>();
                CreateMap<EstablishmentEmailDomain, IdentityProviderListItem.EmailDomain>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}
