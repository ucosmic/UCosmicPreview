using System;
using System.Linq;
using ServiceLocatorPattern;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Impl.Seeders
{
    public abstract class BasePersonSeeder : UCosmicDbSeeder
    {
        protected Person EnsurePerson(string emails, string firstName, string lastName, Establishment employedBy, bool registerUser = true)
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var createPersonHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<CreatePersonCommand>>();
            var createAffiliationHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<CreateAffiliationCommand>>();

            var emailsExploded = emails.Explode(";").ToArray();
            var defaultEmail = emailsExploded.First();
            var emailAddress = entities.Get2<EmailAddress>()
                .SingleOrDefault(e => e.Value.Equals(defaultEmail, StringComparison.OrdinalIgnoreCase));
            var person = (emailAddress != null) ? emailAddress.Person : null;
            //var person = queryProcessor.Execute(
            //    new GetPersonByEmailQuery
            //    {
            //        Email = defaultEmail
            //    }
            //);
            if (person != null) return person;
            //person = new Person
            //{
            //    FirstName = firstName,
            //    LastName = lastName,
            //    DisplayName = string.Format("{0} {1}", firstName, lastName),
            //    User = UserFactory.Create(defaultEmail, registerUser),
            //};
            //Context.People.Add(person);
            var command = new CreatePersonCommand
            {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = string.Format("{0} {1}", firstName, lastName),
                UserName = defaultEmail,
                UserIsRegistered = registerUser,
            };
            createPersonHandler.Handle(command);
            Context.SaveChanges();
            person = command.CreatedPerson;

            foreach (var email in emails.Explode(";"))
            {
                emailAddress = person.AddEmail(email);
                emailAddress.IsConfirmed = true;
                emailAddress.IsDefault = email == defaultEmail;
                //person.Emails.Add(new EmailAddress { Value = email, IsDefault = (email == defaultEmail), IsConfirmed = true, });
            }
            Context.SaveChanges();

            if (employedBy != null)
            {
                //person.Affiliations.Add(new Affiliation
                //{
                //    Establishment = employedBy,
                //    IsClaimingEmployee = true,
                //    IsClaimingStudent = false,
                //    IsDefault = true,
                //});
                createAffiliationHandler.Handle(
                    new CreateAffiliationCommand
                    {
                        EstablishmentId = employedBy.RevisionId,
                        PersonId = person.RevisionId,
                        IsClaimingEmployee = true,
                        IsClaimingStudent = false,
                    }
                );
                Context.SaveChanges();
            }
            else
            {
                throw new NotSupportedException("Why is the person not affiliated with an employer?");
            }
            return person;
        }
    }
}