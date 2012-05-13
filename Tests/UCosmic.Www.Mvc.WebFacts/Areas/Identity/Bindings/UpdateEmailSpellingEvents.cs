using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Impl.Orm;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdateEmailSpellingEvents : BaseStepDefinition
    {
        [BeforeScenario("UsingFreshExampleEmailSpellingForAny1AtUsil")]
        public static void ResetExampleEmailSpellingForAny1AtUsil()
        {
            UpdatePasswordEvents.ResetExamplePasswords();
            using (var context = new UCosmicContext(null))
            {
                var person = context.People.Single(p => UpdateNameEvents.Any1AtUsilDotEduDotPe.Equals(p.User.Name));
                person.DefaultEmail.Value = UpdateNameEvents.Any1AtUsilDotEduDotPe.ToLower();
                context.SaveChanges();
            }
        }
    }
}
