using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.Identity;

namespace UCosmic.Orm
{
    public static class IdentityRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserOrm());
            modelBuilder.Configurations.Add(new RoleOrm());
            modelBuilder.Configurations.Add(new RoleGrantOrm());
        }

        private class UserOrm : EntityTypeConfiguration<User>
        {
            internal UserOrm()
            {
                ToTable(typeof(User).Name, DbSchemaName.Identity);
            }
        }

        private class RoleOrm : EntityTypeConfiguration<Role>
        {
            internal RoleOrm()
            {
                ToTable(typeof(Role).Name, DbSchemaName.Identity);
            }
        }

        private class RoleGrantOrm : EntityTypeConfiguration<RoleGrant>
        {
            internal RoleGrantOrm()
            {
                ToTable(typeof(RoleGrant).Name, DbSchemaName.Identity);

                // has one user
                HasRequired(d => d.User)
                    .WithMany(p => p.Grants)
                    .Map(d => d.MapKey("UserId"))
                    .WillCascadeOnDelete(true);

                // has one role
                HasRequired(d => d.Role)
                    .WithMany(p => p.Grants)
                    .Map(d => d.MapKey("RoleId"))
                    .WillCascadeOnDelete(true);

                // may have an establishment
                HasOptional(d => d.ForEstablishment)
                    .WithMany()
                    .Map(d => d.MapKey("ForEstablishmentId"))
                    .WillCascadeOnDelete(true);
            }
        }
    }
}
