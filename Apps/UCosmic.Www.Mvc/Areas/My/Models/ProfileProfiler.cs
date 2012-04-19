using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public static class ProfileProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ProfileProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Person, ProfileInfo>()
                    .ForMember(d => d.Emails, o => o.MapFrom(s => 
                        Mapper.Map<IEnumerable<ProfileInfo.EmailInfo>>(s.Emails
                            .OrderByDescending(e => e.IsDefault)
                            .ThenByDescending(e => e.IsConfirmed)
                            .ThenBy(e => e.Value)
                        )
                    ))
                ;

                CreateMap<EmailAddress, ProfileInfo.EmailInfo>();

                CreateMap<Affiliation, ProfileInfo.AffiliationInfo>();

                CreateMap<Establishment, ProfileInfo.AffiliationInfo.EstablishmentInfo>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}