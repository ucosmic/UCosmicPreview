using System;

namespace UCosmic.Domain.Files
{
    public class GetLooseFileByGuidQuery : BaseEntityQuery<LooseFile>, IDefineQuery<LooseFile>
    {
        public GetLooseFileByGuidQuery(Guid guid)
        {
            if (guid == Guid.Empty) throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class GetLooseFileByGuidHandler : IHandleQueries<GetLooseFileByGuidQuery, LooseFile>
    {
        private readonly IQueryEntities _entities;

        public GetLooseFileByGuidHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public LooseFile Handle(GetLooseFileByGuidQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Get<LooseFile>()
                .ById(query.Guid)
            ;
        }
    }
}
