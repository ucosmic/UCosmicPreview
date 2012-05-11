using System.Data;
using System.Data.Entity;
using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Domain.Establishments;
using UCosmic.Impl.Orm;
using UCosmic.Www.Mvc.SpecFlow;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignOnBindings : BaseStepDefinition
    {
        [Given(@"I am not signed on")]
        [When(@"I am not signed on")]
        public void SignOut()
        {
            new NavigationSteps().GoToPage(NamedUrl.SignOut);
        }

        //[BeforeTestRun]
        //public static void PretendUcIsNotSamlEnabled()
        //{
        //    using (var context = new UCosmicContext(null))
        //    {
        //        var uc = context.Establishments
        //            .Include(e => e.SamlSignOn)
        //            .Single(e => e.WebsiteUrl == "www.uc.edu")
        //        ;
        //        if (!uc.HasSamlSignOn()) return;
        //        _ucSamlEntityId = uc.SamlSignOn.EntityId;
        //        _ucSamlMetadataUrl = uc.SamlSignOn.MetadataUrl;
        //        context.Entry(uc.SamlSignOn).State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}
        //private static string _ucSamlEntityId;
        //private static string _ucSamlMetadataUrl;

        //[AfterTestRun]
        //public static void StopPretendingUcIsNotSamlEnabled()
        //{
        //    using (var context = new UCosmicContext(null))
        //    {
        //        var uc = context.Establishments
        //            .Single(e => e.WebsiteUrl == "www.uc.edu")
        //        ;
        //        var samlSignOn = new EstablishmentSamlSignOn
        //        {
        //            EntityId = _ucSamlEntityId ?? "https://qalogin.uc.edu/idp/shibboleth",
        //            MetadataUrl = _ucSamlMetadataUrl ?? "https://qalogin.uc.edu/idp/profile/Metadata/SAML",
        //        };
        //        uc.SamlSignOn = samlSignOn;
        //        context.SaveChanges();
        //    }
        //}
    }
}
