using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Elmah;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Mappers;
using UCosmic.Www.Mvc.Areas.Identity.Models.Self;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class SelfController : BaseController
    {
        #region Construction & DI

        private readonly PersonFinder _people;
        private readonly ICommandObjects _objectCommander;

        public SelfController(IQueryEntities queryEntities, ICommandObjects objectCommander)
        {
            _people = new PersonFinder(queryEntities);
            _objectCommander = objectCommander;
        }

        #endregion
        #region Personal Homepage
        
        [HttpGet]
        [Authorize]
        [ActionName("me")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Me()
        {
            var person = _people.FindOne(PersonBy.Principal(User));
            if (person != null)
            {
                var model = Mapper.Map<PersonForm>(person);
                model.Emails = model.Emails.OrderByDescending(e => e.IsDefault).ThenByDescending(e => e.IsConfirmed).ThenBy(e => e.Value).ToArray(); // TODO: put this in the model mapper
                //person.Emails = person.Emails.OrderBy(e => e.IsDefault).ThenBy(e => e.IsConfirmed).ThenBy(e => e.Value).ToList();
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        [ActionName("me")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Me(PersonForm model)
        {
            var person = _people.FindOne(PersonBy.Principal(User).ForInsertOrUpdate());
            if (person != null && person.RevisionId == model.RevisionId)
            {
                if (ModelState.IsValid)
                {
                    Mapper.Map(model, person);
                    if (person.IsDisplayNameDerived && !person.DisplayName.Equals(person.DeriveDisplayName()))
                    {
                        ErrorSignal.FromCurrentContext().Raise(new InvalidOperationException(string.Format(
                            "Client Person DisplayName '{0}' was not consistent with server Person DerviveDisplayName() value '{1}'.",
                            person.DisplayName, person.DeriveDisplayName())));
                    }
                    _objectCommander.Update(person, true);
                    SetFeedbackMessage("Your personal info was saved successfully.");
                    return RedirectToAction(MVC.Identity.Self.Me());
                }
                model.Emails = Mapper.Map<EmailInfo[]>( // TODO: put this in the model mapper
                    person.Emails.OrderByDescending(e => e.IsDefault).ThenByDescending(e => e.IsConfirmed).ThenByDescending(e => e.Value));
                model.Affiliations = Mapper.Map<IList<PersonForm.AffiliationInfo>>(person.Affiliations);
                return View(model);
            }
            return HttpNotFound();
        }

        #endregion
        #region Edit Affiliation

        [HttpGet]
        [Authorize]
        //[OpenTopTab(TopTabName.User)]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("edit-affiliation")]
        [ReturnUrlReferrer(SelfRouteMapper.Me.OutboundRoute)]
        public virtual ActionResult EditAffiliation(Guid? entityId)
        {
            if (entityId.HasValue && entityId.Value != Guid.Empty)
            {
                // get the affiliation by id
                var person = _people.FindOne(PersonBy.Principal(User));
                if (person != null)
                {
                    // ensure this member owns the affiliation
                    var affiliation = person.Affiliations.Current(entityId.Value);
                    if (affiliation != null)
                    {
                        var model = Mapper.Map<AffiliationForm>(affiliation);
                        model.DeriveEmployeeOrStudentAnswer();
                        return View(model);
                    }
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        //[OpenTopTab(TopTabName.User)]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("edit-affiliation")]
        public virtual ActionResult EditAffiliation(AffiliationForm model)
        {
            if (model != null)
            {
                // get the affiliation by id
                var person = _people.FindOne(PersonBy.Principal(User).ForInsertOrUpdate());
                if (person != null)
                {
                    // ensure this member owns the affiliation
                    var affiliation = person.Affiliations.Current(model.EntityId);
                    if (affiliation != null)
                    {
                        if (ModelState.IsValid)
                        {
                            model.IsAcknowledged = true;
                            model.ApplyEmployeeOrStudentAnswer();
                            Mapper.Map(model, affiliation);
                            _objectCommander.Update(affiliation, true);
                            SetFeedbackMessage("Your affiliation info was saved successfully.");
                            return RedirectToAction(MVC.Identity.Self.Me());
                        }
                        model.Establishment = Mapper.Map<AffiliationForm.EstablishmentInfo>(affiliation.Establishment);
                        return View(model);
                    }
                }
            }
            return HttpNotFound();
        }

        #endregion
        #region Json

        #region AutoComplete Salutations & Suffixes

        [ActionName("autocomplete-name-salutations")]
        public virtual ActionResult AutoCompleteNameSalutations(string term)
        {
            const string nullOptionLabel = "[None]";
            // get all of the unique salutations already in the database
            var data = _people.GetDistinctSalutations();

            // set up default examples
            var defaults = new List<string> { "Prof.", "Dr.", "Mr.", "Mrs.", };

            // integrate defaults with data
            var union = data.Union(defaults);

            // apply term
            if (!string.IsNullOrWhiteSpace(term))
            {
                union = union.AsEnumerable()
                    .Where(x => x.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                    .AsQueryable();
            }

            // add null option if term is empty
            if (string.IsNullOrWhiteSpace(term) && !string.IsNullOrWhiteSpace(nullOptionLabel))
            {
                union = union.Union(new List<string> { nullOptionLabel });
            }

            // sort & return results
            union = union.OrderBy(a => a);
            var options = union.Select(a => new AutoCompleteOption { value = a, label = a, }).ToList();
            var nullOption = options.SingleOrDefault(o => o.value == nullOptionLabel);
            if (nullOption != null)
            {
                nullOption.value = string.Empty;
            }
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        [ActionName("autocomplete-name-suffixes")]
        public virtual ActionResult AutoCompleteNameSuffixes(string term)
        {
            const string nullOptionLabel = "[None]";
            //var autoComplete = _identities.AutoCompleteNameSuffix(term, nullOptionLabel);
            // get all of the unique suffixes already in the database
            var data = _people.GetDistinctSuffixes();

            // set up default examples
            var defaults = new List<string> { "Jr.", "Sr.", "PhD", "Esq.", };

            // integrate defaults with data
            var union = data.Union(defaults);

            // apply term
            if (!string.IsNullOrWhiteSpace(term))
            {
                union = union.AsEnumerable()
                    .Where(x => x.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                    .AsQueryable();
            }

            // add null option if term is empty
            if (string.IsNullOrWhiteSpace(term) && !string.IsNullOrWhiteSpace(nullOptionLabel))
            {
                union = union.Union(new List<string> { nullOptionLabel });
            }

            // sort & return results
            union = union.OrderBy(a => a);
            var options = union.Select(a => new AutoCompleteOption { value = a, label = a, }).ToList();
            var nullOption = options.SingleOrDefault(o => o.value == nullOptionLabel);
            if (nullOption != null)
            {
                nullOption.value = string.Empty;
            }
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region AutoComplete Person Name

        public enum PersonNameProperty
        {
            FirstName,
            LastName,
            DefaultEmail,
        }

        [ActionName("autocomplete-name")]
        [Authorize]
        public virtual ActionResult AutoCompletePersonName(string term, PersonNameProperty termTarget, PersonNameProperty? orderTarget = null)
        {
            var criteria = With<Person>.DefaultCriteria();
            switch (termTarget)
            {
                case PersonNameProperty.FirstName:
                    //data = _people.Current.Where(p => p.FirstName.ToLower().StartsWith(term.ToLower()));
                    //if (!orderTarget.HasValue)
                    //    data = data.OrderBy(p => p.FirstName);
                    criteria = PeopleWith.AutoCompleteFirstNamePrefix(term);
                    if (!orderTarget.HasValue)
                        criteria = criteria.OrderBy(p => p.FirstName);
                    break;

                case PersonNameProperty.LastName:
                    //data = _people.Current.Where(p => p.LastName.ToLower().StartsWith(term.ToLower()));
                    //if (!orderTarget.HasValue)
                    //    data = data.OrderBy(p => p.LastName);
                    criteria = PeopleWith.AutoCompleteLastNamePrefix(term);
                    if (!orderTarget.HasValue)
                        criteria = criteria.OrderBy(p => p.LastName);
                    break;

                case PersonNameProperty.DefaultEmail:
                    //data = _people.Current.Where(p => p.Emails.Any(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
                    //    && e.Value.ToLower().Contains(term.ToLower())));
                    //if (!orderTarget.HasValue)
                    //    data = data.OrderBy(p => p.Emails.FirstOrDefault(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
                    //        && e.IsDefault).Value);
                    criteria = PeopleWith.AutoCompleteEmailTerm(term);
                    if (!orderTarget.HasValue)
                        criteria = criteria.OrderBy(p => p.Emails.FirstOrDefault(
                            e => e.IsDefault).Value);
                    break;
            }

            if (criteria != null)
            {
                if (orderTarget.HasValue)
                {
                    switch (orderTarget)
                    {
                        case PersonNameProperty.FirstName:
                            criteria = criteria.OrderBy(p => p.FirstName);
                            break;

                        case PersonNameProperty.LastName:
                            criteria = criteria.OrderBy(p => p.LastName);
                            break;

                        case PersonNameProperty.DefaultEmail:
                            criteria = criteria.OrderBy(p => p.Emails.FirstOrDefault(
                                e => e.IsDefault).Value);
                            break;
                    }
                }

                var options = _people.FindMany(criteria).Select(p => new
                {
                    p.EntityId,
                    p.FirstName,
                    p.LastName,
                    // ReSharper disable PossibleNullReferenceException
                    DefaultEmail = p.Emails.SingleOrDefault(e => e.IsDefault).Value,
                    // ReSharper restore PossibleNullReferenceException
                });

                return Json(options, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        [Authorize]
        [ActionName("person-info-by-id")]
        public virtual ActionResult PersonInfoById(Guid personId)
        {
            var data = _people.FindOne(By<Person>.EntityId(personId));
            if (data != null)
            {
                var defaultEmail = data.Emails.SingleOrDefault(e => e.IsDefault);
                var person = new
                {
                    data.EntityId,
                    data.Salutation,
                    data.FirstName,
                    data.MiddleName,
                    data.LastName,
                    data.Suffix,
                    DefaultEmail = (defaultEmail != null) ? defaultEmail.Value : null,
                };
                return Json(person, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [ActionName("person-info-by-email")]
        [Authorize]
        public virtual ActionResult PersonInfoByEmail(string email)
        {
            var data = _people.FindOne(PersonBy.EmailAddress(email));
            if (data != null)
            {
                var defaultEmail = data.Emails.SingleOrDefault(e => e.IsDefault);
                var person = new
                {
                    data.EntityId,
                    data.Salutation,
                    data.FirstName,
                    data.MiddleName,
                    data.LastName,
                    data.Suffix,
                    DefaultEmail = (defaultEmail != null) ? defaultEmail.Value : null,
                };
                return Json(person, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Derive Person DisplayName

        [HttpPost]
        [ActionName("derive-display-name")]
        public virtual ActionResult DeriveDisplayName(PersonForm model)
        {
            var person = Mapper.Map<Person>(model);
            model.DisplayName = person.DeriveDisplayName();
            return Json(model);
        }

        #endregion

        #endregion
    }
}