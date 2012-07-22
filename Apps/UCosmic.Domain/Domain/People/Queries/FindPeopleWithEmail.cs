using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace UCosmic.Domain.People
{
    public class FindPeopleWithEmailQuery : BaseEntitiesQuery<Person>, IDefineQuery<Person[]>
    {
        public string Term { get; set; }
        public StringMatchStrategy TermMatchStrategy { get; set; }
    }

    public class FindPeopleWithEmailHandler : IHandleQueries<FindPeopleWithEmailQuery, Person[]>
    {
        private readonly IQueryEntities _entities;

        public FindPeopleWithEmailHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Person[] Handle(FindPeopleWithEmailQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (string.IsNullOrWhiteSpace(query.Term))
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Term", "Term cannot be null or white space string", query.Term),
                });

            var results = _entities.Query<Person>()
                .EagerLoad(query.EagerLoad, _entities)
                .WithEmail(query.Term, query.TermMatchStrategy)
                .OrderBy(query.OrderBy)
            ;

            return results.ToArray();
        }
    }
}
