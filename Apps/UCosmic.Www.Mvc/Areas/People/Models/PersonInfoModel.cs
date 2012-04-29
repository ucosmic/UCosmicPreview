using System;
using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    public class PersonInfoModel
    {
        public Guid EntityId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string DefaultEmail { get; set; }
    }

    public static class PersonInfoProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(PersonInfoProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Person, PersonInfoModel>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}