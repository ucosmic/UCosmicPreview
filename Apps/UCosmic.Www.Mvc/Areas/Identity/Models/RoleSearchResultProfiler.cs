using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class RoleSearchResultProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(RoleSearchResultProfiler));
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