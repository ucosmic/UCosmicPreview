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
            //var configuration = _entities.ForCurrentUserDefaultAffiliation();
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
                    //var configuration = _entities.ForCurrentUserDefaultAffiliation();
                    var configuration = _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));
                    if (configuration != null)
                    {
                        SetFeedbackMessage("Your configuration has already been set up.");
                        return RedirectToAction(MVC.InstitutionalAgreements.ConfigurationForms.Add());
                    }

                    // configuration must have a ForEstablishmentId, and all items should be added
                    //var person = context.People.ForThreadPrincipal();
                    var person = _queryProcessor.Execute(new GetMyPersonQuery(User));
                    var establishment = person.DefaultAffiliation.Establishment;
                    model.ForEstablishmentId = establishment.RevisionId;

                    //var entity = Mapper.Map<InstitutionalAgreementConfiguration>(model);
                    //_entities.Create(entity);
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
                    //_unitOfWork.SaveChanges();

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

            // connect to database
            // find a configuration for the currently signed in user's default affiliation establishment
            //var configuration = _entities.ForCurrentUserDefaultAffiliation();
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
                //var existingEntity = _entities.Get<InstitutionalAgreementConfiguration>()
                //    .Include(c => c.ForEstablishment.Affiliates.Select(a => a.Person.User))
                //    .Include(c => c.AllowedTypeValues).Include(c => c.AllowedStatusValues).Include(c => c.AllowedContactTypeValues)
                //    .SingleOrDefault(c => c.EntityId == model.EntityId);
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
                    //_entities.ForCurrentUserDefaultAffiliation();
                    //context.ForCurrentUserDefaultAffiliation();
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

        //[HttpPost]
        //[ActionName("edit2")]
        //[Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        //public virtual ActionResult Edit2(InstitutionalAgreementConfigurationForm model)
        //{
        //    if (model != null)
        //    {
        //        var dep = true;
        //        using (var context = new UCosmicContext(null))
        //        {
        //            // look for current entity
        //            var existingEntity = context.InstitutionalAgreementConfigurations.Current()
        //                //var existingEntity = _entities.Get<InstitutionalAgreementConfiguration>()
        //                .Include(c => c.ForEstablishment.Affiliates.Select(a => a.Person.User))
        //                .Include(c => c.AllowedTypeValues).Include(c => c.AllowedStatusValues).Include(c => c.AllowedContactTypeValues)
        //                .SingleOrDefault(c => c.EntityId == model.EntityId);

        //            if (existingEntity == null)
        //                return HttpNotFound();

        //            var compareConfiguration =
        //                //_entities.ForCurrentUserDefaultAffiliation();
        //                //context.ForCurrentUserDefaultAffiliation();
        //                _queryProcessor.Execute(new GetMyInstitutionalAgreementConfigurationQuery(User));

        //            if (existingEntity.RevisionId != compareConfiguration.RevisionId || existingEntity.EntityId != compareConfiguration.EntityId)
        //                ModelState.AddModelError(string.Empty, string.Format(
        //                    "You are not authorized to configure the Institutional Agreements module for {0}.", existingEntity.ForEstablishment.OfficialName));

        //            if (ModelState.IsValid)
        //            {
        //                model.ForEstablishmentId = existingEntity.ForEstablishmentId;
        //                var revisedEntity = Mapper.Map<InstitutionalAgreementConfiguration>(model);
        //                var existingEntry = context.Entry(existingEntity);
        //                existingEntry.CurrentValues.SetValues(revisedEntity);
        //                existingEntity.CreatedOnUtc = DateTime.UtcNow;
        //                existingEntity.CreatedByPrincipal = User.Identity.Name;
        //                var originalEntity = existingEntry.OriginalValues.ToObject() as InstitutionalAgreementConfiguration;
        //                if (originalEntity != null)
        //                {
        //                    if (originalEntity.IsCustomTypeAllowed != revisedEntity.IsCustomTypeAllowed
        //                        || originalEntity.IsCustomStatusAllowed != revisedEntity.IsCustomStatusAllowed
        //                        || originalEntity.IsCustomContactTypeAllowed != revisedEntity.IsCustomContactTypeAllowed)
        //                    {
        //                        originalEntity.IsCurrent = false;
        //                        context.InstitutionalAgreementConfigurations.Add(originalEntity);
        //                    }
        //                }
        //                // agreement types
        //                if (revisedEntity.AllowedTypeValues != null)
        //                {
        //                    var newItems = revisedEntity.AllowedTypeValues.ToList();
        //                    var oldItems = existingEntity.AllowedTypeValues.ToList();
        //                    foreach (var oldItem in
        //                        from oldItem in oldItems
        //                        let matchedItem = newItems.SingleOrDefault(
        //                            p => p.Id == oldItem.Id)
        //                        where matchedItem == null
        //                        select oldItem)
        //                    {
        //                        context.Entry(oldItem).State = EntityState.Deleted;
        //                    }
        //                    foreach (var newItem in
        //                        from newItem in newItems
        //                        let matchedItem = oldItems.SingleOrDefault(
        //                            p => p.Id == newItem.Id)
        //                        where matchedItem == null
        //                        select newItem)
        //                    {
        //                        existingEntity.AllowedTypeValues.Add(newItem);
        //                    }
        //                    foreach (var oldItem in oldItems)
        //                    {
        //                        var matchedItem = newItems.SingleOrDefault(i => i.Id == oldItem.Id);
        //                        if (matchedItem != null)
        //                        {
        //                            context.Entry(oldItem).CurrentValues.SetValues(matchedItem);
        //                        }
        //                    }
        //                }

        //                //agreement status
        //                if (revisedEntity.AllowedStatusValues != null)
        //                {
        //                    var newStatusItems = revisedEntity.AllowedStatusValues.ToList();
        //                    var oldStatusItems = existingEntity.AllowedStatusValues.ToList();
        //                    foreach (var oldItem in
        //                        from oldItem in oldStatusItems
        //                        let matchedItem = newStatusItems.SingleOrDefault(
        //                            p => p.Id == oldItem.Id)
        //                        where matchedItem == null
        //                        select oldItem)
        //                    {
        //                        context.Entry(oldItem).State = EntityState.Deleted;
        //                    }
        //                    foreach (var newItem in
        //                        from newItem in newStatusItems
        //                        let matchedItem = oldStatusItems.SingleOrDefault(
        //                            p => p.Id == newItem.Id)
        //                        where matchedItem == null
        //                        select newItem)
        //                    {
        //                        existingEntity.AllowedStatusValues.Add(newItem);
        //                    }
        //                    foreach (var oldItem in oldStatusItems)
        //                    {
        //                        var matchedItem = newStatusItems.SingleOrDefault(i => i.Id == oldItem.Id);
        //                        if (matchedItem != null)
        //                        {
        //                            context.Entry(oldItem).CurrentValues.SetValues(matchedItem);
        //                        }
        //                    }
        //                }
        //                //agreement contact type
        //                if (revisedEntity.AllowedContactTypeValues != null)
        //                {
        //                    var newContactTypeItems = revisedEntity.AllowedContactTypeValues.ToList();
        //                    var oldContactTypeItems = existingEntity.AllowedContactTypeValues.ToList();
        //                    foreach (var oldItem in
        //                        from oldItem in oldContactTypeItems
        //                        let matchedItem = newContactTypeItems.SingleOrDefault(
        //                            p => p.Id == oldItem.Id)
        //                        where matchedItem == null
        //                        select oldItem)
        //                    {
        //                        context.Entry(oldItem).State = EntityState.Deleted;
        //                    }
        //                    foreach (var newItem in
        //                        from newItem in newContactTypeItems
        //                        let matchedItem = oldContactTypeItems.SingleOrDefault(
        //                            p => p.Id == newItem.Id)
        //                        where matchedItem == null
        //                        select newItem)
        //                    {
        //                        existingEntity.AllowedContactTypeValues.Add(newItem);
        //                    }
        //                    foreach (var oldItem in oldContactTypeItems)
        //                    {
        //                        var matchedItem = newContactTypeItems.SingleOrDefault(i => i.Id == oldItem.Id);
        //                        if (matchedItem != null)
        //                        {
        //                            context.Entry(oldItem).CurrentValues.SetValues(matchedItem);
        //                        }
        //                    }
        //                }

        //                existingEntry.State = EntityState.Modified;

        //                context.SaveChanges();
        //                SetFeedbackMessage("Module configuration was saved successfully.");
        //                return RedirectToAction(MVC.InstitutionalAgreements.ConfigurationForms.Edit());
        //            }

        //            AddEmptyAllowedOptions(model);
        //            return View(Views.edit, model);
        //        }
        //    }
        //    return HttpNotFound();
        //}

        #endregion
        #region Partials

        [ActionName("new-agreement-type-option")]
        [Authorize(Roles = RoleName.InstitutionalAgreementSupervisor)]
        public virtual PartialViewResult NewAgreementType(Guid configurationId)
        {
            var configurationRevisionId = 0;
            //var configuration = _entities2.Read<InstitutionalAgreementConfiguration>()
            //    .SingleOrDefault(x => x.EntityId == configurationId);
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
            //var configuration = _entities2.Read<InstitutionalAgreementConfiguration>()
            //    .SingleOrDefault(x => x.EntityId == configurationId);
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
            //var configuration = _entities2.Read<InstitutionalAgreementConfiguration>()
            //    .SingleOrDefault(x => x.EntityId == configurationId);
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
            //var configuration = _entities.ForCurrentUserDefaultAffiliation(true);
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
            //var configuration = _entities.ForCurrentUserDefaultAffiliation(true);
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
            //var configuration = _entities.ForCurrentUserDefaultAffiliation(true);
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
            //"Foreign Principal",
            //"Foreign Secondary",
            //"Local Principal",
            //"Local Secondary",
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
            //var person = context.People
            //    //.Including(p => p.User)
            //    //.Including(p => p.Affiliations.Select(a => a.Establishment.Parent))
            //    .ForThreadPrincipal();
            var person = _queryProcessor.Execute(new GetMyPersonQuery(User));

            // do not show anything to null users, unaffiliated users, non-member affiliations, or member-by-parent affiliations
            if (person == null || person.DefaultAffiliation == null || !person.DefaultAffiliation.Establishment.IsMember
                || person.DefaultAffiliation.Establishment.IsAncestorMember)
                return null;

            return person;
        }

        #endregion

    }

    //public static class InstitutionalAgreementConfigurationExtensions
    //{
    //    public static InstitutionalAgreementConfiguration ForCurrentUserDefaultAffiliation2(this IQueryEntities entities, bool checkAncestors = false)
    //    {
    //        // set up common predicate expressions
    //        Expression<Func<Affiliation, bool>> currentUserDefaultAffiliation = affiliation =>
    //            affiliation.Establishment.IsMember && affiliation.IsDefault && affiliation.Person.User != null
    //                && affiliation.Person.User.Name.Equals(Thread.CurrentPrincipal.Identity.Name);

    //        //var query = context.InstitutionalAgreementConfigurations.AsNoTracking().Current()
    //        var query = entities.Read<InstitutionalAgreementConfiguration>()
    //            //.Including(c => c.ForEstablishment.Affiliates.Select(a => a.Person.User))
    //            //.Including(c => c.AllowedTypeValues)
    //            //.Including(c => c.AllowedStatusValues)
    //            //.Including(c => c.AllowedContactTypeValues)
    //            .Where(c => c.ForEstablishment != null && c.ForEstablishment.IsMember);

    //        InstitutionalAgreementConfiguration configuration;
    //        if (!checkAncestors)
    //        {
    //            configuration = query.SingleOrDefault(c => c.ForEstablishment.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation)
    //                || c.ForEstablishment.Offspring.Any(offspring => offspring.Offspring.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation)));
    //        }
    //        else
    //        {
    //            configuration = query.SingleOrDefault(c => c.ForEstablishment.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation));
    //        }

    //        if (configuration != null)
    //        {
    //            configuration.AllowedTypeValues = configuration.AllowedTypeValues.OrderBy(o => o.Text).ToList();
    //            configuration.AllowedStatusValues = configuration.AllowedStatusValues.OrderBy(o => o.Text).ToList();
    //            configuration.AllowedContactTypeValues = configuration.AllowedContactTypeValues.OrderBy(o => o.Text).ToList();
    //        }

    //        return configuration;
    //    }

    //    //public static InstitutionalAgreementConfiguration ForCurrentUserDefaultAffiliation(this UCosmicContext context, bool checkAncestors = false)
    //    //{
    //    //    // set up common predicate expressions
    //    //    Expression<Func<Affiliation, bool>> currentUserDefaultAffiliation = affiliation =>
    //    //        affiliation.Establishment.IsMember && affiliation.IsDefault && affiliation.Person.User != null
    //    //            && affiliation.Person.User.Name.Equals(Thread.CurrentPrincipal.Identity.Name);

    //    //    var query = context.InstitutionalAgreementConfigurations.AsNoTracking().Current()
    //    //        //.Including(c => c.ForEstablishment.Affiliates.Select(a => a.Person.User))
    //    //        //.Including(c => c.AllowedTypeValues)
    //    //        //.Including(c => c.AllowedStatusValues)
    //    //        //.Including(c => c.AllowedContactTypeValues)
    //    //        .Where(c => c.ForEstablishment != null && c.ForEstablishment.IsMember);

    //    //    InstitutionalAgreementConfiguration configuration;
    //    //    if (!checkAncestors)
    //    //    {
    //    //        configuration = query.SingleOrDefault(c => c.ForEstablishment.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation)
    //    //            || c.ForEstablishment.Offspring.Any(offspring => offspring.Offspring.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation)));
    //    //    }
    //    //    else
    //    //    {
    //    //        configuration = query.SingleOrDefault(c => c.ForEstablishment.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation));
    //    //    }

    //    //    if (configuration != null)
    //    //    {
    //    //        configuration.AllowedTypeValues = configuration.AllowedTypeValues.OrderBy(o => o.Text).ToList();
    //    //        configuration.AllowedStatusValues = configuration.AllowedStatusValues.OrderBy(o => o.Text).ToList();
    //    //        configuration.AllowedContactTypeValues = configuration.AllowedContactTypeValues.OrderBy(o => o.Text).ToList();
    //    //    }

    //    //    return configuration;
    //    //}

    //    //public static InstitutionalAgreementConfiguration ForCurrentUserDefaultAffiliationV1(this UCosmicContext context, bool checkAncestors = false)
    //    //{
    //    //    // set up common predicate expressions
    //    //    Expression<Func<Affiliation, bool>> currentUserDefaultAffiliation = a =>
    //    //        a.Establishment.IsMember && a.IsDefault && a.Person.User != null
    //    //        && a.Person.User.UserName.Equals(Thread.CurrentPrincipal.Identity.Name);
    //    //    //Expression<Func<Establishment, bool>> currentUserDefaultEstablishment = e =>
    //    //    //    e.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation);

    //    //    var query = context.InstitutionalAgreementConfigurations.AsNoTracking().Current()
    //    //        .Including(c => c.ForEstablishment.Affiliates.Select(a => a.Person.User))
    //    //        .Including(c => c.AllowedTypeValues)
    //    //        .Including(c => c.AllowedStatusValues)
    //    //        .Including(c => c.AllowedContactTypeValues)
    //    //        .Where(c => c.ForEstablishment != null && c.ForEstablishment.IsMember);

    //    //    var configuration = query.SingleOrDefault(c => c.ForEstablishment.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation));
    //    //    if (configuration == null && checkAncestors)
    //    //    {
    //    //        // nested offspring collection is too slow
    //    //        //configuration = query.SingleOrDefault(c => c.ForEstablishment.Affiliates.AsQueryable().Any(currentUserDefaultAffiliation)
    //    //        //    || (c.ForEstablishment.Children.AsQueryable().Any(currentUserDefaultEstablishment))
    //    //        //    || (c.ForEstablishment.Children.Any(o2 => o2.Children
    //    //        //        .AsQueryable().Any(currentUserDefaultEstablishment)))
    //    //        //    || (c.ForEstablishment.Children.Any(o2 => o2.Children.Any(o3 => o3.Children
    //    //        //        .AsQueryable().Any(currentUserDefaultEstablishment))))
    //    //        //    || (c.ForEstablishment.Children.Any(o2 => o2.Children.Any(o3 => o3.Children.Any(o4 => o4.Children
    //    //        //        .AsQueryable().Any(currentUserDefaultEstablishment)))))
    //    //        //    || (c.ForEstablishment.Children.Any(o2 => o2.Children.Any(o3 => o3.Children.Any(o4 => o4.Children.Any(o5 =>
    //    //        //        o5.Children.AsQueryable().Any(currentUserDefaultEstablishment)))))));

    //    //        var person =
    //    //            context.People.AsNoTracking().Current().Including(p => p.User).Include(
    //    //                p => p.Affiliations.Select(a => a.Establishment.Parent.Parent.Parent.Parent.Parent))
    //    //                .SingleOrDefault(p => p.User != null && p.User.UserName.Equals(Thread.CurrentPrincipal.Identity.Name));
    //    //        if (person != null)
    //    //        {
    //    //            var establishment = person.DefaultAffiliation.Establishment.Parent;
    //    //            while (configuration == null && establishment != null && establishment.IsMember)
    //    //            {
    //    //                var revisionId = establishment.RevisionId;
    //    //                configuration = query.SingleOrDefault(c => c.ForEstablishmentId == revisionId);
    //    //                establishment = establishment.Parent;
    //    //            }
    //    //        }
    //    //    }

    //    //    if (configuration != null)
    //    //    {
    //    //        configuration.AllowedTypeValues = configuration.AllowedTypeValues.OrderBy(o => o.Text).ToList();
    //    //        configuration.AllowedStatusValues = configuration.AllowedStatusValues.OrderBy(o => o.Text).ToList();
    //    //        configuration.AllowedContactTypeValues = configuration.AllowedContactTypeValues.OrderBy(o => o.Text).ToList();
    //    //    }

    //    //    return configuration;
    //    //}

    //}
}
