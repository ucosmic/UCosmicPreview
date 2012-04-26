using System.Linq;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class RoleFormProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(RoleFormProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModel : Profile
        {
            protected override void Configure()
            {
                CreateMap<Role, RoleForm>()
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())
                    .ForMember(d => d.Grants, o => o.MapFrom(s => s.Grants.OrderBy(g => g.User.Name)))
                ;
                CreateMap<RoleGrant, RoleForm.RoleGrantForm>();
                CreateMap<User, RoleForm.RoleGrantForm.UserForm>();
                CreateMap<User, RoleForm.RoleGrantForm>()
                    .ForMember(target => target.User, opt => opt
                        .ResolveUsing(source => new RoleForm.RoleGrantForm.UserForm
                        {
                            EntityId = source.EntityId,
                            Name = source.Name
                        }))
                ;
                CreateMap<User, AutoCompleteOption>()
                    .ForMember(target => target.value, opt => opt
                        .ResolveUsing(source => source.EntityId.ToString()))
                    .ForMember(target => target.label, opt => opt
                        .ResolveUsing(source => source.Name))
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}