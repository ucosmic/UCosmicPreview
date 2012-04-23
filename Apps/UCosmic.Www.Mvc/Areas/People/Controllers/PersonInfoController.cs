using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    [Authorize]
    public partial class PersonInfoController : BaseController
    {
        private readonly PersonInfoServices _services;

        public PersonInfoController(PersonInfoServices services)
        {
            _services = services;
        }

        private static readonly IEnumerable<Expression<Func<Person, object>>> PersonInfoEagerLoad =
            new Expression<Func<Person, object>>[]
            {
                p => p.Emails,
            };

        [HttpPost]
        public virtual JsonResult ByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return Json(null);

            var person = _services.QueryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = email,
                    EagerLoad = PersonInfoEagerLoad,
                }
            );

            var model = person != null
                ? Mapper.Map<PersonInfoModel>(person)
                : null;

            return Json(model);
        }

        [HttpPost]
        public virtual JsonResult ByGuid(Guid guid)
        {
            if (guid == Guid.Empty) return Json(null);

            var person = _services.QueryProcessor.Execute(
                new GetPersonByGuidQuery
                {
                    Guid = guid,
                    EagerLoad = PersonInfoEagerLoad,
                }
            );

            var model = person != null
                ? Mapper.Map<PersonInfoModel>(person)
                : null;

            return Json(model);
        }

        [HttpPost]
        public virtual JsonResult WithEmail(string term, StringMatchStrategy matchStrategy = StringMatchStrategy.Equals)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json(null);

            var people = _services.QueryProcessor.Execute(
                new FindPeopleWithEmailQuery
                {
                    Term = term,
                    TermMatchStrategy = matchStrategy,
                    EagerLoad = PersonInfoEagerLoad,
                    OrderBy = new Dictionary<Expression<Func<Person, object>>, OrderByDirection>
                    {
                        { p => p.Emails.FirstOrDefault(e => e.IsDefault).Value, OrderByDirection.Ascending },
                    },
                }
            );

            var models = Mapper.Map<PersonInfoModel[]>(people);

            return Json(models);
        }

        [HttpPost]
        public virtual JsonResult WithFirstName(string term, StringMatchStrategy matchStrategy = StringMatchStrategy.Equals)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json(null);

            var people = _services.QueryProcessor.Execute(
                new FindPeopleWithFirstNameQuery
                {
                    Term = term,
                    TermMatchStrategy = matchStrategy,
                    EagerLoad = PersonInfoEagerLoad,
                    OrderBy = new Dictionary<Expression<Func<Person, object>>, OrderByDirection>
                    {
                        { p => p.FirstName, OrderByDirection.Ascending },
                    },
                }
            );

            var models = Mapper.Map<PersonInfoModel[]>(people);

            return Json(models);
        }

        [HttpPost]
        public virtual JsonResult WithLastName(string term, StringMatchStrategy matchStrategy = StringMatchStrategy.Equals)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json(null);

            var people = _services.QueryProcessor.Execute(
                new FindPeopleWithLastNameQuery
                {
                    Term = term,
                    TermMatchStrategy = matchStrategy,
                    EagerLoad = PersonInfoEagerLoad,
                    OrderBy = new Dictionary<Expression<Func<Person, object>>, OrderByDirection>
                    {
                        { p => p.LastName, OrderByDirection.Ascending },
                    },
                }
            );

            var models = Mapper.Map<PersonInfoModel[]>(people);

            return Json(models);
        }
    }
}
