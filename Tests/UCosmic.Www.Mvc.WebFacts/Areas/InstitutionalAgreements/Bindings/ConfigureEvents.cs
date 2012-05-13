using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Impl.Orm;

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
            using (var context = new UCosmicContext(null))
            {
                var config = context.InstitutionalAgreementConfigurations.SingleOrDefault(c =>
                    url.Equals(c.ForEstablishment.WebsiteUrl));
                if (config == null) return;
                context.InstitutionalAgreementConfigurations.Remove(config);
                context.SaveChanges();
            }
        }
    }
}
