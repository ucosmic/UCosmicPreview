using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Languages;
using UCosmic.Www.Mvc.Areas.Establishments.Models.ManagementForms;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Establishments.Controllers
{
    public partial class ManagementFormsController : BaseController
    {
        #region Construction & DI

        private readonly IProcessQueries _queryProcessor;
        //private readonly EstablishmentFinder _establishments;
        //private readonly EstablishmentTypeFinder _establishmentTypes2;
        //private readonly LanguageFinder _languages;

        public ManagementFormsController(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            //_establishments = new EstablishmentFinder(entityQueries);
            //_establishmentTypes2 = new EstablishmentTypeFinder(entityQueries);
            //_languages = new LanguageFinder(entityQueries);
        }

        #endregion
        #region List

        [ActionName("browse")]
        [Authorize(Users = "Daniel.Ludwig@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual ActionResult Browse()
        {
            //var entities = _establishments.FindMany(new EstablishmentQuery().OrderBy(e => e.OfficialName));
            var entities = _queryProcessor.Execute(new FindAllEstablishmentsQuery
            {
                OrderBy = new Dictionary<Expression<Func<Establishment, object>>, OrderByDirection>
                {
                    { e => e.OfficialName, OrderByDirection.Ascending },
                },
            });
            var models = Mapper.Map<IEnumerable<EstablishmentSearchResult>>(entities);
            return View(models);
        }

        #endregion
        #region Form

        [HttpGet]
        [ActionName("form")]
        [NullLayoutOnAjaxRequest]
        public virtual ActionResult Form(Guid? entityId = null)
        {
            // do not process empty Guid
            if (entityId.HasValue && entityId.Value == Guid.Empty) return HttpNotFound();

            EstablishmentForm model;
            if (entityId.HasValue)
            {
                // only check the db when the entity id is present
                //var establishment = _establishments.FindOne(By<Establishment>.EntityId(entityId.Value));
                var establishment = _queryProcessor.Execute(new EstablishmentByGuid(entityId.Value));
                if (establishment == null) return HttpNotFound();
                model = Mapper.Map<EstablishmentForm>(establishment);
            }
            else
            {
                model = new EstablishmentForm { EntityId = Guid.NewGuid() };
            }

            return EstablishmentFormView(model);
        }

        [HttpPut]
        [ActionName("put")]
        [NullLayoutOnAjaxRequest]
        public virtual ActionResult Put(EstablishmentForm model)
        {
            if (ModelState.IsValid)
            {
                //var establishment = Mapper.Map<Establishment>(model);
            }
            return EstablishmentFormView(model);
        }

        [NonAction]
        private ViewResult EstablishmentFormView(EstablishmentForm model)
        {
            model.Type.Options = GetTypeOptions();
            model.Names.Insert(0, new EstablishmentForm.EstablishmentNameForm());
            model.Names.ToList().ForEach(i =>
                i.TranslationToLanguage.Options = GetLanguageOptions());
            return View(Views.form, model);
        }

        #endregion
        #region SelectLists
        [NonAction]
        private GroupedSelectListItem[] GetTypeOptions()
        {
            return _typeOptions ??
            (
                //_typeOptions = _establishmentTypes2
                //    .FindMany(With<EstablishmentType>.DefaultCriteria()
                //        .OrderBy(p => p.Category.EnglishName)
                //        .OrderBy(p => p.EnglishName)
                //    )
                _typeOptions = _queryProcessor.Execute(new FindAllEstablishmentTypesQuery
                {
                    OrderBy = new Dictionary<Expression<Func<EstablishmentType, object>>, OrderByDirection>
                    {
                        { t => t.Category.EnglishName, OrderByDirection.Ascending },
                        { t => t.EnglishName, OrderByDirection.Ascending },
                    },
                })
                .Select(e => new GroupedSelectListItem
                {
                    GroupKey = e.Category.RevisionId.ToInvariantString(),
                    GroupName = e.Category.EnglishName,
                    Text = e.EnglishName,
                    Value = e.RevisionId.ToInvariantString(),
                })
                .ToArray()
            );
        }
        private GroupedSelectListItem[] _typeOptions;

        [NonAction]
        private SelectListItem[] GetLanguageOptions()
        {
            return _languageOptions ??
            (
                //_languageOptions = _languages
                //    .FindMany(With<Language>.DefaultCriteria())
                //    .Select(e => new SelectListItem
                //        {
                //            Text = e.TranslatedName.Text,
                //            Value = e.RevisionId.ToInvariantString(),
                //        }
                //    )
                //    .OrderBy(s => s.Text)
                //    .ToArray()
                _languageOptions = _queryProcessor
                    .Execute(new FindAllLanguagesQuery())
                    .Select(l =>
                        new SelectListItem
                        {
                            Text = l.TranslatedName.Text,
                            Value = l.RevisionId.ToInvariantString(),
                        })
                    .OrderBy(s => s.Text)
                    .ToArray()
            );
        }
        private SelectListItem[] _languageOptions;

        #endregion
        #region Partials

        [ActionName("new-establishment-alternatename-option")]
        [Authorize(Users = "Daniel.Ludwig@uc.edu")]
        //[Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual PartialViewResult NewName()
        {
            var model = new EstablishmentForm.EstablishmentNameForm(GetLanguageOptions());
            return PartialView(GetEditorTemplateViewName(Area, Name,
                Views.EditorTemplates.EstablishmentNameForm), model);
        }

        #endregion
        #region Json

        [ActionName("validate-duplicate-option")]
        [Authorize(Users = "Daniel.Ludwig@uc.edu")]
        public virtual JsonResult ValidateDuplicateOption(string name, List<string> values)
        {
            var model = new EstablishmentForm();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            var isValid = false;

            if (string.Compare(name, typeof(EstablishmentForm.EstablishmentNameForm).FullName, StringComparison.Ordinal) == 0)
            {
                values.ForEach(t => model.Names.Add(new EstablishmentForm.EstablishmentNameForm { Text = t }));
                validationContext.MemberName = "AlternateNames";
                isValid = Validator.TryValidateProperty(model.Names, validationContext, validationResults);
            }
            var errorMessage = (isValid)
                ? null
                : validationResults[0].ErrorMessage;

            return Json(new { IsValid = isValid, ErrorMessage = errorMessage, }, JsonRequestBehavior.AllowGet);
            //return true;
        }

        #endregion
    }

    public static class ManagementFormsRouter
    {
        private static readonly string Area = MVC.Establishments.Name;
        private static readonly string Controller = MVC.Establishments.ManagementForms.Name;

        public class BrowseRoute : MvcRoute
        {
            public BrowseRoute()
                : base(RouteRegistration.CreateRouteHandler(WebConfig.IsDeployedToCloud))
            {
                Url = "establishments";
                DataTokens = RouteRegistration.CreateDataTokens(Area, typeof (EstablishmentsAreaRegistration));
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.ManagementForms.ActionNames.Browse,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class BrowseManageRoute : BrowseRoute
        {
            public BrowseManageRoute()
            {
                Url = "establishments/manage";
            }
        }

        public class BrowseManageBrowseRoute : BrowseRoute
        {
            public BrowseManageBrowseRoute()
            {
                Url = "establishments/manage/browse";
            }
        }

        public class BrowseManageBrowseDotHtmlRoute : BrowseRoute
        {
            public BrowseManageBrowseDotHtmlRoute()
            {
                Url = "establishments/manage/browse.html";
            }
        }

        public class FormEditRoute : MvcRoute
        {
            public FormEditRoute()
                : base(RouteRegistration.CreateRouteHandler(WebConfig.IsDeployedToCloud))
            {
                Url = "establishments/{entityId}/edit";
                DataTokens = RouteRegistration.CreateDataTokens(Area, typeof(EstablishmentsAreaRegistration));
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.ManagementForms.ActionNames.Form,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    entityId = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class FormAddRoute : FormEditRoute
        {
            public FormAddRoute()
            {
                Url = "establishments/new";
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PutRoute : MvcRoute
        {
            public PutRoute()
                : base(RouteRegistration.CreateRouteHandler(WebConfig.IsDeployedToCloud))
            {
                Url = "establishments/{entityId}";
                DataTokens = RouteRegistration.CreateDataTokens(Area, typeof(EstablishmentsAreaRegistration));
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.ManagementForms.ActionNames.Put,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("PUT", "POST"),
                    entityId = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class NewNameRoute : MvcRoute
        {
            public NewNameRoute()
                : base(RouteRegistration.CreateRouteHandler(WebConfig.IsDeployedToCloud))
            {
                Url = "establishments/new/name";
                DataTokens = RouteRegistration.CreateDataTokens(Area, typeof(EstablishmentsAreaRegistration));
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Establishments.ManagementForms.ActionNames.NewName,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
