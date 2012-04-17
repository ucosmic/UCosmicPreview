using System.Linq;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Seeders
{
    public abstract class BasePersonSeeder : UCosmicDbSeeder
    {
        protected Person EnsurePerson(string emails, string firstName, string lastName, Establishment employedBy, bool registerUser = true)
        {
            var queryProcessor = DependencyInjector.Current.GetService<IProcessQueries>();

            var emailsExploded = emails.Explode(";").ToArray();
            var defaultEmail = emailsExploded.First();
            var person = queryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = defaultEmail
                }
            );
            if (person != null) return person;
            person = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = string.Format("{0} {1}", firstName, lastName),
                User = UserFactory.Create(defaultEmail, registerUser),
            };
            Context.People.Add(person);
            Context.SaveChanges();

            foreach (var email in emails.Explode(";"))
            {
                var emailAddress = person.AddEmail(email);
                emailAddress.IsConfirmed = true;
                emailAddress.IsDefault = email == defaultEmail;
                //person.Emails.Add(new EmailAddress { Value = email, IsDefault = (email == defaultEmail), IsConfirmed = true, });
            }
            Context.SaveChanges();

            if (employedBy != null)
            {
                person.Affiliations.Add(new Affiliation
                {
                    Establishment = employedBy,
                    IsClaimingEmployee = true,
                    IsClaimingStudent = false,
                    IsDefault = true,
                });
                Context.SaveChanges();
            }
            return person;
        }
    }
}