using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Security.Principal;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class GenerateTitleQuery : IDefineQuery<string>
    {
        public GenerateTitleQuery(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
        public bool IsTitleDerived { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public IEnumerable<Guid> ParticipantGuids { get; set; }
    }

    public class GenerateTitleHandler : IHandleQueries<GenerateTitleQuery, string>
    {
        private readonly IProcessQueries _queryProcessor;

        public GenerateTitleHandler(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public string Handle(GenerateTitleQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (!query.IsTitleDerived) return query.Title;

            var person = _queryProcessor.Execute(new GetMyPersonQuery(query.Principal));
            var establishments = _queryProcessor.Execute(
                new FindEstablishmentsWithGuidsQuery(query.ParticipantGuids)
                {
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                        {
                            e => e.Affiliates.Select(a => a.Person),
                            e => e.Ancestors,
                        },
                });
            var unorderedParticipants = establishments.Select(e =>
                new InstitutionalAgreementParticipant
                {
                    Establishment = e,
                    IsOwner = person.IsAffiliatedWith(e),
                }).ToArray();

            var title = new StringBuilder();
            title.Append(string.Format("{0} between ", query.Type ?? "Institutional Agreement"));
            if (unorderedParticipants.Any())
            {
                var participants = unorderedParticipants.OrderByDescending(p => p.IsOwner).ToList();
                foreach (var participant in participants)
                {
                    if (participants.Count > 1 && participant == participants.Last())
                    {
                        title.Append(string.Format("and {0} ", participant.Establishment.TranslatedName));
                    }
                    else if (participants.Count == 1)
                    {
                        title.Append(string.Format("{0} and... ", participant.Establishment.TranslatedName));
                    }
                    else if (participants.Count <= 2)
                    {
                        title.Append(string.Format("{0} ", participant.Establishment.TranslatedName));
                    }
                    else
                    {
                        title.Append(string.Format("{0}, ", participant.Establishment.TranslatedName));
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                title.Append(string.Format("- Status is {0} ", query.Status));
            }

            return title.ToString().Trim();
        }
    }
}
