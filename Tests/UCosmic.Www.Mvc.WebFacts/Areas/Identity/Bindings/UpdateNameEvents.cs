using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Impl.Orm;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdateNameEvents : BaseStepDefinition
    {
        public const string Any1AtUsilDotEduDotPe = "any1@usil.edu.pe";

        [BeforeScenario("UsingFreshExamplePersonNameForAny1AtUsil")]
        public static void ResetExamplePersonNameForAny1AtUsil()
        {
            UpdatePasswordEvents.ResetExamplePasswords();
            using (var context = new UCosmicContext(null))
            {
                var person = context.People.Single(p => Any1AtUsilDotEduDotPe.Equals(p.User.Name));
                person.DisplayName = Any1AtUsilDotEduDotPe;
                person.Salutation = null;
                person.FirstName = null;
                person.MiddleName = null;
                person.LastName = null;
                person.Suffix = null;
                context.SaveChanges();
            }
        }
    }
}
