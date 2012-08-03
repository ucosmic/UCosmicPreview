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

            var db = ServiceProviderLocator.Current.GetService<IWrapDataConcerns>();

            var person = db.Commands.Get<Person>()
                .Single(p => UpdateNameEvents.Any1AtUsilDotEduDotPe.Equals(p.User.Name));
            person.DefaultAffiliation.IsAcknowledged = false;
            person.DefaultAffiliation.JobTitles = "Dir. Co. XPR-4";
            person.DefaultAffiliation.IsClaimingInternationalOffice = false;
            person.DefaultAffiliation.IsClaimingAdministrator = false;
            person.DefaultAffiliation.IsClaimingFaculty = false;
            person.DefaultAffiliation.IsClaimingStaff = false;
            db.UnitOfWork.SaveChanges();
        }
    }
}
