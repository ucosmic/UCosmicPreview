using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class SignOnEvents : BaseStepDefinition
    {
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
