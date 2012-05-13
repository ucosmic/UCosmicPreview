using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public class PersonNameServices
    {
        public PersonNameServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    [Authenticate]
    public partial class PersonNameController : BaseController
    {
        #region Construction & DI

        private readonly PersonNameServices _services;

        public PersonNameController(PersonNameServices services)
        {
            _services = services;
        }

        #endregion
        #region GenerateDisplayName

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult GenerateDisplayName(GenerateDisplayNameForm model)
        {
            var query = Mapper.Map<GenerateDisplayNameQuery>(model);
            var displayName = _services.QueryProcessor.Execute(query);
            return Json(displayName);
        }

        #endregion
        #region AutoComplete Salutations & Suffixes

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult AutoCompleteSalutations(string term)
        {
            var data = _services.QueryProcessor.Execute(
                new FindDistinctSalutationsQuery
                {
                    Exclude = DefaultSalutationValues,
                }
            );

            var options = GetAutoCompleteOptions(term, data, DefaultSalutationValues);

            return Json(options, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult AutoCompleteSuffixes(string term)
        {
            var data = _services.QueryProcessor.Execute(
                new FindDistinctSuffixesQuery
                {
                    Exclude = DefaultSuffixValues,
                }
            );

            var options = GetAutoCompleteOptions(term, data, DefaultSuffixValues);

            return Json(options, JsonRequestBehavior.AllowGet);
        }

        public const string SalutationAndSuffixNullValueLabel = "[None]";
        public static readonly string[] DefaultSalutationValues = new[] { SalutationAndSuffixNullValueLabel, "Prof.", "Dr.", "Mr.", "Ms." };
        public static readonly string[] DefaultSuffixValues = new[] { SalutationAndSuffixNullValueLabel, "Jr.", "Sr.", "PhD", "Esq." };

        [NonAction]
        private static IEnumerable GetAutoCompleteOptions(string term, ICollection<string> data, IEnumerable<string> defaults)
        {
            // begin with the defaults
            var merged = defaults.ToList();

            // mix in the data
            if (data.IsNotNull() && data.Any()) merged.AddRange(data);

            // to not return anything for exact matches
            if (term.IsNotNullOrWhiteSpace())
            {
                var exact = merged.SingleOrDefault(term.Equals);
                var others = merged.Where(s => !term.Equals(s) && s.StartsWith(term));
                if (exact.IsNotNull() && !others.Any())
                    return Enumerable.Empty<object>();
            }

            // apply term
            if (term.IsNotNullOrWhiteSpace())
                merged = merged.Where(s => s != SalutationAndSuffixNullValueLabel && s.StartsWith(term)).ToList();

            // sort and convert to autocomplete anonymous object
            var options = merged.OrderBy(s => s).Select(s => new
            {
                label = s,
                value = s != SalutationAndSuffixNullValueLabel ? s : string.Empty,
            });

            return options;
        }

        #endregion
    }

    public static class PersonNameRouter
    {
        private static readonly string Area = MVC.People.Name;
        private static readonly string Controller = MVC.People.PersonName.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(PersonNameRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class GenerateDisplayName
        {
            public const string Route = "people/generate-display-name";
            private static readonly string Action = MVC.People.PersonName.ActionNames.GenerateDisplayName;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class AutoCompleteSalutations
        {
            public const string Route = "people/salutations";
            private static readonly string Action = MVC.People.PersonName.ActionNames.AutoCompleteSalutations;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class AutoCompleteSuffixes
        {
            public const string Route = "people/suffixes";
            private static readonly string Action = MVC.People.PersonName.ActionNames.AutoCompleteSuffixes;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
