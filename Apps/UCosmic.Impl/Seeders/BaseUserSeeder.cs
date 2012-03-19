using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Security;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Seeders
{
    public abstract class BaseUserSeeder : BasePersonSeeder
    {
        protected void EnsureUser(string emails, string firstName, string lastName,
            string affiliationUrl, IEnumerable<string> roleNames = null)
        {
            // get affiliated establishment
            var establishment = Context.Establishments.ByWebsiteUrl(affiliationUrl);
            //Context.Entry(establishment).Reload();
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
            var person = EnsurePerson(emails, firstName, lastName, establishment, true);

            // make user registered and confirm all email addresses
            //person.User.IsRegistered = true;
            foreach (var email in emailsExploded)
                person.Emails.Current().ByValue(email).IsConfirmed = true;

            // add grants to user
            if (roleNames != null)
            {
                foreach (var roleName in roleNames)
                {
                    if (!person.User.Grants.Select(g => g.Role.Name).Contains(roleName))
                    {
                        var role = Context.Roles.ByName(roleName);
                        role.Grants = role.Grants ?? new Collection<RoleGrant>();
                        role.Grants.Add(new RoleGrant { User = person.User });
                    }
                }
            }
            //if (roleNames != null)
            //    foreach (var role in roleNames
            //        .Where(r => !person.User.Grants.Select(g => g.Role.Name).Contains(r))
            //        //.Select(grant => EnsureRole(grant, null)))
            //        .Select(grant => Context.Roles.ByName(grant)))
            //        role.Grants.Add(new RoleGrant { User = person.User });

            Context.SaveChanges();
        }
    }
}