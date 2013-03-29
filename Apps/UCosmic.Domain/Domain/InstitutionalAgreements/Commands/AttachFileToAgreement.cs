using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using UCosmic.Domain.Files;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class AttachFileToAgreementCommand
    {
        public AttachFileToAgreementCommand(IPrincipal principal, Guid fileGuid, Guid agreementGuid)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;

            if (fileGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "fileGuid");
            FileGuid = fileGuid;

            if (agreementGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "agreementGuid");
            AgreementGuid = agreementGuid;
        }

        internal AttachFileToAgreementCommand(IPrincipal principal, Guid fileGuid, InstitutionalAgreement agreement)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;

            if (fileGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "fileGuid");
            FileGuid = fileGuid;

            if (agreement == null) throw new ArgumentNullException("agreement");
            Agreement = agreement;
        }

        public IPrincipal Principal { get; private set; }
        public Guid FileGuid { get; private set; }
        public Guid AgreementGuid { get; private set; }
        internal InstitutionalAgreement Agreement { get; set; }
        public bool IsNewlyAttached { get; internal set; }
    }

    public class AttachFileToAgreementHandler : IHandleCommands<AttachFileToAgreementCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly IStoreBinaryData _binaryData;

        public AttachFileToAgreementHandler(ICommandEntities entities
            , IStoreBinaryData binaryData
        )
        {
            _entities = entities;
            _binaryData = binaryData;
        }

        public void Handle(AttachFileToAgreementCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var agreement = command.Agreement ??
                _entities.Get<InstitutionalAgreement>()
                .EagerLoad(_entities, new Expression<Func<InstitutionalAgreement, object>>[]
                {
                    r => r.Files,
                })
                .By(command.AgreementGuid);

            var file = agreement.Files.SingleOrDefault(g => g.EntityId == command.FileGuid);
            if (file != null) return;

            var looseFile = _entities.Get<LooseFile>().By(command.FileGuid);
            if (looseFile == null) return;

            // also store in binary data
            var path = string.Format(InstitutionalAgreementFile.PathFormat, agreement.RevisionId, Guid.NewGuid());
            _binaryData.Put(path, looseFile.Content);

            file = new InstitutionalAgreementFile
            {
                Agreement = agreement,
                //Content = looseFile.Content,
                Length = looseFile.Length,
                MimeType = looseFile.MimeType,
                Name = looseFile.Name,
                Path = path,
            };

            _entities.Create(file);
            _entities.Purge(looseFile);
            command.IsNewlyAttached = true;
        }
    }
}
