using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    [TestClass]
    public class InstitutionalAgreementTypeValueFormTests
    {
        [TestMethod]
        public void ViewModel_InstitutionalAgreements_ConfigurationForms_InstitutionalAgreementTypeValueForm_ShouldBeConstructible()
        {
            var model = new InstitutionalAgreementTypeValueForm
            {
                Id = 1,
            };

            model.ShouldNotBeNull();
        }
    }
}
