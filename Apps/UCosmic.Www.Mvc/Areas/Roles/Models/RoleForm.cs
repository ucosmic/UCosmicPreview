using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Roles.Models
{
    public class RoleForm : IReturnUrl
    {
        public RoleForm()
        {
            Grants = new List<RoleGrantForm>();
        }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [Display(Name = "Role name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(200, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        public string Name { get; set; }

        [Display(Name = DescriptionDisplayName)]
        [Required(ErrorMessage = DescriptionRequiredErrorFormat)]
        [StringLength(4000, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public const string DescriptionDisplayName = "Description";
        public const string DescriptionRequiredErrorFormat = "{0} is required.";

        [ScaffoldColumn(false)]
        [NoDuplicateGrants(ErrorMessage = "The user '{0}' has already been added to this role.")]
        public IList<RoleGrantForm> Grants { get; set; }
        public class RoleGrantForm
        {
            public UserForm User { get; set; }
            public class UserForm
            {
                public Guid EntityId { get; set; }
                public string Name { get; set; }
            }

            [HiddenInput(DisplayValue = false)]
            public bool IsDeleted { get; set; }
        }
    }

    public static class RoleFormProfiler
    {
        public class EntityToModelProfile : Profile
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
    }
}