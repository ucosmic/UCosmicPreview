using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Impl.Seeders
{
    public class InstitutionalAgreementSettingsEntitySeeder : BaseDataSeeder
    {
        private readonly ICommandEntities _entities;
        private readonly IHandleCommands<CreateOrUpdateConfigurationCommand> _settingsHandler;
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionalAgreementSettingsEntitySeeder(ICommandEntities entities
            , IHandleCommands<CreateOrUpdateConfigurationCommand> settingsHandler
            , IUnitOfWork unitOfWork
        )
        {
            _entities = entities;
            _settingsHandler = settingsHandler;
            _unitOfWork = unitOfWork;
        }

        public override void Seed()
        {
            // seed settings for a few test tenants
            PurgeCurrentSettings();
            var tenants = new Dictionary<string, string>
            {
                { "www.fue.edu.eg", "@fue.edu.eg" },
                { "www.griffith.edu.au", "@griffith.edu.au" },
                { "www.unsw.edu.au", "@unsw.edu.au" },
            };

            foreach (var tenant in tenants)
            {
                var principal = GetPrincipal(tenant.Value);
                var command = new CreateOrUpdateConfigurationCommand(principal);
                Seed(command);
            }
        }

        protected void Seed(CreateOrUpdateConfigurationCommand command)
        {
            command.AllowedTypeValues = new[]
            {
                "Agreement Type #1",
                "Agreement Type #2",
                "Agreement Type #3",
            };
            command.AllowedStatusValues = new[]
            {
                "Current Status #1",
                "Current Status #2",
                "Current Status #3",
                "Current Status #4",
            };
            command.AllowedContactTypeValues = new[]
            {
                "Contact Type #1",
                "Contact Type #2",
                "Contact Type #3",
                "Contact Type #4",
            };
            _settingsHandler.Handle(command);
            _unitOfWork.SaveChanges();
        }

        private IPrincipal GetPrincipal(string domain)
        {
            var identity = new GenericIdentity(string.Format("supervisor1{0}", domain));
            var principal = new GenericPrincipal(identity, new[]
            {
                RoleName.InstitutionalAgreementSupervisor
            });
            return principal;
        }

        private void PurgeCurrentSettings()
        {
            _entities.Get<InstitutionalAgreementConfiguration>().ToList().ForEach(x =>
            {
                _entities.Purge(x);
                _unitOfWork.SaveChanges();
            });
        }
    }
}
