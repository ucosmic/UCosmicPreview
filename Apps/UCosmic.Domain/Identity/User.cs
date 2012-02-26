using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UCosmic.Domain.Identity
{
    public class User : RevisableEntity
    {
        public User()
        {
            _grants = new List<RoleGrant>();
        }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public bool IsRegistered { get; set; }

        private ICollection<RoleGrant> _grants;
        public virtual ICollection<RoleGrant> Grants
        {
            get { return _grants; }
            set { _grants = value; }
        }
    }

}