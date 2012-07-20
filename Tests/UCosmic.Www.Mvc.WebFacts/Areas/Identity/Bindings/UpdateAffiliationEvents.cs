using System.Linq;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdateAffiliationEvents : BaseStepDefinition
    {
        [BeforeScenario("UsingFreshExampleDefaultAffiliationForAny1AtUsil")]
        public static void ResetExampleUnacknowledgedAffiliationForAny1AtUsil()
        {
            UpdatePasswordEvents.ResetExamplePasswords();

            var entities = ServiceProviderLocator.Current.GetService<IQueryEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var person = entities.Get<Person>()
                .Single(p => UpdateNameEvents.Any1AtUsilDotEduDotPe.Equals(p.User.Name));
            person.DefaultAffiliation.IsAcknowledged = false;
            person.DefaultAffiliation.JobTitles = "Dir. Co. XPR-4";
            unitOfWork.SaveChanges();
        }
    }
}
