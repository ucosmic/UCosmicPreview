using System;

namespace UCosmic.Domain.Identity
{
    public class GetRoleBySlugQuery : BaseEntityQuery<Role>, IDefineQuery<Role>
    {
        public GetRoleBySlugQuery(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Cannot be null or white space", "slug");
            Slug = slug;
        }

        public string Slug { get; private set; }
        internal string RoleName { get { return Slug.Replace("-", " "); } }
    }

    public class GetRoleBySlugHandler : IHandleQueries<GetRoleBySlugQuery, Role>
    {
        private readonly IQueryEntities _entities;

        public GetRoleBySlugHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Role Handle(GetRoleBySlugQuery query)
        {
            return _entities.Query<Role>()
                .EagerLoad(query.EagerLoad, _entities)
                .By(query.RoleName)
            ;
        }
    }
}
