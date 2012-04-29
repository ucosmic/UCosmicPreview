using System;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class RoleSearchResult
    {
        public int RevisionId { get; set; }
        public Guid EntityId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }

    public static class RoleSearchResultProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(RoleSearchResultProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Role, RoleSearchResult>()
                    .ForMember(d => d.Slug, o => o.ResolveUsing(s => s.Name.Replace(" ", "-").ToLower()))
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}