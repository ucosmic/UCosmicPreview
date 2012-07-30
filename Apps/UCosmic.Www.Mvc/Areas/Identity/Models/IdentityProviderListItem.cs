using AutoMapper;
using UCosmic.Domain.Establishments;

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
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                // entities to models
                CreateMap<Establishment, IdentityProviderListItem>();
                CreateMap<EstablishmentEmailDomain, IdentityProviderListItem.EmailDomain>();
            }
        }
    }
}
