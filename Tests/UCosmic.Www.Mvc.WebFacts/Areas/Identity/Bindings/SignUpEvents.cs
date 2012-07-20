using System;
using System.Linq;
using System.Web.Security;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignUpEvents : TestRunEvents
    {
        [BeforeScenario("UsingFreshExampleUnregisteredEmailAddresses")]
        [AfterScenario("UsingFreshExampleUnregisteredEmailAddresses")]
        public static void ResetExampleUnregisteredEmailAddresses()
        {
            var membersToClear = new[] { "new@bjtu.edu.cn", "new@usil.edu.pe", "new@griffith.edu.au" };

            var db = ServiceProviderLocator.Current.GetService<IWrapDataConcerns>();

            foreach (var memberToClear in membersToClear)
            {
                var member = Membership.GetUser(memberToClear);
                if (member != null)
                    Membership.DeleteUser(memberToClear);

                var person = db.Queries.Get<Person>().SingleOrDefault(p => p.User != null
                    && memberToClear.Equals(p.User.Name, StringComparison.OrdinalIgnoreCase))
                    ?? db.Queries.Get<Person>().SingleOrDefault(p => p.Emails.Any(
                        e => memberToClear.Equals(e.Value, StringComparison.OrdinalIgnoreCase)));

                if (person == null) continue;
                if (person.User != null)
                    db.Commands.Purge(person.User);
                db.Commands.Purge(person);
            }
            db.UnitOfWork.SaveChanges();
        }
    }
}
