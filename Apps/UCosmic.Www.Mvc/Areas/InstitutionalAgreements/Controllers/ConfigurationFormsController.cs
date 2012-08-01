using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;
using System.Web.Routing;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers
{
    public partial class ConfigurationFormsController : BaseController
    {
        #region Construction & DI

        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<CreateOrUpdateConfigurationCommand> _commandHandler;

        public ConfigurationFormsController(IProcessQueries queryProcessor
            , IHandleCommands<CreateOrUpdateConfigurationCommand> commandHandler
        )
        {
            _queryProcessor = queryProcessor;
            _commandHandler = commandHandler;
        }

        #endregion
        #region Add / Set Up

        [HttpGet]
        [ActionName("add")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual ActionResult Add()
        {
            var model = new InstitutionalAgreementConfigurationForm();

            // find the configuration for the currently signed in user's default affiliation
            var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));

            // if configuration exists, cannot add
            if (configuration != null)
            {
                model = Mapper.Map<InstitutionalAgreementConfigurationForm>(configuration);
            }
            else
            {
                // when configuration does not exist, get the default affiliation for the currently signed in user
                var person = GetConfigurationSupervisor();
                if (person == null)
                    return HttpNotFound();

                // allow the viewmodel to disclose the establishment official name
                model.ForEstablishmentOfficialName = person.DefaultAffiliation.Establishment.OfficialName;

                // add default options
                DefaultTypes.ForEach(option => model.AllowedTypeValues.Add(
                    new InstitutionalAgreementTypeValueForm { IsAdded = true, Text = option }));
                DefaultStatuses.ForEach(option => model.AllowedStatusValues.Add(
                    new InstitutionalAgreementStatusValueForm { IsAdded = true, Text = option }));
                DefaultContactTypes.ForEach(option => model.AllowedContactTypeValues.Add(
                    new InstitutionalAgreementContactTypeValueForm { IsAdded = true, Text = option }));

                // add a default empty allowed options
                AddEmptyAllowedOptions(model);
            }
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("add")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual ActionResult Add(InstitutionalAgreementConfigurationForm model)
        {
            // do nothing without a viewmodel
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    // make sure configuration does not already exist
                    var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));
                    if (configuration != null)
                    {
                        SetFeedbackMessage("Your configuration has already been set up.");
                        return RedirectToAction(MVC.InstitutionalAgreements.ConfigurationForms.Add());
                    }

                    // configuration must have a ForEstablishmentId, and all items should be added
                    var person = _queryProcessor.Execute(new GetMyPersonQuery(User));
                    var establishment = person.DefaultAffiliation.Establishment;
                    model.ForEstablishmentId = establishment.RevisionId;

                    var command = new CreateOrUpdateConfigurationCommand(User)
                    {
                        IsCustomTypeAllowed = model.IsCustomTypeAllowed,
                        IsCustomStatusAllowed = model.IsCustomStatusAllowed,
                        IsCustomContactTypeAllowed = model.IsCustomTypeAllowed,
                        AllowedTypeValues = model.AllowedTypeValues.Select(x => x.Text),
                        AllowedStatusValues = model.AllowedStatusValues.Select(x => x.Text),
                        AllowedContactTypeValues = model.AllowedContactTypeValues.Select(x => x.Text),
                    };
                    _commandHandler.Handle(command);

                    SetFeedbackMessage("Module configuration was set up successfully.");
                    return RedirectToAction(MVC.InstitutionalAgreements.ManagementForms.Browse());
                }

                // add a default empty allowed options
                AddEmptyAllowedOptions(model);
                return View(model);
            }
            return HttpNotFound();
        }

        #endregion
        #region Edit / Configure

        [ActionName("edit")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual ActionResult Edit()
        {
            // even if there is no configuration, return default affiliation establishment info to view
            var model = new InstitutionalAgreementConfigurationForm();

            // find a configuration for the currently signed in user's default affiliation establishment
            var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));

            // when configuration exists, return corresponding viewmodel
            if (configuration != null)
            {
                model = Mapper.Map<InstitutionalAgreementConfigurationForm>(configuration);
                AddEmptyAllowedOptions(model);
            }
            else
            {
                // when configuration does not exist, get the default affiliation for the currently signed in user
                var person = GetConfigurationSupervisor();
                if (person == null)
                    return HttpNotFound();

                // allow the viewmodel to disclose the establishment official name
                model.ForEstablishmentOfficialName = person.DefaultAffiliation.Establishment.OfficialName;
            }
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("edit")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual ActionResult Edit(InstitutionalAgreementConfigurationForm model)
        {
            if (model != null)
            {
                // look for current entity
                var existingEntity = _queryProcessor.Execute(
                    new GetInstitutionalAgreementConfigurationByGuidQuery(model.EntityId)
                        {
                            EagerLoad = new Expression<Func<InstitutionalAgreementConfiguration, object>>[]
                            {
                                c => c.ForEstablishment.Affiliates.Select(a => a.Person.User),
                                c => c.AllowedTypeValues,
                                c => c.AllowedStatusValues,
                                c => c.AllowedContactTypeValues,
                            },
                        });

                if (existingEntity == null)
                    return HttpNotFound();

                var compareConfiguration =
                    _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));

                if (existingEntity.RevisionId != compareConfiguration.RevisionId || existingEntity.EntityId != compareConfiguration.EntityId)
                    ModelState.AddModelError(string.Empty, string.Format(
                        "You are not authorized to configure the Institutional Agreements module for {0}.", existingEntity.ForEstablishment.OfficialName));

                if (ModelState.IsValid)
                {
                    var command = new CreateOrUpdateConfigurationCommand(User, model.RevisionId)
                    {
                        IsCustomTypeAllowed = model.IsCustomTypeAllowed,
                        IsCustomStatusAllowed = model.IsCustomStatusAllowed,
                        IsCustomContactTypeAllowed = model.IsCustomTypeAllowed,
                        AllowedTypeValues = model.AllowedTypeValues.Select(x => x.Text),
                        AllowedStatusValues = model.AllowedStatusValues.Select(x => x.Text),
                        AllowedContactTypeValues = model.AllowedContactTypeValues.Select(x => x.Text),
                    };
                    _commandHandler.Handle(command);
                    SetFeedbackMessage("Module configuration was saved successfully.");
                    return RedirectToAction(MVC.InstitutionalAgreements.ConfigurationForms.Edit());
                }

                AddEmptyAllowedOptions(model);
                return View(model);
            }
            return HttpNotFound();
        }

        #endregion
        #region Partials

        [ActionName("new-agreement-type-option")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual PartialViewResult NewAgreementType(Guid configurationId)
        {
            var configurationRevisionId = 0;
            var configuration = _queryProcessor.Execute(
                new GetInstitutionalAgreementConfigurationByGuidQuery(configurationId));
            if (configuration != null)
                configurationRevisionId = configuration.RevisionId;
            var model = new InstitutionalAgreementTypeValueForm
            {
                ConfigurationId = configurationRevisionId,
            };
            return PartialView(GetEditorTemplateViewName(Area, Name,
                Views.EditorTemplates.InstitutionalAgreementTypeValueForm), model);
        }

        [ActionName("new-agreement-status-option")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual PartialViewResult NewAgreementStatus(Guid configurationId)
        {
            var configurationRevisionId = 0;
            var configuration = _queryProcessor.Execute(
                new GetInstitutionalAgreementConfigurationByGuidQuery(configurationId));
            if (configuration != null)
                configurationRevisionId = configuration.RevisionId;
            var model = new InstitutionalAgreementStatusValueForm
            {
                ConfigurationId = configurationRevisionId,
            };
            return PartialView(GetEditorTemplateViewName(Area, Name,
                Views.EditorTemplates.InstitutionalAgreementStatusValueForm), model);
        }

        [ActionName("new-agreement-contact-type-option")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual PartialViewResult NewAgreementContactType(Guid configurationId)
        {
            var configurationRevisionId = 0;
            var configuration = _queryProcessor.Execute(
                new GetInstitutionalAgreementConfigurationByGuidQuery(configurationId));
            if (configuration != null)
                configurationRevisionId = configuration.RevisionId;
            var model = new InstitutionalAgreementContactTypeValueForm
            {
                ConfigurationId = configurationRevisionId,
            };
            return PartialView(GetEditorTemplateViewName(Area, Name,
                Views.EditorTemplates.InstitutionalAgreementContactTypeValueForm), model);
        }

        #endregion
        #region Json

        [ActionName("validate-duplicate-option")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual JsonResult ValidateDuplicateOption(string type, List<string> values)
        {
            var model = new InstitutionalAgreementConfigurationForm();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            var isValid = false;

            if (string.Compare(type, typeof(InstitutionalAgreementTypeValueForm).FullName, false, CultureInfo.InvariantCulture) == 0)
            {
                values.ForEach(t => model.AllowedTypeValues.Add(new InstitutionalAgreementTypeValueForm { Text = t }));
                validationContext.MemberName = "AllowedTypeValues";
                isValid = Validator.TryValidateProperty(model.AllowedTypeValues, validationContext, validationResults);
            }
            else if (string.Compare(type, typeof(InstitutionalAgreementStatusValueForm).FullName, false, CultureInfo.InvariantCulture) == 0)
            {
                values.ForEach(t => model.AllowedStatusValues.Add(new InstitutionalAgreementStatusValueForm { Text = t }));
                validationContext.MemberName = "AllowedStatusValues";
                isValid = Validator.TryValidateProperty(model.AllowedStatusValues, validationContext, validationResults);
            }
            else if (string.Compare(type, typeof(InstitutionalAgreementContactTypeValueForm).FullName, false, CultureInfo.InvariantCulture) == 0)
            {
                values.ForEach(t => model.AllowedContactTypeValues.Add(new InstitutionalAgreementContactTypeValueForm { Text = t }));
                validationContext.MemberName = "AllowedContactTypeValues";
                isValid = Validator.TryValidateProperty(model.AllowedContactTypeValues, validationContext, validationResults);
            }

            var errorMessage = (isValid)
                ? null
                : validationResults[0].ErrorMessage;

            return Json(new { IsValid = isValid, ErrorMessage = errorMessage, }, JsonRequestBehavior.AllowGet);
        }

        [ActionName("get-type-options")]
        [Authorize(Roles = RoleName.InstitutionalAgreementManagers)]
        public virtual JsonResult AgreementTypeOptions()
        {
            var comboBox = new ComboBoxOptions
            {
                strict = false,
                buttonTitle = "Show examples",
                source = DefaultTypes.Select(text => new AutoCompleteOption { label = text, value = text, }),
            };
            var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));
            if (configuration != null)
            {
                comboBox.source = configuration.AllowedTypeValues.OrderBy(o => o.Text).Select(o => o.Text)
                    .Select(t => new AutoCompleteOption { label = t, value = t, });
                comboBox.strict = !configuration.IsCustomTypeAllowed;
                if (comboBox.strict)
                    comboBox.buttonTitle = "Select one";
            }
            return Json(comboBox, JsonRequestBehavior.AllowGet);
        }

        [ActionName("get-status-options")]
        [Authorize(Roles = RoleName.InstitutionalAgreementManagers)]
        public virtual JsonResult AgreementStatusOptions()
        {
            var comboBox = new ComboBoxOptions
            {
                strict = false,
                buttonTitle = "Show examples",
                source = DefaultStatuses.Select(text => new AutoCompleteOption { label = text, value = text, }),
            };
            var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));
            if (configuration != null)
            {
                comboBox.source = configuration.AllowedStatusValues.OrderBy(o => o.Text).Select(o => o.Text)
                    .Select(t => new AutoCompleteOption { label = t, value = t, });
                comboBox.strict = !configuration.IsCustomStatusAllowed;
                if (comboBox.strict)
                    comboBox.buttonTitle = "Select one";
            }
            return Json(comboBox, JsonRequestBehavior.AllowGet);
        }

        [ActionName("get-contact-type-options")]
        [Authorize(Roles = RoleName.InstitutionalAgreementManagers)]
        public virtual JsonResult AgreementContactTypeOptions()
        {
            var comboBox = new ComboBoxOptions
            {
                strict = false,
                buttonTitle = "Show examples",
                source = DefaultContactTypes.Select(text => new AutoCompleteOption { label = text, value = text, }),
            };
            var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));
            if (configuration != null)
            {
                comboBox.source = configuration.AllowedContactTypeValues.OrderBy(o => o.Text).Select(o => o.Text)
                    .Select(t => new AutoCompleteOption { label = t, value = t, });
                comboBox.strict = !configuration.IsCustomContactTypeAllowed;
                if (comboBox.strict)
                    comboBox.buttonTitle = "Select one";
            }
            return Json(comboBox, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Private Methods

        private static readonly List<string> DefaultTypes = new List<string>
        {
            "Activity Agreement",
            "Institutional Collaboration Agreement",
            "Memorandum of Understanding",
        }.OrderBy(s => s).ToList();

        private static readonly List<string> DefaultStatuses = new List<string>
        {
            "Active",
            "Dead",
            "Inactive",
            "Unknown",
        }.OrderBy(s => s).ToList();

        private static readonly List<string> DefaultContactTypes = new List<string>
        {
            "Home Principal",
            "Home Secondary",
            "Partner Principal",
            "Partner Secondary",

        }.OrderBy(s => s).ToList();

        private static void AddEmptyAllowedOptions(InstitutionalAgreementConfigurationForm model)
        {
            model.AllowedTypeValues.Insert(0, new InstitutionalAgreementTypeValueForm { ConfigurationId = model.RevisionId });
            model.AllowedStatusValues.Insert(0, new InstitutionalAgreementStatusValueForm { ConfigurationId = model.RevisionId });
            model.AllowedContactTypeValues.Insert(0, new InstitutionalAgreementContactTypeValueForm { ConfigurationId = model.RevisionId });
        }

        private Person GetConfigurationSupervisor()
        {
            var person = _queryProcessor.Execute(new GetMyPersonQuery(User));

            // do not show anything to null users, unaffiliated users, non-member affiliations, or member-by-parent affiliations
            if (person == null || person.DefaultAffiliation == null || !person.DefaultAffiliation.Establishment.IsMember
                || person.DefaultAffiliation.Establishment.IsAncestorMember)
                return null;

            return person;
        }

        #endregion

    }

    public static class ConfigurationFormsRouter
    {
        private static readonly string Area = MVC.InstitutionalAgreements.Name;
        private static readonly string Controller = MVC.InstitutionalAgreements.ConfigurationForms.Name;

        public class AddRoute : MvcRoute
        {
            public AddRoute()
            {
                Url = "my/institutional-agreements/configure/set-up.html";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.Add,
                });
            }
        }

        public class EditRoute : MvcRoute
        {
            public EditRoute()
            {
                Url = "my/institutional-agreements/configure";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.Edit,
                });
            }
        }

        public class EditHtmlRoute : EditRoute
        {
            public EditHtmlRoute()
            {
                Url = "my/institutional-agreements/configure.html";
            }
        }

        public class NewAgreementTypeRoute : MvcRoute
        {
            public NewAgreementTypeRoute()
            {
                Url = "my/institutional-agreements/configure/{configurationId}/new-type-option.partial.html";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.NewAgreementType,
                });
            }
        }

        public class NewAgreementStatusRoute : MvcRoute
        {
            public NewAgreementStatusRoute()
            {
                Url = "my/institutional-agreements/configure/{configurationId}/new-status-option.partial.html";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.NewAgreementStatus,
                });
            }
        }

        public class NewAgreementContactTypeRoute : MvcRoute
        {
            public NewAgreementContactTypeRoute()
            {
                Url = "my/institutional-agreements/configure/{configurationId}/new-contact-type-option.partial.html";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.NewAgreementContactType,
                });
            }
        }

        public class AgreementTypeOptionsRoute : MvcRoute
        {
            public AgreementTypeOptionsRoute()
            {
                Url = "my/institutional-agreements/configure/get-type-options.json";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.AgreementTypeOptions,
                });
            }
        }

        public class AgreementStatusOptionsRoute : MvcRoute
        {
            public AgreementStatusOptionsRoute()
            {
                Url = "my/institutional-agreements/configure/get-status-options.json";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.AgreementStatusOptions,
                });
            }
        }

        public class AgreementContactTypeOptionsRoute : MvcRoute
        {
            public AgreementContactTypeOptionsRoute()
            {
                Url = "my/institutional-agreements/configure/get-contact-type-options.json";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.AgreementContactTypeOptions,
                });
            }
        }

        public class ValidateDuplicateOptionRoute : MvcRoute
        {
            public ValidateDuplicateOptionRoute()
            {
                Url = "my/institutional-agreements/configure/validate-duplicate-option.json";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.ValidateDuplicateOption,
                });
            }
        }
    }
}
