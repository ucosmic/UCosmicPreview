using UCosmic.Domain;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    public class RoleSeeder : ISeedDb
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public void Seed(UCosmicContext context)
        {
            new RolePreview4Seeder().Seed(context);
            new RoleDecember2011Preview2Seeder().Seed(context);

            context.SaveChanges();
        }

        private class RoleDecember2011Preview2Seeder : BaseRoleSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                // add authentication agent
                EnsureRole(RoleName.AuthenticationAgent,
                    "Authentication Agents can sign on as any user, regardless of establishment."
                );

                // add establishment place editor
                EnsureRole(RoleName.EstablishmentLocationAgent,
                    "Establishment Location Agents can modify location information for any establishment."
                );
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