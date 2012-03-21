using System.Linq;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models.Roles;
using UCosmic.Www.Mvc.Mappers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class RolesModelMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(RolesModelMapper));
        }

        // ReSharper disable UnusedMember.Local

        private class RoleFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Role, RoleForm>()
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())
                    .ForMember(d => d.Grants, o => o.MapFrom(s => s.Grants.OrderBy(g => g.User.UserName)))
                ;
                CreateMap<RoleGrant, RoleForm.RoleGrantForm>();
                CreateMap<User, RoleForm.RoleGrantForm.UserForm>();
                CreateMap<User, RoleForm.RoleGrantForm>()
                    .ForMember(target => target.User, opt => opt
                        .ResolveUsing(source => new RoleForm.RoleGrantForm.UserForm
                        {
                            EntityId = source.EntityId, UserName = source.UserName
                        }))
                ;
                CreateMap<User, AutoCompleteOption>()
                    .ForMember(target => target.value, opt => opt
                        .ResolveUsing(source => source.EntityId.ToString()))
                    .ForMember(target => target.label, opt => opt
                        .ResolveUsing(source => source.UserName))
                ;
            }
        }

        private class RoleSearchResultProfile : Profile
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