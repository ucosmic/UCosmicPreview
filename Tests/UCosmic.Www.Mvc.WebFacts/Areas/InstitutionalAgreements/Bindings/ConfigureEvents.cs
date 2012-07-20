using System.Linq;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    [Binding]
    public class ConfigureEvents : BaseStepDefinition
    {
        [BeforeTestRun]
        [AfterTestRun]
        public static void RemoveUcInstitutionalAgreementConfiguration()
        {
            RemoveInstitutionalAgreementConfigurationFor("www.uc.edu");
        }

        [BeforeScenario("UsingExampleUnconfiguredInstitutionalAgreementModules")]
        public static void RemoveSideEffectInstitutionalAgreementConfigurations()
        {
            RemoveInstitutionalAgreementConfigurationFor("www.lehigh.edu");
            RemoveInstitutionalAgreementConfigurationFor("www.umn.edu");
            RemoveInstitutionalAgreementConfigurationFor("www.usil.edu.pe");
            RemoveInstitutionalAgreementConfigurationFor("www.napier.ac.uk");
            RemoveInstitutionalAgreementConfigurationFor("www.suny.edu");
            RemoveInstitutionalAgreementConfigurationFor("www.bjtu.edu.cn");
        }

        private static void RemoveInstitutionalAgreementConfigurationFor(string url)
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var config = entities.Get<InstitutionalAgreementConfiguration>().SingleOrDefault(c =>
                url.Equals(c.ForEstablishment.WebsiteUrl));
            if (config == null) return;
            entities.Purge(config);
            unitOfWork.SaveChanges();
        }
    }
}
