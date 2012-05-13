using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Impl.Orm;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdateAffiliationEvents : BaseStepDefinition
    {
        [BeforeScenario("UsingFreshExampleDefaultAffiliationForAny1AtUsil")]
        public static void ResetExampleUnacknowledgedAffiliationForAny1AtUsil()
        {
            UpdatePasswordEvents.ResetExamplePasswords();
            using (var context = new UCosmicContext(null))
            {
                var person = context.People.Single(p => UpdateNameEvents.Any1AtUsilDotEduDotPe.Equals(p.User.Name));
                person.DefaultAffiliation.IsAcknowledged = false;
                person.DefaultAffiliation.JobTitles = "Dir. Co. XPR-4";
                context.SaveChanges();
            }
        }
    }
}
