using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace UCosmic.Www.Mvc.Models
{
    public static class AutoMapperRegistration
    {
        public static void RegisterProfiles(params Assembly[] assemblies)
        {
            RegisterProfiles(assemblies.Length < 1 ? null : assemblies.AsEnumerable());
        }

        public static void RegisterProfiles(IEnumerable<Assembly> assemblies)
        {
            Mapper.Initialize(configuration => GetConfiguration(Mapper.Configuration, assemblies));
        }

        private static void GetConfiguration(IConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            assemblies = assemblies
                //?? AppDomain.CurrentDomain.GetAssemblies()
                ?? new[] { Assembly.GetExecutingAssembly() }
            ;
            foreach (var assembly in assemblies)
            {
                var profileClasses = assembly.GetTypes()
                    .Where(t => t != typeof(Profile) && typeof(Profile).IsAssignableFrom(t))
                    .ToArray()
                ;
                foreach (var profileClass in profileClasses)
                {
                    configuration.AddProfile((Profile)Activator.CreateInstance(profileClass));
                }
            }
        }

        //private static readonly ICollection<Profile> ProfilesToRegister;

        //static AutoMapperRegistration()
        //{
        //    ProfilesToRegister = new List<Profile>();
        //}

        //public static void AddProfiles(IEnumerable<Profile> profiles)
        //{
        //    foreach (var profile in profiles)
        //        ProfilesToRegister.Add(profile);
        //}

        //// We are no longer putting mapping configurations here.
        //// Instead, put your mapping configurations in the appropriate
        //// ModelMapper.cs file.  For example, mappings for the ManagementForms
        //// controller in the Establishments area will go in the
        //// ~/Areas/Establishments/Mappers/ManagementFormsModelMapper.cs file.
        //public static void RegisterAllProfiles()
        //{
        //    RootModelProfiler.RegisterProfiles();
        //    Mapper.Initialize(configuration =>
        //    {
        //        foreach (var profile in ProfilesToRegister)
        //            configuration.AddProfile(profile);
        //    });
        //}
    }
}