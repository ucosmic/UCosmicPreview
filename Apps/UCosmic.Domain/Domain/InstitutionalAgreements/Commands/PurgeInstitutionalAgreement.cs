using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class PurgeInstitutionalAgreement
    {
        public PurgeInstitutionalAgreement(IPrincipal principal, Guid agreementId)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            if (agreementId == Guid.Empty) throw new ArgumentException("Cannot be empty", "agreementId");
            Principal = principal;
            AgreementId = agreementId;
        }

        public IPrincipal Principal { get; private set; }
        public Guid AgreementId { get; private set; }
    }

    public class HandlePurgeInstitutionalAgreementCommand : IHandleCommands<PurgeInstitutionalAgreement>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;

        public HandlePurgeInstitutionalAgreementCommand(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IUnitOfWork unitOfWork
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _unitOfWork = unitOfWork;
        }

        public void Handle(PurgeInstitutionalAgreement command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // find agreement
            var agreement = _queryProcessor.Execute(
                new GetMyInstitutionalAgreementByGuidQuery(command.Principal, command.AgreementId));
            if (agreement == null) return;

            agreement = _entities.Get<InstitutionalAgreement>().Single(x => x.EntityId == command.AgreementId);
            _entities.Purge(agreement);
            _unitOfWork.SaveChanges();
        }
    }
}
