using System.Linq;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.Identity;

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

            var db = ServiceProviderLocator.Current.GetService<IWrapDataConcerns>();

            var role = new Role
            {
                Name = TestRoleName,
                Description = "This role is for testing in the web facts project",
            };
            db.Commands.Create(role);
            db.UnitOfWork.SaveChanges();
        }

        [AfterTestRun]
        [AfterScenario("UsingFreshExampleRoleData")]
        public static void RemoveExampleRoleData()
        {
            var db = ServiceProviderLocator.Current.GetService<IWrapDataConcerns>();

            var role = db.Commands.Get<Role>().SingleOrDefault(r => TestRoleName.Equals(r.Name));
            if (role == null) return;
            db.Commands.Purge(role);
            db.UnitOfWork.SaveChanges();
        }
    }
}
