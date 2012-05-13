using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Domain.Identity;
using UCosmic.Impl.Orm;

namespace UCosmic.Www.Mvc.Areas.Roles
{
    [Binding]
    public class ManageEvents : BaseStepDefinition
    {
        private const string TestRoleName = "Test Role";

        [BeforeTestRun]
        [BeforeScenario("UsingFreshExampleRoleData")]
        public static void AddExampleRoleData()
        {
            RemoveExampleRoleData();
            using (var context = new UCosmicContext(null))
            {
                var role = new Role
                {
                    Name = TestRoleName,
                    Description = "This role is for testing in the web facts project",
                };
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }

        [AfterTestRun]
        [AfterScenario("UsingFreshExampleRoleData")]
        public static void RemoveExampleRoleData()
        {
            using (var context = new UCosmicContext(null))
            {
                var role = context.Roles.SingleOrDefault(r => TestRoleName.Equals(r.Name));
                if (role == null) return;
                context.Roles.Remove(role);
                context.SaveChanges();
            }
        }
    }
}
