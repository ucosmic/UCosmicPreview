using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class MyHomeInfo
    {
        public string UserEduPersonTargetedId { get; set; }

        public bool CanChangePassword
        {
            get { return string.IsNullOrWhiteSpace(UserEduPersonTargetedId); }
        }

        public MyEmailAddress[] Emails { get; set; }
        public class MyEmailAddress
        {
            public int Number { get; set; }
            public string Value { get; set; }
            public bool IsDefault { get; set; }
            public bool IsConfirmed { get; set; }
        }

        public MyAffiliation[] Affiliations { get; set; }
        public class MyAffiliation
        {
            public int EstablishmentId { get; set; }

            [DisplayFormat(NullDisplayText = JobTitlesNullDisplayText)]
            public string JobTitles { get; set; }
            public const string JobTitlesNullDisplayText = "[Job Title(s) Unknown]";

            public bool IsAcknowledged { get; set; }

            public bool IsClaimingStudent { get; set; }

            public bool IsClaimingEmployee { get; set; }

            public EstablishmentModel Establishment { get; set; }
            public class EstablishmentModel
            {
                public string OfficialName { get; set; }

                public bool IsInstitution { get; set; }
            }
        }
    }

    public static class MyHomeProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(MyHomeProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Person, MyHomeInfo>()
                    .ForMember(d => d.Emails, o => o.MapFrom(s =>
                        Mapper.Map<IEnumerable<MyHomeInfo.MyEmailAddress>>(s.Emails
                            .OrderByDescending(e => e.IsDefault)
                            .ThenByDescending(e => e.IsConfirmed)
                            .ThenBy(e => e.Value)
                        )
                    ))
                ;

                CreateMap<EmailAddress, MyHomeInfo.MyEmailAddress>();

                CreateMap<Affiliation, MyHomeInfo.MyAffiliation>();

                CreateMap<Establishment, MyHomeInfo.MyAffiliation.EstablishmentModel>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}