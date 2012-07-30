using System;
using AutoMapper;
using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Roles.Models
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
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Role, RoleSearchResult>()
                    .ForMember(d => d.Slug, o => o.ResolveUsing(s => s.Name.Replace(" ", "-").ToLower()))
                ;
            }
        }
    }
}