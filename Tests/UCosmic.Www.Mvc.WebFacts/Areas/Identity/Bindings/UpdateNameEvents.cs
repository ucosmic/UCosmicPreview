using System.Linq;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdateNameEvents : BaseStepDefinition
    {
        public const string Any1AtUsilDotEduDotPe = "any1@usil.edu.pe";

        [BeforeScenario("UsingFreshExamplePersonNameForAny1AtUsil")]
        public static void ResetExamplePersonNameForAny1AtUsil()
        {
            var entities = ServiceProviderLocator.Current.GetService<IQueryEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            UpdatePasswordEvents.ResetExamplePasswords();
                var person = entities.Get<Person>().Single(p => Any1AtUsilDotEduDotPe.Equals(p.User.Name));
                person.DisplayName = Any1AtUsilDotEduDotPe;
                person.Salutation = null;
                person.FirstName = null;
                person.MiddleName = null;
                person.LastName = null;
                person.Suffix = null;
                unitOfWork.SaveChanges();
        }
    }
}
