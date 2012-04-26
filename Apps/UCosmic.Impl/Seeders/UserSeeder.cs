using System.Linq;
using System.Web.Security;
using UCosmic.Domain.People;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    public class UserSeeder : ISeedDb
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public void Seed(UCosmicContext context)
        {
            new UserPreview4Seeder().Seed(context);
        }

        private class UserPreview4Seeder : BaseUserSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                // delete all app services db users
                var members = Membership.GetAllUsers();
                foreach (var member in members.Cast<MembershipUser>())
                    Membership.DeleteUser(member.UserName, true);

                // members for developers
                var developerRoles = new[]
                {
                    RoleName.AuthorizationAgent,
                    RoleName.EstablishmentLocationAgent,
                    RoleName.InstitutionalAgreementManager,
                    RoleName.InstitutionalAgreementSupervisor,
                };
                EnsureUser("ludwigd1@uc.edu;ludwigd11@uc.edu;ludwigd111@uc.edu", "Test1", "Test1", "www.uc.edu",
                            developerRoles);
                var queryProcessor = DependencyInjector.Current.GetService<IProcessQueries>();
                var findByEmailQuery = new GetPersonByEmailQuery {Email = "ludwigd1@uc.edu"};
                queryProcessor.Execute(findByEmailQuery).Emails.ByValue("ludwigd11@uc.edu").IsConfirmed = false;
                Context.SaveChanges(); // make 1 of ludwigd1's email addresses unconfirmed

                EnsureUser("sodhiha1@uc.edu", "Haritma", "Sodhi", "www.uc.edu", developerRoles);
                EnsureUser("ganesh_c@uc.edu", "Ganesh", "Chitrothu", "www.uc.edu", developerRoles);
                EnsureUser("test@terradotta.com", "Terradotta", "Test", "www.terradotta.com", null);

                // members for non-role-based tests
                EnsureUser("any1@uc.edu", "Any", "One", "www.uc.edu");
                EnsureUser("any1@suny.edu", "Any", "One", "www.suny.edu");
                EnsureUser("any1@umn.edu", "Any", "One", "www.umn.edu");
                EnsureUser("any1@lehigh.edu", "Any", "One", "www.lehigh.edu");
                EnsureUser("any1@usil.edu.pe", "Any", "One", "www.usil.edu.pe");
                EnsureUser("any1@bjtu.edu.cn", "Any", "One", "www.bjtu.edu.cn");
                EnsureUser("any1@napier.ac.uk", "Any", "One", "www.napier.ac.uk");
                EnsureUser("any1@fue.edu.eg", "Any", "One", "www.fue.edu.eg");
                EnsureUser("any1@griffith.edu.au", "Any", "One", "www.griffith.edu.au");
                EnsureUser("any1@unsw.edu.au", "Any", "One", "www.unsw.edu.au");

                // members for manager-role-based tests
                var managerRoles = new[] { RoleName.InstitutionalAgreementManager };
                EnsureUser("manager1@uc.edu", "Manager", "One", "www.uc.edu", managerRoles);
                EnsureUser("manager1@suny.edu", "Manager", "One", "www.suny.edu", managerRoles);
                EnsureUser("manager1@umn.edu", "Manager", "One", "www.umn.edu", managerRoles);
                EnsureUser("manager1@lehigh.edu", "Manager", "One", "www.lehigh.edu", managerRoles);
                EnsureUser("manager1@usil.edu.pe", "Manager", "One", "www.usil.edu.pe", managerRoles);
                EnsureUser("manager1@bjtu.edu.cn", "Manager", "One", "www.bjtu.edu.cn", managerRoles);
                EnsureUser("manager1@napier.ac.uk", "Manager", "One", "www.napier.ac.uk", managerRoles);
                EnsureUser("manager1@fue.edu.eg", "Manager", "One", "www.fue.edu.eg", managerRoles);
                EnsureUser("manager1@griffith.edu.au", "Manager", "One", "www.griffith.edu.au", managerRoles);
                EnsureUser("manager1@unsw.edu.au", "Manager", "One", "www.unsw.edu.au", managerRoles);

                // members for supervisor-role-based tests
                var supervisorRoles = new[] { RoleName.InstitutionalAgreementSupervisor };
                EnsureUser("supervisor1@uc.edu", "Supervisor", "One", "www.uc.edu", supervisorRoles);
                EnsureUser("supervisor1@suny.edu", "Supervisor", "One", "www.suny.edu", supervisorRoles);
                EnsureUser("supervisor1@umn.edu", "Supervisor", "One", "www.umn.edu", supervisorRoles);
                EnsureUser("supervisor1@lehigh.edu", "Supervisor", "One", "www.lehigh.edu", supervisorRoles);
                EnsureUser("supervisor1@usil.edu.pe", "Supervisor", "One", "www.usil.edu.pe", supervisorRoles);
                EnsureUser("supervisor1@bjtu.edu.cn", "Supervisor", "One", "www.bjtu.edu.cn", supervisorRoles);
                EnsureUser("supervisor1@napier.ac.uk", "Supervisor", "One", "www.napier.ac.uk", supervisorRoles);
                EnsureUser("supervisor1@fue.edu.eg", "Supervisor", "One", "www.fue.edu.eg", supervisorRoles);
                EnsureUser("supervisor1@griffith.edu.au", "Supervisor", "One", "www.griffith.edu.au", supervisorRoles);
                EnsureUser("supervisor1@unsw.edu.au", "Supervisor", "One", "www.unsw.edu.au", supervisorRoles);

                // members for agent-role-based tests
                var agentRoles = new[] { RoleName.AuthorizationAgent };
                EnsureUser("agent1@uc.edu", "Agent", "One", "www.uc.edu", agentRoles);
                EnsureUser("agent1@suny.edu", "Agent", "One", "www.suny.edu", agentRoles);
                EnsureUser("agent1@umn.edu", "Agent", "One", "www.umn.edu", agentRoles);
                EnsureUser("agent1@lehigh.edu", "Agent", "One", "www.lehigh.edu", agentRoles);
                EnsureUser("agent1@usil.edu.pe", "Agent", "One", "www.usil.edu.pe", agentRoles);
            }
        }
    }
}