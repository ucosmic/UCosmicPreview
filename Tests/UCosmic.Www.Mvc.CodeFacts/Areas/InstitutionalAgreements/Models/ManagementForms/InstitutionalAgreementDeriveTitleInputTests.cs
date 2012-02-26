using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    [TestClass]
    public class InstitutionalAgreementDeriveTitleInputTests
    {
        [TestMethod]
        public void ViewModel_InstitutionalAgreements_ManagementForms_InstitutionalAgreementDeriveTitleInput_ShouldBeConstructible()
        {
            var model = new InstitutionalAgreementDeriveTitleInput
            {
                IsTitleDerived = false,
                Type = "type",
                ParticipantEstablishmentIds = null,
                Status = "status",
                Title = "title",
            };

            model.ShouldNotBeNull();
            model.IsTitleDerived.ShouldBeFalse();
            model.Title.ShouldNotBeNull();
            model.Type.ShouldNotBeNull();
            model.Status.ShouldNotBeNull();
        }
    }
}
