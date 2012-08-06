using System;
using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Impl.Seeders
{
    public class UserEntitySeeder : BasePersonEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<GrantRoleToUserCommand> _grantRole;
        private readonly IUnitOfWork _unitOfWork;

        public UserEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreatePersonCommand> createPerson
            , IHandleCommands<CreateAffiliationCommand> createAffiliation
            , IHandleCommands<GrantRoleToUserCommand> grantRole
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createPerson, createAffiliation, unitOfWork)
        {
            _queryProcessor = queryProcessor;
            _grantRole = grantRole;
            _unitOfWork = unitOfWork;
        }

        public override void Seed()
        {
            var developerRoles = new[]
            {
                RoleName.AuthorizationAgent,
                RoleName.EstablishmentLocationAgent,
                RoleName.InstitutionalAgreementManager,
                RoleName.InstitutionalAgreementSupervisor,
            };

            // seed developers
            Seed(new[]
                {
                    "Daniel.Ludwig@uc.edu",
                    "ludwigd@ucmail.uc.edu",
                    "ludwigd@uc.edu",
                    "Daniel.Ludwig@ucmail.uc.edu",
                },
                "Dan", "Ludwig", "www.uc.edu", developerRoles);
            Seed("ganesh_c@uc.edu", "Ganesh", "Chitrothu", "www.uc.edu", developerRoles);

            // seed example non-academic users
            Seed("test@terradotta.com", "Terradotta", "Test", "www.terradotta.com");

            var testEstablishments = new Dictionary<string, string>
            {
                { "www.uc.edu",           "@uc.edu"             },
                { "www.suny.edu",         "@suny.edu"           },
                { "www.umn.edu",          "@umn.edu"            },
                { "www.lehigh.edu",       "@lehigh.edu"         },
                { "www.usil.edu.pe",      "@usil.edu.pe"        },
                { "www.bjtu.edu.cn",      "@bjtu.edu.cn"        },
                { "www.napier.ac.uk",     "@napier.ac.uk"       },
                { "www.fue.edu.eg",       "@fue.edu.eg"         },
                { "www.griffith.edu.au",  "@griffith.edu.au"    },
                { "www.unsw.edu.au",      "@unsw.edu.au"        },
                { "www.usf.edu",          "@usf.edu"            },
            };
            var managerRoles = new[] { RoleName.InstitutionalAgreementManager };
            var supervisorRoles = new[] { RoleName.InstitutionalAgreementSupervisor };
            var agentRoles = new[] { RoleName.AuthorizationAgent };

            foreach (var testEstablishment in testEstablishments)
            {
                Seed(string.Format("any1{0}", testEstablishment.Value), "Any", "One", testEstablishment.Key);
                Seed(string.Format("manager1{0}", testEstablishment.Value), "Manager", "One", testEstablishment.Key, managerRoles);
                Seed(string.Format("supervisor1{0}", testEstablishment.Value), "Supervisor", "One", testEstablishment.Key, supervisorRoles);
                Seed(string.Format("agent1{0}", testEstablishment.Value), "Agent", "One", testEstablishment.Key, agentRoles);
            }
        }

        protected void Seed(string[] emails, string firstName, string lastName, string establishmentUrl, string[] roles = null)
        {
            // make sure establishment exists
            var establishment = _queryProcessor.Execute(new GetEstablishmentByUrlQuery(establishmentUrl));
            if (establishment == null)
                throw new InvalidOperationException(string.Format(
                    "There is no establishment for URL '{0}'.", establishmentUrl));

            var person = Seed(establishment.RevisionId, new CreatePersonCommand
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = emails.First(),
                UserIsRegistered = true,
                EmailAddresses = emails.Select(e =>
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = e,
                        IsConfirmed = (!e.Equals("Daniel.Ludwig@ucmail.uc.edu")),
                        IsDefault = e == emails.First(),
                    })
                    .ToArray(),
            });

            if (roles != null && roles.Any())
            {
                foreach (var roleName in roles)
                {
                    if (person.User.Grants.Select(g => g.Role.Name).Contains(roleName))
                        continue;

                    var role = _queryProcessor.Execute(new GetRoleBySlugQuery(roleName.Replace(" ", "-")));
                    _grantRole.Handle(new GrantRoleToUserCommand(role.EntityId, person.User.EntityId));
                }
            }

            _unitOfWork.SaveChanges();
        }

        protected void Seed(string userName, string firstName, string lastName, string establishmentUrl, string[] roles = null)
        {
            Seed(new[] { userName }, firstName, lastName, establishmentUrl, roles);
        }
    }
}
