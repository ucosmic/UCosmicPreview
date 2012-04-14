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
            modelBuilder.Configurations.Add(new SubjectNameIdentifierOrm());
        }

        private class UserOrm : RevisableEntityTypeConfiguration<User>
        {
            internal UserOrm()
            {
                ToTable(typeof(User).Name, DbSchemaName.Identity);

                Property(u => u.Name).IsRequired().HasMaxLength(256);
            }
        }

        private class RoleOrm : RevisableEntityTypeConfiguration<Role>
        {
            internal RoleOrm()
            {
                ToTable(typeof(Role).Name, DbSchemaName.Identity);

                Property(p => p.Name).IsRequired().HasMaxLength(200);
                Property(p => p.Description).IsMaxLength();
            }
        }

        private class RoleGrantOrm : RevisableEntityTypeConfiguration<RoleGrant>
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

        private class SubjectNameIdentifierOrm : EntityTypeConfiguration<SubjectNameIdentifier>
        {
            internal SubjectNameIdentifierOrm()
            {
                ToTable(typeof(SubjectNameIdentifier).Name, DbSchemaName.Identity);

                HasKey(p => new { p.Number, p.UserId });

                // has one user
                HasRequired(d => d.User)
                    .WithMany(p => p.SubjectNameIdentifiers)
                    .HasForeignKey(d => d.UserId)
                    .WillCascadeOnDelete(true);

                Property(p => p.Value).HasMaxLength(256);
            }
        }
    }
}
