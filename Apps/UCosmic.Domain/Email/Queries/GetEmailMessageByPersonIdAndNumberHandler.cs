using System;
using System.Linq.Expressions;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetEmailMessageByPersonIdAndNumberHandler : IHandleQueries<GetEmailMessageByPersonIdAndNumberQuery, EmailMessage>
    {
        private readonly IProcessQueries _queryProcessor;

        public GetEmailMessageByPersonIdAndNumberHandler(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public EmailMessage Handle(GetEmailMessageByPersonIdAndNumberQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var person = _queryProcessor.Execute(
                new GetPersonByIdQuery
                {
                    Id = query.PersonId,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Messages,
                    },
                }
            );

            if (person == null) return null;

            var message = person.GetMessage(query.Number);

            return message;
        }
    }
}
