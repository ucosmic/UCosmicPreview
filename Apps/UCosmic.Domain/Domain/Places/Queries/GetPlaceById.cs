using System;

namespace UCosmic.Domain.Places
{
    public class GetPlaceByIdQuery : BaseEntityQuery<Place>, IDefineQuery<Place>
    {
        public int Id { get; set; }
    }

    public class GetPlaceByIdHandler : IHandleQueries<GetPlaceByIdQuery, Place>
    {
        private readonly IQueryEntities _entities;

        public GetPlaceByIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Place Handle(GetPlaceByIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<Place>()
                .EagerLoad(query.EagerLoad, _entities)
                .ById(query.Id)
            ;

            return result;
        }
    }
}
