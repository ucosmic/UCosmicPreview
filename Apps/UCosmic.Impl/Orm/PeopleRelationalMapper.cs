using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UCosmic.Domain.People;

namespace UCosmic.Orm
{
    public static class PeopleRelationalMapper
    {
        public static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonOrm());
            modelBuilder.Configurations.Add(new EmailAddressOrm());
            modelBuilder.Configurations.Add(new EmailConfirmationOrm());
            modelBuilder.Configurations.Add(new EmailMessageOrm());
            modelBuilder.Configurations.Add(new AffiliationOrm());
        }

        private class PersonOrm : RevisableEntityTypeConfiguration<Person>
        {
            internal PersonOrm()
            {
                ToTable(typeof(Person).Name, DbSchemaName.People);

                // has zero or one user
                HasOptional(p => p.User)
                    .WithRequired(d => d.Person)
                    .Map(d => d.MapKey("PersonId"))
                    .WillCascadeOnDelete(false)
                ;

                // has many email addresses
                HasMany(p => p.Emails)
                    .WithRequired(d => d.Person)
                    .HasForeignKey(d => d.PersonId)
                    .WillCascadeOnDelete(true)
                ;

                // has many messages
                HasMany(p => p.Messages)
                    .WithRequired(d => d.ToPerson)
                    .HasForeignKey(d => d.ToPersonId)
                    .WillCascadeOnDelete(true);

                // has many affiliations
                HasMany(p => p.Affiliations)
                    .WithRequired(d => d.Person)
                    .HasForeignKey(d => d.PersonId)
                    .WillCascadeOnDelete(true)
                ;

                Property(p => p.DisplayName).IsRequired().HasMaxLength(200);
                Property(p => p.Salutation).HasMaxLength(50);
                Property(p => p.FirstName).HasMaxLength(100);
                Property(p => p.MiddleName).HasMaxLength(100);
                Property(p => p.LastName).HasMaxLength(100);
                Property(p => p.Suffix).HasMaxLength(50);
            }
        }

        private class EmailAddressOrm : EntityTypeConfiguration<EmailAddress>
        {
            internal EmailAddressOrm()
            {
                ToTable(typeof(EmailAddress).Name, DbSchemaName.People);

                HasKey(p => new { p.PersonId, p.Number });

                // has many confirmations
                HasMany(p => p.Confirmations)
                    .WithRequired(d => d.EmailAddress)
                    .HasForeignKey(d => new { d.EmailAddressId, d.EmailAddressNumber, })
                    .WillCascadeOnDelete(true);

                Property(p => p.Value).IsRequired().HasMaxLength(256);
            }
        }

        private class EmailConfirmationOrm : EntityTypeConfiguration<EmailConfirmation>
        {
            internal EmailConfirmationOrm()
            {
                ToTable(typeof(EmailConfirmation).Name, DbSchemaName.People);

                HasKey(p => p.Id);

                Property(p => p.SecretCode).HasMaxLength(15);
                Property(p => p.Intent).IsRequired().HasMaxLength(20);
            }
        }

        private class EmailMessageOrm : EntityTypeConfiguration<EmailMessage>
        {
            internal EmailMessageOrm()
            {
                ToTable(typeof(EmailMessage).Name, DbSchemaName.People);

                HasKey(p => new { p.ToPersonId, p.Number });

                Property(m => m.FromEmailTemplate).HasMaxLength(150);
                Property(m => m.ToAddress).IsRequired().HasMaxLength(256);
                Property(m => m.Subject).IsRequired().HasMaxLength(250);
                Property(m => m.FromAddress).IsRequired().HasMaxLength(256);
                Property(m => m.FromDisplayName).HasMaxLength(150);
                Property(m => m.ReplyToAddress).HasMaxLength(256);
                Property(m => m.ReplyToDisplayName).HasMaxLength(150);
                Property(m => m.Body).IsRequired().HasColumnType("ntext");
                Property(m => m.ComposedByPrincipal).HasMaxLength(256);
            }
        }

        private class AffiliationOrm : EntityTypeConfiguration<Affiliation>
        {
            internal AffiliationOrm()
            {
                ToTable(typeof(Affiliation).Name, DbSchemaName.People);

                HasKey(p => new { p.PersonId, p.EstablishmentId });

                Property(p => p.JobTitles).HasMaxLength(500);
            }
        }
    }
}
