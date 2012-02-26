using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    [TestClass]
    public class InstitutionalAgreementContactTypeValueFormTests
    {
        [TestMethod]
        public void ViewModel_InstitutionalAgreements_ConfigurationForms_InstitutionalAgreementContactTypeValueForm_ShouldBeConstructible()
        {
            var model = new InstitutionalAgreementContactTypeValueForm
            {
                Id = 1,
            };

            model.ShouldNotBeNull();
        }
    }
}
