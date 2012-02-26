using UCosmic.Domain.Identity;
using UCosmic.Orm;

namespace UCosmic.Seeders
{
    public class RoleSeeder : ISeedDb
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public void Seed(UCosmicContext context)
        {
            new RolePreview4Seeder().Seed(context);
            new RolePreview5Seeder().Seed(context);
            new RoleDecember2011Preview2Seeder().Seed(context);
        }

        private class RoleDecember2011Preview2Seeder : BaseRoleSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                // add authentication agent
                var authenticationAgent = Context.Roles.ByName(RoleName.AuthenticationAgent);
                if (authenticationAgent == null)
                {
                    EnsureRole(RoleName.AuthenticationAgent,
                        "Authentication Agents can sign in as any user, regardless of establishment."
                    );
                    Context.SaveChanges();
                }

                // add establishment place editor
                var establishmentLocationAgent = Context.Roles.ByName(RoleName.EstablishmentLocationAgent);
                if (establishmentLocationAgent == null)
                {
                    EnsureRole(RoleName.EstablishmentLocationAgent,
                        "Establishment Location Agents can modify location information for any establishment."
                    );
                    Context.SaveChanges();
                }
            }
        }

        private class RolePreview5Seeder : BaseRoleSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                //// rename authorization executive to authorization agent
                //var roleFinder = new RoleFinder(Context);
                ////var authorizationAgent = Context.Roles.ByName("Authorization Executive");
                //var authorizationAgent = roleFinder.FindOne(RoleBy.Name("Authorization Executive").ForInsertOrUpdate());
                //if (authorizationAgent == null) return;

                //authorizationAgent.Name = RoleName.AuthorizationAgent;
                //authorizationAgent.Description =
                //    "Authorization Agents can control the roles and role grants to any user, regardless of establishment.";
                //Context.SaveChanges();
            }
        }

        private class RolePreview4Seeder : BaseRoleSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                EnsureRole(RoleName.AuthorizationAgent,
                    "Authorization Agents can control the roles and role grants to any user, regardless of establishment."
                );
                EnsureRole(RoleName.InstitutionalAgreementManager,
                    "Institutional Agreement Managers can add, edit, and otherwise manage institutional agreements for their institutions. " +
                    "Additionally, they are allowed to view fields marked with 'private' access."
                );
                EnsureRole(RoleName.InstitutionalAgreementSupervisor,
                    "Institutional Agreement Supervisors can grant Institutional Agreement Manager " +
                    "privileges to users at their institutions."
                );
            }
        }
    }
}