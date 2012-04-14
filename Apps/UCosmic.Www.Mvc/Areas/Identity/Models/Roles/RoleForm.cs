using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Roles
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

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(4000, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

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
}