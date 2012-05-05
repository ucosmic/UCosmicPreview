using System;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public class GetEmailMessageByNumberQuery : BaseQuery, IDefineQuery<EmailMessage>
    {
        public int PersonId { get; set; }
        public int Number { get; set; }
    }

    public class GetEmailMessageByNumberHandler : IHandleQueries<GetEmailMessageByNumberQuery, EmailMessage>
    {
        private readonly IProcessQueries _queryProcessor;

        public GetEmailMessageByNumberHandler(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public EmailMessage Handle(GetEmailMessageByNumberQuery query)
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
