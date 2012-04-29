using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class ProfileInfo
    {
        public string UserEduPersonTargetedId { get; set; }
        public bool CanChangePassword
        {
            get { return string.IsNullOrWhiteSpace(UserEduPersonTargetedId); }
        }

        public EmailInfo[] Emails { get; set; }
        public class EmailInfo
        {
            public int Number { get; set; }
            public string Value { get; set; }
            public bool IsDefault { get; set; }
            public bool IsConfirmed { get; set; }
        }

        public AffiliationInfo[] Affiliations { get; set; }
        public class AffiliationInfo
        {
            public int EstablishmentId { get; set; }

            public const string JobTitlesNullDisplayText = "[Job Title(s) Unknown]";
            [DisplayFormat(NullDisplayText = JobTitlesNullDisplayText)]
            public string JobTitles { get; set; }

            public bool IsAcknowledged { get; set; }
            public bool IsClaimingStudent { get; set; }
            public bool IsClaimingEmployee { get; set; }

            public EstablishmentInfo Establishment { get; set; }
            public class EstablishmentInfo
            {
                public string OfficialName { get; set; }
                public bool IsInstitution { get; set; }
            }
        }
    }

    public static class ProfileProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ProfileProfiler));
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