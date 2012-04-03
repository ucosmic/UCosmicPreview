using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementParticipant : Entity
    {
        public int Id { get; set; }

        public virtual InstitutionalAgreement Agreement { get; set; }

        public virtual Establishment Establishment { get; set; }

        public bool IsOwner { get; set; }

        internal int Remove(ICommandObjects commander)
        {
            commander.Delete(this);
            return 1;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}",
                IsOwner ? "Owner: " : "Non-Owner: ",
                Establishment.OfficialName);
        }

        internal void DeriveIsOwner(IPrincipal principal)
        {
            Expression<Func<Affiliation, bool>> principalDefaultAffiliation =
                affiliation => affiliation.IsDefault && affiliation.Person.User != null &&
                               affiliation.Person.User.UserName.Equals(principal.Identity.Name,
                                                                       StringComparison.OrdinalIgnoreCase);
            IsOwner = 
                Establishment.Affiliates.AsQueryable().Any(principalDefaultAffiliation) 
                ||
                Establishment.Ancestors.Any(n => n.Ancestor.Affiliates.AsQueryable().Any(principalDefaultAffiliation));
        }

    }
}