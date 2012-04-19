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
        private readonly PersonNameServices _services;

        public PersonNameController(PersonNameServices services)
        {
            _services = services;
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800, Location = OutputCacheLocation.Server)]
        public virtual JsonResult GenerateDisplayName(GenerateDisplayNameForm model)
        {
            var query = Mapper.Map<GenerateDisplayNameQuery>(model);
            var displayName = _services.QueryProcessor.Execute(query);
            return Json(displayName);
        }

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 1800, Location = OutputCacheLocation.Server)]
        public virtual JsonResult AutoCompleteSalutations(string term)
        {
            var data = new List<string> { SalutationAndSuffixNullValueLabel };
            data.AddRange(DefaultSalutationValues);

            var results = _services.QueryProcessor.Execute(
                new FindDistinctSalutationsQuery
                {
                    Exclude = data.ToArray(),
                }
            );
            if (results != null && results.Length > 0)
                data.AddRange(results);

            var options = data.OrderBy(s => s).Select(s => new
            {
                label = s,
                value = s != SalutationAndSuffixNullValueLabel ? s : string.Empty,
            });
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        public const string SalutationAndSuffixNullValueLabel = "[None]";
        public static readonly string[] DefaultSalutationValues = new[] { "Prof.", "Dr.", "Mr.", "Ms." };
    }
}
