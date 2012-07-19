using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UCosmic.Domain.Identity
{
    public class Role : RevisableEntity
    {
        #region Construction

        private ICollection<RoleGrant> _grants;

        protected internal Role()
        {
            _grants = _grants ?? new Collection<RoleGrant>();
        }

        #endregion
        #region Scalars

        public string Name { get; protected internal set; }

        public string Description { get; protected internal set; }

        #endregion
        #region Collections

        public virtual ICollection<RoleGrant> Grants
        {
            get { return _grants; }
            protected internal set { _grants = value; }
        }

        #endregion
        //#region Operations

        ////internal int RevokeUser(Guid userEntityId, ICommandEntities commander)
        ////{
        ////    var grant = Grants.ByUser(userEntityId);
        ////    return (grant != null) ? grant.Revoke(commander) : 0;
        ////}

        ////internal int GrantUser(Guid userEntityId, IQueryEntities query)
        ////{
        ////    var grant = Grants.ByUser(userEntityId);
        ////    if (grant != null) return 0;

        ////    var user = query.Users.By(userEntityId);
        ////    if (user == null)
        ////        throw new InvalidOperationException(string.Format(
        ////            "Unable to find User with EntityId '{0}'.", userEntityId));

        ////    Grants.Add(new RoleGrant { User = user, });
        ////    return 1;
        ////}

        //#endregion
    }
}