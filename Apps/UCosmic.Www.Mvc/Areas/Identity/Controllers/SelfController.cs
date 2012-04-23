//using System;
//using System.Linq;
//using System.Web.Mvc;
//using UCosmic.Domain;
//using UCosmic.Domain.People;
//using UCosmic.Www.Mvc.Controllers;

//namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
//{
//    public partial class SelfController : BaseController
//    {
//        #region Construction & DI

//        private readonly PersonFinder _people;
//        //private readonly ICommandObjects _objectCommander;

//        public SelfController(IQueryEntities queryEntities)
//        {
//            _people = new PersonFinder(queryEntities);
//            //_objectCommander = objectCommander;
//        }

//        #endregion
//        #region AutoComplete Person Name

//        //public enum PersonNameProperty
//        //{
//        //    FirstName,
//        //    LastName,
//        //    DefaultEmail,
//        //}

//        //[ActionName("autocomplete-name")]
//        //[Authorize]
//        //public virtual ActionResult AutoCompletePersonName(string term, PersonNameProperty termTarget, PersonNameProperty? orderTarget = null)
//        //{
//        //    var criteria = With<Person>.DefaultCriteria();
//        //    switch (termTarget)
//        //    {
//        //        case PersonNameProperty.FirstName:
//        //            //data = _people.Current.Where(p => p.FirstName.ToLower().StartsWith(term.ToLower()));
//        //            //if (!orderTarget.HasValue)
//        //            //    data = data.OrderBy(p => p.FirstName);
//        //            criteria = PeopleWith.AutoCompleteFirstNamePrefix(term);
//        //            if (!orderTarget.HasValue)
//        //                criteria = criteria.OrderBy(p => p.FirstName);
//        //            break;

//        //        case PersonNameProperty.LastName:
//        //            //data = _people.Current.Where(p => p.LastName.ToLower().StartsWith(term.ToLower()));
//        //            //if (!orderTarget.HasValue)
//        //            //    data = data.OrderBy(p => p.LastName);
//        //            criteria = PeopleWith.AutoCompleteLastNamePrefix(term);
//        //            if (!orderTarget.HasValue)
//        //                criteria = criteria.OrderBy(p => p.LastName);
//        //            break;

//        //        case PersonNameProperty.DefaultEmail:
//        //            //data = _people.Current.Where(p => p.Emails.Any(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
//        //            //    && e.Value.ToLower().Contains(term.ToLower())));
//        //            //if (!orderTarget.HasValue)
//        //            //    data = data.OrderBy(p => p.Emails.FirstOrDefault(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted
//        //            //        && e.IsDefault).Value);
//        //            criteria = PeopleWith.AutoCompleteEmailTerm(term);
//        //            if (!orderTarget.HasValue)
//        //                criteria = criteria.OrderBy(p => p.Emails.FirstOrDefault(
//        //                    e => e.IsDefault).Value);
//        //            break;
//        //    }

//        //    if (criteria != null)
//        //    {
//        //        if (orderTarget.HasValue)
//        //        {
//        //            switch (orderTarget)
//        //            {
//        //                case PersonNameProperty.FirstName:
//        //                    criteria = criteria.OrderBy(p => p.FirstName);
//        //                    break;

//        //                case PersonNameProperty.LastName:
//        //                    criteria = criteria.OrderBy(p => p.LastName);
//        //                    break;

//        //                case PersonNameProperty.DefaultEmail:
//        //                    criteria = criteria.OrderBy(p => p.Emails.FirstOrDefault(
//        //                        e => e.IsDefault).Value);
//        //                    break;
//        //            }
//        //        }

//        //        var options = _people.FindMany(criteria).Select(p => new
//        //        {
//        //            p.EntityId,
//        //            p.FirstName,
//        //            p.LastName,
//        //            // ReSharper disable PossibleNullReferenceException
//        //            DefaultEmail = p.Emails.SingleOrDefault(e => e.IsDefault).Value,
//        //            // ReSharper restore PossibleNullReferenceException
//        //        });

//        //        return Json(options, JsonRequestBehavior.AllowGet);
//        //    }

//        //    return HttpNotFound();
//        //}

//        #endregion
//    }
//}