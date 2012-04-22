using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    [Authorize]
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
        [OutputCache(VaryByParam = "*", Duration = 1800, Location = OutputCacheLocation.Server)]
        public virtual JsonResult GenerateDisplayName(GenerateDisplayNameForm model)
        {
            var query = Mapper.Map<GenerateDisplayNameQuery>(model);
            var displayName = _services.QueryProcessor.Execute(query);
            return Json(displayName);
        }

        #endregion
        #region AutoComplete Salutations & Suffixes

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 1800, Location = OutputCacheLocation.Server)]
        public virtual JsonResult AutoCompleteSalutations(string term)
        {
            var data = _services.QueryProcessor.Execute(
                new FindDistinctSalutationsQuery
                {
                    Exclude = DefaultSalutationValues,
                }
            );

            var options = GetAutoCompleteOptions(data, DefaultSalutationValues);

            return Json(options, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 1800, Location = OutputCacheLocation.Server)]
        public virtual JsonResult AutoCompleteSuffixes(string term)
        {
            var data = _services.QueryProcessor.Execute(
                new FindDistinctSuffixesQuery
                {
                    Exclude = DefaultSuffixValues,
                }
            );

            var options = GetAutoCompleteOptions(data, DefaultSuffixValues);

            return Json(options, JsonRequestBehavior.AllowGet);
        }

        public const string SalutationAndSuffixNullValueLabel = "[None]";
        public static readonly string[] DefaultSalutationValues = new[] { SalutationAndSuffixNullValueLabel, "Prof.", "Dr.", "Mr.", "Ms." };
        public static readonly string[] DefaultSuffixValues = new[] { SalutationAndSuffixNullValueLabel, "Jr.", "Sr.", "PhD", "Esq." };

        [NonAction]
        private static IEnumerable GetAutoCompleteOptions(ICollection<string> data, IEnumerable<string> defaults)
        {
            // begin with the defaults
            var merged = defaults.ToList();

            // mix in the data
            if (data != null && data.Count > 0) merged.AddRange(data);

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
}
