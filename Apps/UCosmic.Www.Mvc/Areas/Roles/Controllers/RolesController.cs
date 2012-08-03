using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Roles.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Roles.Controllers
{
    public class RolesServices
    {
        public RolesServices(IProcessQueries queryProcessor
            , IHandleCommands<UpdateRoleCommand> updateHandler
        )
        {
            QueryProcessor = queryProcessor;
            UpdateHandler = updateHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateRoleCommand> UpdateHandler { get; private set; }
    }

    [Authorize(Roles = RoleName.AuthorizationAgent)]
    public partial class RolesController : BaseController
    {
        private readonly RolesServices _services;

        public RolesController(RolesServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("browse")]
        public virtual ActionResult Browse()
        {
            var entities = _services.QueryProcessor.Execute(new FindAllRolesQuery());
            var models = Mapper.Map<RoleSearchResult[]>(entities);
            return View(models);
        }

        [HttpGet]
        [ActionName("form")]
        [ReturnUrlReferrer(RolesRouter.BrowseRoute.UrlConstant)]
        public virtual ActionResult Form(string slug)
        {
            if (!string.IsNullOrWhiteSpace(slug))
            {
                var entity = _services.QueryProcessor.Execute(new GetRoleBySlugQuery(slug));
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
                    var command = new UpdateRoleCommand(User)
                    {
                        EntityId = model.EntityId,
                        Description = model.Description,
                        RevokedUserEntityIds = model.Grants.Where(g => g.IsDeleted).Select(g => g.User.EntityId),
                        GrantedUserEntityIds = model.Grants.Where(g => !g.IsDeleted).Select(g => g.User.EntityId),
                    };
                    _services.UpdateHandler.Handle(command);
                    SetFeedbackMessage(command.ChangeCount > 0
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
            var user = _services.QueryProcessor.Execute(
                new UserByEntityId
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

    public static class RolesRouter
    {
        private static readonly string Area = MVC.Roles.Name;
        private static readonly string Controller = MVC.Roles.Roles.Name;

        public class BrowseRoute : MvcRoute
        {
            public const string UrlConstant = "roles";

            public BrowseRoute()
            {
                Url = UrlConstant;
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Roles.Roles.ActionNames.Browse,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class FormRoute : MvcRoute
        {
            public FormRoute()
            {
                Url = "roles/{slug}/edit";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Roles.Roles.ActionNames.Form,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PutRoute : MvcRoute
        {
            public PutRoute()
            {
                Url = "roles/{slug}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Roles.Roles.ActionNames.Put,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                });
            }
        }

        public class AutoCompleteUserNameRoute : MvcRoute
        {
            public AutoCompleteUserNameRoute()
            {
                Url = "roles/manage/autocomplete-username";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Roles.Roles.ActionNames.AutoCompleteUserName,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class AddUserNameRoute : MvcRoute
        {
            public AddUserNameRoute()
            {
                Url = "roles/manage/add-role-member";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Roles.Roles.ActionNames.AddUserName,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}