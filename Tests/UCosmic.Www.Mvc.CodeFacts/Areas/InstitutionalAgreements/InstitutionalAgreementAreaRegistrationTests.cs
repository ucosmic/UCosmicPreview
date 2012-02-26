using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Should;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    [TestClass]
    public class InstitutionalAgreementsAreaRegistrationTests
    {
        [TestMethod]
        public void AreaRegistration_InstitutionalAgreements_HasCorrectAreaName()
        {
            var areaRegistration = new InstitutionalAgreementsAreaRegistration();
            areaRegistration.AreaName.ShouldEqual("InstitutionalAgreements");

            MVC.InstitutionalAgreements.Name.DefaultAreaRoutes().ShouldMapToNothing();
            MVC.InstitutionalAgreements.Name.DefaultAreaRoutes(MVC.InstitutionalAgreements.ConfigurationForms.Name).ShouldMapToNothing();
            MVC.InstitutionalAgreements.Name.DefaultAreaRoutes(MVC.InstitutionalAgreements.ManagementForms.Name).ShouldMapToNothing();
            MVC.InstitutionalAgreements.Name.DefaultAreaRoutes(MVC.InstitutionalAgreements.PublicSearch.Name).ShouldMapToNothing();
        }

        #region JsonApi

        [TestMethod]
        public void Route_Establishments_JsonApi_AutoCompleteEstablishmentNames_IsSetUp()
        {
            Expression<Func<ManagementFormsController, ActionResult>> action = 
                controller => controller.AutoCompleteEstablishmentNames(null, null);
            const string routeUrl = "institutional-agreements/autocomplete/official-name.json";
            var url = routeUrl.ToAppRelativeUrl();

            url.WithMethod(HttpVerbs.Get).ShouldMapTo(action);
            url.WithMethodsExcept(HttpVerbs.Get).ShouldMapToNothing();
            action.DefaultAreaRoutes(MVC.InstitutionalAgreements.Name).ShouldMapToNothing();
        }

        #endregion
    }
}
