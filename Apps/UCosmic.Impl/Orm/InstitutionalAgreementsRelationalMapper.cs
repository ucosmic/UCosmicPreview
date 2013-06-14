using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Impl.Orm
{
    public static class InstitutionalAgreementsRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InstitutionalAgreementConfigurationOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementTypeValueOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementStatusValueOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementContactTypeValueOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementNodeOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementParticipantOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementContactOrm());
            modelBuilder.Configurations.Add(new InstitutionalAgreementFileOrm());
        }

        private class InstitutionalAgreementConfigurationOrm : RevisableEntityTypeConfiguration<InstitutionalAgreementConfiguration>
        {
            internal InstitutionalAgreementConfigurationOrm()
            {
                ToTable(typeof(InstitutionalAgreementConfiguration).Name, DbSchemaName.InstitutionalAgreements);

                // has one establishment
                HasOptional(d => d.ForEstablishment)
                    .WithMany()
                    .HasForeignKey(d => d.ForEstablishmentId)
                    .WillCascadeOnDelete();
            }
        }

        private class InstitutionalAgreementTypeValueOrm : EntityTypeConfiguration<InstitutionalAgreementTypeValue>
        {
            internal InstitutionalAgreementTypeValueOrm()
            {
                ToTable(typeof(InstitutionalAgreementTypeValue).Name, DbSchemaName.InstitutionalAgreements);

                HasKey(p => p.Id);

                // has one configuration
                HasRequired(d => d.Configuration)
                    .WithMany(p => p.AllowedTypeValues)
                    .HasForeignKey(d => d.ConfigurationId)
                    .WillCascadeOnDelete();

                Property(p => p.Text).IsRequired().HasMaxLength(150);
            }
        }

        private class InstitutionalAgreementStatusValueOrm : EntityTypeConfiguration<InstitutionalAgreementStatusValue>
        {
            internal InstitutionalAgreementStatusValueOrm()
            {
                ToTable(typeof(InstitutionalAgreementStatusValue).Name, DbSchemaName.InstitutionalAgreements);

                HasKey(p => p.Id);

                // has one configuration
                HasRequired(d => d.Configuration)
                    .WithMany(p => p.AllowedStatusValues)
                    .HasForeignKey(d => d.ConfigurationId)
                    .WillCascadeOnDelete();

                Property(p => p.Text).IsRequired().HasMaxLength(50);
            }
        }

        private class InstitutionalAgreementContactTypeValueOrm : EntityTypeConfiguration<InstitutionalAgreementContactTypeValue>
        {
            internal InstitutionalAgreementContactTypeValueOrm()
            {
                ToTable(typeof(InstitutionalAgreementContactTypeValue).Name, DbSchemaName.InstitutionalAgreements);

                HasKey(p => p.Id);

                // has one configuration
                HasRequired(d => d.Configuration)
                    .WithMany(p => p.AllowedContactTypeValues)
                    .HasForeignKey(d => d.ConfigurationId)
                    .WillCascadeOnDelete();

                Property(p => p.Text).IsRequired().HasMaxLength(150);
            }
        }

        private class InstitutionalAgreementOrm : RevisableEntityTypeConfiguration<InstitutionalAgreement>
        {
            internal InstitutionalAgreementOrm()
            {
                ToTable(typeof(InstitutionalAgreement).Name, DbSchemaName.InstitutionalAgreements);

                // offspring is no longer derived from children
                //Ignore(p => p.Offspring);

                Property(p => p.RevisionId).IsRequired().HasColumnName("Id");
                Property(p => p.EntityId).IsRequired().HasColumnName("Guid");
                Ignore(p => p.IsCurrent);
                Ignore(p => p.IsArchived);
                Ignore(p => p.IsDeleted);

                // has 0 or 1 umbrella
                HasOptional(d => d.Umbrella)
                    .WithMany(p => p.Children)
                    .Map(m => m.MapKey("UmbrellaId"))
                    .WillCascadeOnDelete(false);

                // has many participants
                HasMany(p => p.Participants)
                    .WithRequired(d => d.Agreement)
                    .Map(m => m.MapKey("AgreementId"))
                    .WillCascadeOnDelete(true);

                // has many contacts
                HasMany(p => p.Contacts)
                    .WithRequired(d => d.Agreement)
                    .Map(m => m.MapKey("AgreementId"))
                    .WillCascadeOnDelete(true);

                // has many files
                HasMany(p => p.Files)
                    .WithRequired(d => d.Agreement)
                    .Map(m => m.MapKey("AgreementId"))
                    .WillCascadeOnDelete(true);

                // has many ancestors
                HasMany(p => p.Ancestors)
                    .WithRequired(d => d.Offspring)
                    .HasForeignKey(d => d.OffspringId)
                    .WillCascadeOnDelete(false);

                // has many offspring
                HasMany(p => p.Offspring)
                    .WithRequired(d => d.Ancestor)
                    .HasForeignKey(d => d.AncestorId)
                    .WillCascadeOnDelete(false);

                Property(p => p.Title).IsRequired().HasMaxLength(500);
                Property(p => p.Type).IsRequired().HasMaxLength(150);
                Property(p => p.Status).IsRequired().HasMaxLength(50);
                Property(p => p.Description).IsMaxLength();
                Property(p => p.VisibilityText).HasColumnName("Visibility").IsRequired().HasMaxLength(20);
            }
        }

        private class InstitutionalAgreementNodeOrm : EntityTypeConfiguration<InstitutionalAgreementNode>
        {
            internal InstitutionalAgreementNodeOrm()
            {
                ToTable(typeof(InstitutionalAgreementNode).Name, DbSchemaName.InstitutionalAgreements);

                HasKey(p => new { p.AncestorId, p.OffspringId });
            }
        }

        private class InstitutionalAgreementParticipantOrm : EntityTypeConfiguration<InstitutionalAgreementParticipant>
        {
            internal InstitutionalAgreementParticipantOrm()
            {
                ToTable(typeof(InstitutionalAgreementParticipant).Name, DbSchemaName.InstitutionalAgreements);

                HasKey(k => k.Id);

                // has one establishment
                HasRequired(d => d.Establishment)
                    .WithMany()
                    .Map(m => m.MapKey("EstablishmentId"))
                    .WillCascadeOnDelete(false); // do not delete agreements when deleting establishment
            }
        }

        private class InstitutionalAgreementContactOrm : RevisableEntityTypeConfiguration<InstitutionalAgreementContact>
        {
            internal InstitutionalAgreementContactOrm()
            {
                ToTable(typeof(InstitutionalAgreementContact).Name, DbSchemaName.InstitutionalAgreements);

                Property(p => p.RevisionId).IsRequired().HasColumnName("Id");
                Property(p => p.EntityId).IsRequired().HasColumnName("Guid");
                Ignore(p => p.IsCurrent);
                Ignore(p => p.IsArchived);
                Ignore(p => p.IsDeleted);

                // has one establishment
                HasRequired(d => d.Person)
                    .WithMany()
                    .Map(m => m.MapKey("PersonId"))
                    .WillCascadeOnDelete(true);

                Property(p => p.Type).IsRequired().HasMaxLength(150);
            }
        }

        private class InstitutionalAgreementFileOrm : RevisableEntityTypeConfiguration<InstitutionalAgreementFile>
        {
            internal InstitutionalAgreementFileOrm()
            {
                ToTable(typeof(InstitutionalAgreementFile).Name, DbSchemaName.InstitutionalAgreements);

                Property(p => p.RevisionId).IsRequired().HasColumnName("Id");
                Property(p => p.EntityId).IsRequired().HasColumnName("Guid");
                Ignore(p => p.IsCurrent);
                Ignore(p => p.IsArchived);
                Ignore(p => p.IsDeleted);
            }
        }
    }
}
