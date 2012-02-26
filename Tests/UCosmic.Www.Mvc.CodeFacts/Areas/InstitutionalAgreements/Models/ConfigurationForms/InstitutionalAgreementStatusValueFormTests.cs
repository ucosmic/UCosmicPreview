using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    [TestClass]
    public class InstitutionalAgreementStatusValueFormTests
    {
        [TestMethod]
        public void ViewModel_InstitutionalAgreements_ConfigurationForms_InstitutionalAgreementStatusValueForm_ShouldBeConstructible()
        {
            var model = new InstitutionalAgreementStatusValueForm
            {
                Id = 1,
            };

            model.ShouldNotBeNull();
        }
    }
}
