using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentValidation.Mvc;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Areas.My.Services;
using UCosmic.Domain.Identity;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public partial class EmailAddressesController : BaseController
    {
        private readonly EmailAddressesServices _services;

        public EmailAddressesController(EmailAddressesServices services)
        {
            _services = services;
        }

        #region Change Email Spelling

        [HttpGet]
        [Authorize]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-spelling")]
        [ReturnUrlReferrer(SelfRouteMapper.Me.OutboundRoute)]
        public virtual ActionResult ChangeSpelling(int number)
        {
            // get the currently signed-in user / person
            var email = _services.QueryProcessor.Execute(
                new GetEmailAddressByUserNameAndNumberQuery
                {
                    UserName = User.Identity.Name,
                    Number = number,
                }
            );

            if (email != null)
                return View(Mapper.Map<ChangeSpellingForm>(email));

            return HttpNotFound();
        }

        [HttpPut]
        [Authorize]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("change-spelling")]
        public virtual ActionResult ChangeSpelling(ChangeSpellingForm model)
        {
            if (model != null)
            {
                // make sure user owns this email address
                if (model.PersonUserName != User.Identity.Name)
                {
                    
                }
            //    // get the currently signed-in person
            //    var person = _people.FindOne(PersonBy.Principal(User).ForInsertOrUpdate());
            //    if (person != null)
            //    {
            //        // ensure this person owns the email
            //        var email = person.Emails.SingleOrDefault(e => e.Number == model.Number);
            //        if (email != null)
            //        {
            //            if (ModelState.IsValid)
            //            {
            //                email.Value = model.Value;
            //                _objectCommander.SaveChanges();
            //                SetFeedbackMessage(string.Format(
            //                    "Your email address was successfully changed to {0}.",
            //                        model.Value));
            //                return RedirectToAction(MVC.Identity.Self.Me());
            //            }
            //            model.OldSpelling = email.Value;
            //            return View(model);
            //        }
            //    }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual ActionResult CheckEmailSpelling(
            [CustomizeValidator(Properties = ChangeSpellingForm.ValuePropertyName)] ChangeSpellingForm model)
        {
            return ValidateRemote(JsonRequestBehavior.DenyGet, ChangeSpellingForm.ValuePropertyName);
        }

        #endregion
    }
}
