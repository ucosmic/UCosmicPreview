using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public class PersonInfoServices
    {
        public PersonInfoServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

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
                new GetPersonByGuidQuery(guid)
                {
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

    public static class PersonInfoRouter
    {
        private static readonly string Area = MVC.People.Name;
        private static readonly string Controller = MVC.People.PersonInfo.Name;

        public class ByEmailRoute : Route
        {
            public ByEmailRoute()
                : base("people/by-email", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.People.PersonInfo.ActionNames.ByEmail,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ByGuidRoute : Route
        {
            public ByGuidRoute()
                : base("people/by-guid", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.People.PersonInfo.ActionNames.ByGuid,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class WithEmailRoute : Route
        {
            public WithEmailRoute()
                : base("people/with-email", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.People.PersonInfo.ActionNames.WithEmail,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class WithFirstNameRoute : Route
        {
            public WithFirstNameRoute()
                : base("people/with-first-name", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.People.PersonInfo.ActionNames.WithFirstName,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class WithLastNameRoute : Route
        {
            public WithLastNameRoute()
                : base("people/with-last-name", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.People.PersonInfo.ActionNames.WithLastName,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
