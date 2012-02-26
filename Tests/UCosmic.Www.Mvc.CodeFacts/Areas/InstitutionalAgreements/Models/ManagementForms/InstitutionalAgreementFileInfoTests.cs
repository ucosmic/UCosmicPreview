using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    [TestClass]
    public class InstitutionalAgreementFileInfoTests
    {
        [TestMethod]
        public void ViewModel_InstitutionalAgreements_ManagementForms_InstitutionalAgreementFileInfo_ShouldBeConstructible()
        {
            var model = new InstitutionalAgreementFileInfo
            {
                RevisionId = 1,
                EntityId = Guid.NewGuid(),
                AgreementEntityId = Guid.NewGuid(),
                Length = 1,
                MimeType = "mime type",
                Name = "name",
            };

            model.ShouldNotBeNull();
            model.RevisionId.ShouldEqual(1);
            model.EntityId.ShouldNotEqual(Guid.Empty);
            model.AgreementEntityId.ShouldNotEqual(Guid.Empty);
            model.Length.ShouldEqual(1);
            model.MimeType.ShouldNotBeNull();
        }
    }
}
