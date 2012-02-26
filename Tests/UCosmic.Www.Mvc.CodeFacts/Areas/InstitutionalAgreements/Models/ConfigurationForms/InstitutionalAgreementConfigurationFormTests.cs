using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    [TestClass]
    public class InstitutionalAgreementConfigurationFormTests
    {
        [TestMethod]
        public void ViewModel_InstitutionalAgreements_ConfigurationForms_InstitutionalAgreementConfigurationForm_ShouldBeConstructible()
        {
            var model = new InstitutionalAgreementConfigurationForm
            {
                RevisionId = 1,
                ForEstablishmentId = null,
                EntityId = Guid.NewGuid(),
                IsCustomTypeAllowed = false,
                IsCustomStatusAllowed = false,
                IsCustomContactTypeAllowed = false,
                AllowedTypeValues = new List<InstitutionalAgreementTypeValueForm>(),
                AllowedStatusValues = new List<InstitutionalAgreementStatusValueForm>(),
                AllowedContactTypeValues = new List<InstitutionalAgreementContactTypeValueForm>(),
                ExampleTypeInput = "Example",
                ExampleStatusInput = "Example",
                ExampleContactTypeInput = "Example",
            };

            model.ShouldNotBeNull();
            model.EntityId.ShouldNotEqual(Guid.Empty);
            model.ForEstablishmentId.ShouldBeNull();
        }

    }
}
