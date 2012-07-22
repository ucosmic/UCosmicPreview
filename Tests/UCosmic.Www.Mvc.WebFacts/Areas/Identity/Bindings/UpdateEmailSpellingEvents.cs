using System.Linq;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdateEmailSpellingEvents : BaseStepDefinition
    {
        [BeforeScenario("UsingFreshExampleEmailSpellingForAny1AtUsil")]
        public static void ResetExampleEmailSpellingForAny1AtUsil()
        {
            var db = ServiceProviderLocator.Current.GetService<IWrapDataConcerns>();

            UpdatePasswordEvents.ResetExamplePasswords();
            var person = db.Commands.Get<Person>()
                .Single(p => UpdateNameEvents.Any1AtUsilDotEduDotPe.Equals(p.User.Name));
            person.DefaultEmail.Value = UpdateNameEvents.Any1AtUsilDotEduDotPe.ToLower();
            db.UnitOfWork.SaveChanges();
        }
    }
}
