using System.Collections.Generic;
using AutoMapper;

namespace UCosmic.Www.Mvc.Models
{
    public static class AutoMapperRegistration
    {
        private static readonly ICollection<Profile> ProfilesToRegister;

        static AutoMapperRegistration()
        {
            ProfilesToRegister = new List<Profile>();
        }

        public static void AddProfiles(IEnumerable<Profile> profiles)
        {
            foreach (var profile in profiles)
                ProfilesToRegister.Add(profile);
        }

        // We are no longer putting mapping configurations here.
        // Instead, put your mapping configurations in the appropriate
        // ModelMapper.cs file.  For example, mappings for the ManagementForms
        // controller in the Establishments area will go in the
        // ~/Areas/Establishments/Mappers/ManagementFormsModelMapper.cs file.
        public static void RegisterAllProfiles()
        {
            RootModelProfiler.RegisterProfiles();
            Mapper.Initialize(configuration =>
            {
                foreach (var profile in ProfilesToRegister)
                    configuration.AddProfile(profile);
            });
        }
    }
}