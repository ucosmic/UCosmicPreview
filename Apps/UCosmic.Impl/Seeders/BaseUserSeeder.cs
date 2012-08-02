using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ServiceLocatorPattern;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Impl.Seeders
{
    public abstract class BaseUserSeeder : BasePersonSeeder
    {
        protected void EnsureUser(string emails, string firstName, string lastName,
            string affiliationUrl, IEnumerable<string> roleNames = null)
        {
            // get affiliated establishment
            var establishment = Context.Set<Establishment>().ByWebsiteUrl(affiliationUrl);
            if (establishment == null)
                throw new InvalidOperationException(string.Format("There is no establishment for URL '{0}'.", affiliationUrl));

            var emailsExploded = emails.Explode(";").ToArray();
            var defaultEmail = emailsExploded.First();

            // create the app services member
            // ReSharper disable UnusedVariable
            var member = Membership.GetUser(defaultEmail)
                         ?? Membership.CreateUser(defaultEmail, "asdfasdf", defaultEmail);
            // ReSharper restore UnusedVariable

            // ensuring person will ensure user
            var person = EnsurePerson(emails, firstName, lastName, establishment);

            // make user registered and confirm all email addresses
            foreach (var email in emailsExploded)
                person.GetEmail(email).IsConfirmed = true;

            // add grants to user
            if (roleNames != null)
            {
                //var roles = new RoleFacade(Context);
                ////foreach (var roleName in roleNames)
                ////{
                ////    if (person.User.Grants.Select(g => g.Role.Name).Contains(roleName)) continue;

                ////    var role = roles.Get(roleName);
                ////    role.GrantUser(person.User.EntityId, userFinder);
                ////    Context.Entry(role).State = role.RevisionId == 0 ? EntityState.Added : EntityState.Modified;
                ////}
                //foreach (var role in from roleName in roleNames
                //    where !person.User.Grants.Select(g => g.Role.Name).Contains(roleName)
                //    select roles.Get(roleName))
                //{
                //    role.GrantUser(person.User.EntityId, Context);
                //    Context.Entry(role).State = role.RevisionId == 0 ? EntityState.Added : EntityState.Modified;
                //}

                var queryProcessor = ServiceProviderLocator.Current.GetService<IProcessQueries>();
                var grantHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<GrantRoleToUserCommand>>();
                foreach (var roleName in roleNames)
                {
                    if (person.User.Grants.Select(g => g.Role.Name).Contains(roleName)) continue;

                    var role = queryProcessor.Execute(new GetRoleBySlugQuery(roleName.Replace(" ", "-")));
                    var command = new GrantRoleToUserCommand(role.EntityId, person.User.EntityId);
                    grantHandler.Handle(command);
                }
            }

            Context.SaveChanges();
        }
    }
}