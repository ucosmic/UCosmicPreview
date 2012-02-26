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

        private class PersonOrm : EntityTypeConfiguration<Person>
        {
            internal PersonOrm()
            {
                ToTable(typeof(Person).Name, DbSchemaName.People);

                // has one user
                HasOptional(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .WillCascadeOnDelete(false); // do not delete person if user is deleted

                // has many email addresses
                HasMany(p => p.Emails)
                    .WithRequired(d => d.Person)
                    .HasForeignKey(d => d.PersonId)
                    .WillCascadeOnDelete(true);

                // has many affiliations
                HasMany(p => p.Affiliations)
                    .WithRequired(d => d.Person)
                    .HasForeignKey(d => d.PersonId)
                    .WillCascadeOnDelete(true);
            }
        }

        private class EmailAddressOrm : EntityTypeConfiguration<EmailAddress>
        {
            internal EmailAddressOrm()
            {
                ToTable(typeof(EmailAddress).Name, DbSchemaName.People);

                // has many confirmations
                HasMany(p => p.Confirmations)
                    .WithRequired(d => d.EmailAddress)
                    .HasForeignKey(d => d.EmailAddressId)
                    .WillCascadeOnDelete(true);

                // has many messages
                HasMany(p => p.Messages)
                    .WithRequired(d => d.To)
                    .HasForeignKey(d => d.ToEmailAddressId)
                    .WillCascadeOnDelete(true);
            }
        }

        private class EmailConfirmationOrm : EntityTypeConfiguration<EmailConfirmation>
        {
            internal EmailConfirmationOrm()
            {
                ToTable(typeof(EmailConfirmation).Name, DbSchemaName.People);
            }
        }

        private class EmailMessageOrm : EntityTypeConfiguration<EmailMessage>
        {
            internal EmailMessageOrm()
            {
                ToTable(typeof(EmailMessage).Name, DbSchemaName.People);

                // may be composed from template
                HasOptional(d => d.FromEmailTemplate)
                    .WithMany()
                    .HasForeignKey(d => d.FromEmailTemplateId)
                    .WillCascadeOnDelete(false);

                Property(m => m.Body).HasColumnType("ntext");
            }
        }

        private class AffiliationOrm : EntityTypeConfiguration<Affiliation>
        {
            internal AffiliationOrm()
            {
                ToTable(typeof(Affiliation).Name, DbSchemaName.People);
            }
        }
    }
}
