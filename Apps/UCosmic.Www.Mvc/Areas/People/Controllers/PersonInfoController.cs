using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
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

        [HttpPost]
        public virtual JsonResult ByEmail(string email)
        {
            var person = _services.QueryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = email,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails,
                    },
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
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails,
                    },
                }
            );

            var model = person != null
                ? Mapper.Map<PersonInfoModel>(person)
                : null;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}
