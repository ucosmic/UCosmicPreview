using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    [Authorize(Roles = RoleName.AuthorizationAgent)]
    public partial class RolesController : BaseController
    {
        #region Construction & DI

        private readonly RolesServices _services;

        public RolesController(RolesServices services)
        {
            _services = services;
        }

        #endregion

        [HttpGet]
        [ActionName("browse")]
        public virtual ActionResult Browse()
        {
            var entities = _services.Roles.Get();
            var models = Mapper.Map<RoleSearchResult[]>(entities);
            return View(models);
        }

        [HttpGet]
        [ActionName("form")]
        [ReturnUrlReferrer(RolesRouter.Browse.Route)]
        public virtual ActionResult Form(string slug)
        {
            if (!string.IsNullOrWhiteSpace(slug))
            {
                var entity = _services.Roles.GetBySlug(slug);
                if (entity != null)
                {
                    var model = Mapper.Map<RoleForm>(entity);
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        [HttpPut]
        [UnitOfWork]
        [ActionName("put")]
        public virtual ActionResult Put(RoleForm model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    var changes = _services.Roles.Update(User, model.EntityId, model.Description,
                        model.Grants.Where(g => g.IsDeleted).Select(g => g.User.EntityId),
                        model.Grants.Where(g => !g.IsDeleted).Select(g => g.User.EntityId)
                    );
                    SetFeedbackMessage(changes > 0
                        ? "Role has been successfully saved."
                        : "No changes were made.");
                    return Redirect(model.ReturnUrl);
                }
                return View(Views.form, model);
            }
            return HttpNotFound();
        }

        #region AutoComplete UserName

        [HttpPost]
        [ActionName("autocomplete-username")]
        [AutoMapper(typeof(IEnumerable<AutoCompleteOption>))]
        public virtual JsonResult AutoCompleteUserName(string term, List<Guid> excludeUserEntityIds)
        {
            var entities = _services.QueryProcessor.Execute(
                new AutoCompleteUsersByNameQuery
                {
                    Term = term,
                    ExcludeEntityIds = excludeUserEntityIds,
                    OrderBy = new Dictionary<Expression<Func<User, object>>, OrderByDirection>
                    {
                        { u => u.Name, OrderByDirection.Ascending },
                    },
                }
            );
            return Json(entities);
        }

        [HttpGet]
        [ActionName("add-username")]
        public virtual ActionResult AddUserName(Guid userEntityId)
        {
            //var user = _services.Users.Get(userEntityId);
            var user = _services.QueryProcessor.Execute(
                new GetUserByEntityIdQuery
                {
                    EntityId = userEntityId,
                }
            );
            if (user != null)
            {
                var model = Mapper.Map<RoleForm.RoleGrantForm>(user);
                return PartialView(GetEditorTemplateViewName(Area, Name,
                    Views.EditorTemplates.RoleGrantForm), model);
            }
            return HttpNotFound();
        }

        #endregion

    }
}