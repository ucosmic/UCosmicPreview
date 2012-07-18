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

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(PersonInfoRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class ByEmail
        {
            public const string Route = "people/by-email";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.ByEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ByGuid
        {
            public const string Route = "people/by-guid";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.ByGuid;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class WithEmail
        {
            public const string Route = "people/with-email";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.WithEmail;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class WithFirstName
        {
            public const string Route = "people/with-first-name";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.WithFirstName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class WithLastName
        {
            public const string Route = "people/with-last-name";
            private static readonly string Action = MVC.People.PersonInfo.ActionNames.WithLastName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
