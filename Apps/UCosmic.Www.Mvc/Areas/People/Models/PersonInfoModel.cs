using System;
using AutoMapper;
using UCosmic.Domain.People;

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
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Person, PersonInfoModel>();
            }
        }
    }
}