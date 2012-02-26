using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Orm
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

        private class InstitutionalAgreementConfigurationOrm : EntityTypeConfiguration<InstitutionalAgreementConfiguration>
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

                // has one configuration
                HasRequired(d => d.Configuration)
                    .WithMany(p => p.AllowedTypeValues)
                    .HasForeignKey(d => d.ConfigurationId)
                    .WillCascadeOnDelete();
            }
        }

        private class InstitutionalAgreementStatusValueOrm : EntityTypeConfiguration<InstitutionalAgreementStatusValue>
        {
            internal InstitutionalAgreementStatusValueOrm()
            {
                ToTable(typeof(InstitutionalAgreementStatusValue).Name, DbSchemaName.InstitutionalAgreements);

                // has one configuration
                HasRequired(d => d.Configuration)
                    .WithMany(p => p.AllowedStatusValues)
                    .HasForeignKey(d => d.ConfigurationId)
                    .WillCascadeOnDelete();
            }
        }

        private class InstitutionalAgreementContactTypeValueOrm : EntityTypeConfiguration<InstitutionalAgreementContactTypeValue>
        {
            internal InstitutionalAgreementContactTypeValueOrm()
            {
                ToTable(typeof(InstitutionalAgreementContactTypeValue).Name, DbSchemaName.InstitutionalAgreements);

                // has one configuration
                HasRequired(d => d.Configuration)
                    .WithMany(p => p.AllowedContactTypeValues)
                    .HasForeignKey(d => d.ConfigurationId)
                    .WillCascadeOnDelete();
            }
        }

        private class InstitutionalAgreementOrm : EntityTypeConfiguration<InstitutionalAgreement>
        {
            internal InstitutionalAgreementOrm()
            {
                ToTable(typeof(InstitutionalAgreement).Name, DbSchemaName.InstitutionalAgreements);

                // offspring is no longer derived from children
                //Ignore(p => p.Offspring);

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

        private class InstitutionalAgreementContactOrm : EntityTypeConfiguration<InstitutionalAgreementContact>
        {
            internal InstitutionalAgreementContactOrm()
            {
                ToTable(typeof(InstitutionalAgreementContact).Name, DbSchemaName.InstitutionalAgreements);

                // has one establishment
                HasRequired(d => d.Person)
                    .WithMany()
                    .Map(m => m.MapKey("PersonId"))
                    .WillCascadeOnDelete(true);
            }
        }

        private class InstitutionalAgreementFileOrm : EntityTypeConfiguration<InstitutionalAgreementFile>
        {
            internal InstitutionalAgreementFileOrm()
            {
                ToTable(typeof(InstitutionalAgreementFile).Name, DbSchemaName.InstitutionalAgreements);
                HasKey(m => m.RevisionId);
            }
        }
    }
}
