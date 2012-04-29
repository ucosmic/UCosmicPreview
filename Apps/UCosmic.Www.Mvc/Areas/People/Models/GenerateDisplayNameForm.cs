using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    public class GenerateDisplayNameForm
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
    }

    public static class GenerateDisplayNameProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(GenerateDisplayNameProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class ViewModelToQueryProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<GenerateDisplayNameForm, GenerateDisplayNameQuery>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}