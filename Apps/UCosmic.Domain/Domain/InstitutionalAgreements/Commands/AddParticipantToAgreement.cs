using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class AddParticipantToAgreementCommand
    {
        public AddParticipantToAgreementCommand(IPrincipal principal, Guid establishmentGuid, Guid agreementGuid)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;

            if (establishmentGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "establishmentGuid");
            EstablishmentGuid = establishmentGuid;

            if (agreementGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "agreementGuid");
            AgreementGuid = agreementGuid;
        }

        internal AddParticipantToAgreementCommand(IPrincipal principal, Guid establishmentGuid, InstitutionalAgreement agreement)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;

            if (establishmentGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "establishmentGuid");
            EstablishmentGuid = establishmentGuid;

            if (agreement == null) throw new ArgumentNullException("agreement");
            Agreement = agreement;
        }

        public IPrincipal Principal { get; private set; }
        public Guid EstablishmentGuid { get; private set; }
        public Guid AgreementGuid { get; private set; }
        internal InstitutionalAgreement Agreement { get; set; }
        public bool IsNewlyAdded { get; internal set; }
    }

    public class AddParticipantToAgreementHandler : IHandleCommands<AddParticipantToAgreementCommand>
    {
        private readonly ICommandEntities _entities;

        public AddParticipantToAgreementHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(AddParticipantToAgreementCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var agreement = command.Agreement ??
                _entities.Get<InstitutionalAgreement>()
                .EagerLoad(_entities, new Expression<Func<InstitutionalAgreement, object>>[]
                {
                    r => r.Participants,
                })
                .By(command.AgreementGuid);

            var participant = agreement.Participants.SingleOrDefault(
                x => x.Establishment.EntityId == command.EstablishmentGuid);
            if (participant != null) return;

            var establishment = _entities.Get<Establishment>()
                .EagerLoad(_entities, new Expression<Func<Establishment, object>>[]
                {
                    e => e.Affiliates.Select(a => a.Person.User),
                    e => e.Names.Select(n => n.TranslationToLanguage),
                    e => e.Ancestors.Select(h => h.Ancestor.Affiliates.Select(a => a.Person.User)),
                    e => e.Ancestors.Select(h => h.Ancestor.Names.Select(n => n.TranslationToLanguage))
                })
                .By(command.EstablishmentGuid);


            participant = new InstitutionalAgreementParticipant
            {
                Establishment = establishment,
                Agreement = agreement,
            };

            // derive ownership (todo, this should be a separate query)
            Expression<Func<Affiliation, bool>> principalDefaultAffiliation = affiliation =>
                affiliation.IsDefault &&
                affiliation.Person.User != null &&
                affiliation.Person.User.Name.Equals(command.Principal.Identity.Name, StringComparison.OrdinalIgnoreCase);
            participant.IsOwner = establishment.Affiliates.AsQueryable().Any(principalDefaultAffiliation) ||
                establishment.Ancestors.Any(n => n.Ancestor.Affiliates.AsQueryable().Any(principalDefaultAffiliation));

            _entities.Create(participant);
            command.IsNewlyAdded = true;
        }
    }
}
