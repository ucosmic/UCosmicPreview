using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementParticipant : Entity
    {
        public int Id { get; set; }

        public virtual InstitutionalAgreement Agreement { get; set; }

        public virtual Establishment Establishment { get; set; }

        public bool IsOwner { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}",
                IsOwner ? "Owner: " : "Non-Owner: ",
                Establishment.OfficialName);
        }

        //internal int Remove(ICommandEntities entities)
        //{
        //    entities.Purge(this);
        //    return 1;
        //}

        //internal int Remove(ICommandObjects commander)
        //{
        //    commander.Delete(this);
        //    return 1;
        //}

        //internal void DeriveIsOwner(IPrincipal principal)
        //{
        //    Expression<Func<Affiliation, bool>> principalDefaultAffiliation =
        //        affiliation => affiliation.IsDefault && affiliation.Person.User != null &&
        //                       affiliation.Person.User.Name.Equals(principal.Identity.Name,
        //                                                               StringComparison.OrdinalIgnoreCase);
        //    IsOwner = 
        //        Establishment.Affiliates.AsQueryable().Any(principalDefaultAffiliation) 
        //        ||
        //        Establishment.Ancestors.Any(n => n.Ancestor.Affiliates.AsQueryable().Any(principalDefaultAffiliation));
        //}

    }
}