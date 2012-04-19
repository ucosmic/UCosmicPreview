using System.Web.Mvc;
using System.Web.UI;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.People.Models;
using UCosmic.Www.Mvc.Controllers;
using AutoMapper;

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
    }
}
