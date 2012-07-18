using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace UCosmic.Domain.People
{
    public class FindPeopleWithLastNameQuery : BasePeopleQuery, IDefineQuery<Person[]>
    {
        public string Term { get; set; }
        public StringMatchStrategy TermMatchStrategy { get; set; }
    }

    public class FindPeopleWithLastNameHandler : IHandleQueries<FindPeopleWithLastNameQuery, Person[]>
    {
        private readonly IQueryEntities _entities;

        public FindPeopleWithLastNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Person[] Handle(FindPeopleWithLastNameQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (string.IsNullOrWhiteSpace(query.Term))
                throw new ValidationException(new[]
                {
                    new ValidationFailure("Term", "Term cannot be null or white space string", query.Term),
                });

            var results = _entities.People
                .EagerLoad(query.EagerLoad, _entities)
                .WithLastName(query.Term, query.TermMatchStrategy)
                .OrderBy(query.OrderBy)
            ;

            return results.ToArray();
        }
    }
}
