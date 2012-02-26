using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models.Roles;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    [Authorize(Roles = RoleName.AuthorizationAgent)]
    public partial class RolesController : BaseController
    {
        #region Construction & DI

        private readonly UserFinder _users;
        private readonly RoleFinder _roles;
        private readonly RoleChanger _roleChanger;


        public RolesController(IQueryEntities entityQueries, ICommandObjects objectCommander)
        {
            _users = new UserFinder(entityQueries);
            _roles = new RoleFinder(entityQueries);
            _roleChanger = new RoleChanger(objectCommander, entityQueries);
        }

        #endregion

        [HttpGet]
        [ActionName("browse")]
        public virtual ActionResult Browse()
        {
            var entities = _roles.FindMany(With<Role>.DefaultCriteria());
            var models = Mapper.Map<IEnumerable<RoleSearchResult>>(entities);
            return View(models);
        }

        [HttpGet]
        [ActionName("form")]
        public virtual ActionResult Form(string roleNameSlug, string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(roleNameSlug))
            {
                var entity = _roles.FindOne(RoleBy.Slug(roleNameSlug));
                if (entity != null)
                {
                    var model = Mapper.Map<RoleForm>(entity);
                    model.ReturnUrl = returnUrl;
                    return View(model);
                }
            }
            return HttpNotFound();
        }

        [HttpPut]
        [ActionName("put")]
        public virtual ActionResult Put(RoleForm model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    var changes = _roleChanger.Change(User, model.EntityId, model.Description, 
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
            var entities = _users.FindMany(
                UsersWith.AutoCompleteTerm(term, excludeUserEntityIds)
                    .OrderBy(e => e.UserName)
            );
            return Json(entities);
        }

        [HttpGet]
        [ActionName("add-username")]
        public virtual ActionResult AddUserName(Guid userEntityId)
        {
            var user = _users.FindOne(By<User>.EntityId(userEntityId));
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