using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class RemoveContactFromAgreementCommand
    {
        public RemoveContactFromAgreementCommand(IPrincipal principal, Guid contactGuid, Guid agreementGuid)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;

            if (contactGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "contactGuid");
            ContactGuid = contactGuid;

            if (agreementGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "agreementGuid");
            AgreementGuid = agreementGuid;
        }

        public IPrincipal Principal { get; private set; }
        public Guid ContactGuid { get; private set; }
        public Guid AgreementGuid { get; private set; }
        public bool IsNewlyRemoved { get; internal set; }
    }

    public class RemoveContactFromAgreementHandler : IHandleCommands<RemoveContactFromAgreementCommand>
    {
        private readonly ICommandEntities _entities;

        public RemoveContactFromAgreementHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(RemoveContactFromAgreementCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // todo: this should be FindByPrimaryKey
            var entity = _entities.Get<InstitutionalAgreementContact>()
                .SingleOrDefault(x =>
                    x.EntityId == command.ContactGuid
                );

            if (entity == null) return;
            _entities.Purge(entity);
            command.IsNewlyRemoved = true;
        }
    }
}
