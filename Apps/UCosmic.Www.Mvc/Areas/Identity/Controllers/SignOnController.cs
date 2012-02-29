using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignOn;
using UCosmic.Www.Mvc.Controllers;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class SignOnController : BaseController
    {
        #region Construction & DI

        private readonly IQueryEntities _queryEntities = null;

        public SignOnController(IQueryEntities queryEntities)
        {
            _queryEntities = queryEntities;
        }

        #endregion
        #region SignOn

        [HttpGet]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Begin(string returnUrl)
        {
            var model = new SignOnBeginForm();
            return View(model);
        }

        [HttpPost]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Begin(SignOnBeginForm model)
        {
            if (ModelState.IsValid)
            {
                if (model.EmailAddress.EndsWith("@testshib.org", StringComparison.OrdinalIgnoreCase))
                {
                    // return page with info on SAML SSO next step
                }
                
            }
            return View(model);
        }

        #endregion
        #region Saml2Integrations

        [HttpGet]
        [ActionName("providers")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Saml2Integrations()
        {
            // make sure context is not tracked
            var query = _queryEntities.ApplyInsertOrUpdate(_queryEntities.Establishments, 
                With<Establishment>.DefaultCriteria().ForInsertOrUpdate(false));

            // find establishments with a valid saml2 metadata url
            query = query.Where(e =>
                e.SamlSignOn != null &&
                e.SamlSignOn.MetadataUrl != null &&
                e.SamlSignOn.MetadataUrl.Length > 0
            ).OrderBy(e => e.OfficialName);

            var models = Mapper.Map<IEnumerable<Saml2IntegrationInfo>>(query.ToList());
            return View(models);
        }

        #endregion
    }
}
