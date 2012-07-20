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

            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var role = new Role
            {
                Name = TestRoleName,
                Description = "This role is for testing in the web facts project",
            };
            entities.Create(role);
            unitOfWork.SaveChanges();
        }

        [AfterTestRun]
        [AfterScenario("UsingFreshExampleRoleData")]
        public static void RemoveExampleRoleData()
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var role = entities.Get<Role>().SingleOrDefault(r => TestRoleName.Equals(r.Name));
            if (role == null) return;
            entities.Purge(role);
            unitOfWork.SaveChanges();
        }
    }
}
