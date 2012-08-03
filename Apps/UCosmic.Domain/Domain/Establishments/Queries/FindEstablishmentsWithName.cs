using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace UCosmic.Domain.Establishments
{
    public class FindEstablishmentsWithNameQuery : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment[]>
    {
        public string Term { get; set; }
        public int MaxResults { get; set; }
        public StringMatchStrategy TermMatchStrategy { get; set; }
    }

    public class FindEstablishmentsWithNameHandler : IHandleQueries<FindEstablishmentsWithNameQuery, Establishment[]>
    {
        private readonly IQueryEntities _entities;

        public FindEstablishmentsWithNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment[] Handle(FindEstablishmentsWithNameQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (string.IsNullOrWhiteSpace(query.Term))
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Term", "Term cannot be null or white space string", query.Term),
                });

            if (query.MaxResults < 0)
                throw new ValidationException(new[]
                {
                    new ValidationFailure("MaxResults", "MaxResults must be greater than or equal to zero", query.MaxResults),
                });

            var results = _entities.Query<Establishment>()
                .EagerLoad(_entities, query.EagerLoad)
                .WithNameOrUrl(query.Term, query.TermMatchStrategy)
                .OrderBy(query.OrderBy);

            if (query.MaxResults > 0)
                results = results.Take(query.MaxResults);

            return results.ToArray();
        }
    }
}
