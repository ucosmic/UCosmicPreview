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

            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            foreach (var memberToClear in membersToClear)
            {
                var member = Membership.GetUser(memberToClear);
                if (member != null)
                    Membership.DeleteUser(memberToClear);

                var person = entities.Get<Person>().SingleOrDefault(p => p.User != null
                    && memberToClear.Equals(p.User.Name, StringComparison.OrdinalIgnoreCase))
                    ?? entities.Get<Person>().SingleOrDefault(p => p.Emails.Any(
                        e => memberToClear.Equals(e.Value, StringComparison.OrdinalIgnoreCase)));

                if (person == null) continue;
                if (person.User != null)
                    entities.Purge(person.User);
                entities.Purge(person);
            }
            unitOfWork.SaveChanges();
        }
    }
}
