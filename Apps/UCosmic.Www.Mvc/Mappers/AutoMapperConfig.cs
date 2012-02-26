using System.Collections.Generic;
using AutoMapper;

namespace UCosmic.Www.Mvc.Mappers
{
    public static class AutoMapperConfig
    {
        private static readonly ICollection<Profile> RegisteredProfiles;

        static AutoMapperConfig()
        {
            RegisteredProfiles = new List<Profile>();
        }

        public static void RegisterProfiles(IEnumerable<Profile> profiles)
        {
            foreach (var profile in profiles)
                RegisteredProfiles.Add(profile);
        }

        // We are no longer putting mapping configurations here.
        // Instead, put your mapping configurations in the appropriate
        // ModelMapper.cs file.  For example, mappings for the ManagementForms 
        // controller in the Establishments area will go in the 
        // ~/Areas/Establishments/Mappers/ManagementFormsModelMapper.cs file.
        public static void Configure()
        {
            DefaultModelMapper.RegisterProfiles();
            Mapper.Initialize(configuration =>
            {
                foreach (var profile in RegisteredProfiles)
                    configuration.AddProfile(profile);
            });
        }
    }
}