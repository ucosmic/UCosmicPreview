using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Files;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers
{
    [Authorize(Roles = RoleName.InstitutionalAgreementManagers)]
    public partial class ManagementFormsController : BaseController
    {
        #region Construction & DI

        private readonly IProcessQueries _queryProcessor;
        private readonly InstitutionalAgreementFinder _agreements;
        //private readonly InstitutionalAgreementChanger _agreementChanger;
        //private readonly PersonFinder _people;
        private readonly EstablishmentFinder _establishments;
        private readonly FileFactory _fileFactory;
        private readonly IHandleCommands<CreateOrUpdateInstitutionalAgreementCommand> _commandHandler;

        public ManagementFormsController(IProcessQueries queryProcessor
            , IQueryEntities entityQueries
            , ICommandObjects objectCommander
            , IHandleCommands<CreateOrUpdateInstitutionalAgreementCommand> commandHandler
        )
        {
            _queryProcessor = queryProcessor;
            _agreements = new InstitutionalAgreementFinder(entityQueries);
            //_agreementChanger = new InstitutionalAgreementChanger(objectCommander, entityQueries);
            //_people = new PersonFinder(entityQueries);
            _establishments = new EstablishmentFinder(entityQueries);
            _fileFactory = new FileFactory(objectCommander, entityQueries);
            _commandHandler = commandHandler;
        }

        #endregion
        #region List

        [HttpGet]
        [ActionName("browse")]
        public virtual ActionResult Browse()
        {
            var agreementEntities = _agreements.FindMany(
                InstitutionalAgreementsWith.PrincipalContext(User)
                    .OrderBy(e => e.Title)
            );

            var locationEntities = agreementEntities.SelectMany(a => a.Participants).Where(a => !a.IsOwner)
                .Select(p => p.Establishment.Location).Distinct(new RevisableEntityEqualityComparer()).Cast<EstablishmentLocation>().ToList();
            var locationModels = Mapper.Map<ICollection<InstitutionalAgreementParticipantMarker>>(locationEntities);
            foreach (var locationModel in locationModels)
            {
                var agreementModels = agreementEntities.Where(a => a.Participants.Any(p => !p.IsOwner && p.Establishment.EntityId.Equals(locationModel.ForEstablishmentEntityId))).ToList();
                locationModel.Agreements = Mapper.Map<ICollection<InstitutionalAgreementMapSearchResult>>(agreementModels);
            }

            var tableModels = Mapper.Map<ICollection<InstitutionalAgreementSearchResult>>(agreementEntities);

            var countryCount = locationEntities.SelectMany(l => l.Places).Where(l => l.IsCountry).Distinct(new RevisableEntityEqualityComparer()).Count();

            var compositeModel = new InstitutionalAgreementBrowseView
            {
                ParticipantMarkers = locationModels,
                TableResults = tableModels,
                CountryCount = countryCount,
            };

            return View(compositeModel);
        }

        #endregion
        #region Post

        [HttpGet]
        [ActionName("post")]
        [ReturnUrlReferrer(ManagementFormsRouteMapper.Browse.Route)]
        public virtual ActionResult Post(Guid? entityId)
        {
            // do not process empty Guid
            if (entityId.HasValue && entityId.Value == Guid.Empty) return HttpNotFound();

            InstitutionalAgreementForm model;
            if (!entityId.HasValue)
            {
                // find person's default affiliation
                //var person = _people.FindOne(PersonBy.Principal(User));
                var person = _queryProcessor.Execute(new GetMyPersonQuery(User));
                if (person == null || person.DefaultAffiliation == null) return HttpNotFound();

                model = new InstitutionalAgreementForm
                {
                    EntityId = Guid.NewGuid(),
                };
                model.Participants.Add(new InstitutionalAgreementParticipantForm
                {
                    EstablishmentOfficialName = person.DefaultAffiliation.Establishment.OfficialName,
                    EstablishmentEntityId = person.DefaultAffiliation.Establishment.EntityId,
                    IsOwner = true,
                });
            }
            else
            {
                // find agreement
                var agreement = _agreements
                    .FindOne(By<InstitutionalAgreement>.EntityId(entityId.Value))
                    .OwnedBy(User);
                if (agreement == null) return HttpNotFound();

                model = Mapper.Map<InstitutionalAgreementForm>(agreement);
            }

            model.Umbrella.Options = GetUmbrellaOptions(model.RevisionId);
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("post")]
        public virtual ActionResult Post(InstitutionalAgreementForm model)
        {
            if (model != null)
            {
                // do not process empty Guid
                if (model.EntityId == Guid.Empty) return HttpNotFound();

                if (model.RevisionId != 0)
                {
                    // find agreement
                    var agreement = _agreements
                        .FindOne(By<InstitutionalAgreement>.EntityId(model.EntityId)
                    ).OwnedBy(User);
                    if (agreement == null) return HttpNotFound();
                }
                else
                {
                    // find person's default affiliation
                    //var person = _people.FindOne(PersonBy.Principal(User));
                    var person = _queryProcessor.Execute(new GetMyPersonQuery(User));
                    if (person == null || person.DefaultAffiliation == null) return HttpNotFound();
                }

                // always upload files
                UploadFiles(model);

                if (ModelState.IsValid)
                {
                    //var scalars = Mapper.Map<InstitutionalAgreement>(model);
                    //var changes = _agreementChanger.CreateOrModify(User, scalars, model.Umbrella.EntityId,
                    //    model.Participants.Where(m => m.IsDeleted).Select(m => m.EstablishmentEntityId),
                    //    model.Participants.Where(m => !m.IsDeleted).Select(m => m.EstablishmentEntityId),
                    //    model.Contacts.Where(m => m.IsDeleted).Select(m => m.EntityId),
                    //    Mapper.Map<IEnumerable<InstitutionalAgreementContact>>(model.Contacts.Where(m => !m.IsDeleted)),
                    //    model.Files.Where(m => m.IsDeleted).Select(m => m.EntityId),
                    //    model.Files.Where(m => !m.IsDeleted).Select(m => m.EntityId)
                    //);
                    // todo -- use automapper for this
                    var command = new CreateOrUpdateInstitutionalAgreementCommand(User)
                    {
                        RevisionId = model.RevisionId,
                        Title = model.Title,
                        IsTitleDerived = model.IsTitleDerived,
                        Type = model.Type,
                        Status = model.Status,
                        Description = model.Description,
                        IsAutoRenew = model.IsAutoRenew,
                        StartsOn = model.StartsOnValue,
                        ExpiresOn = model.ExpiresOnValue,
                        IsExpirationEstimated = model.IsExpirationEstimated,
                        //Visibility = model.Visibility,
                        UmbrellaEntityId = model.Umbrella.EntityId,
                        RemoveParticipantEstablishmentEntityIds = model.Participants.Where(m => m.IsDeleted).Select(m => m.EstablishmentEntityId),
                        AddParticipantEstablishmentEntityIds = model.Participants.Where(m => !m.IsDeleted).Select(m => m.EstablishmentEntityId),
                        RemoveContactEntityIds = model.Contacts.Where(m => m.IsDeleted).Select(m => m.EntityId),
                        AddContacts = Mapper.Map<IEnumerable<InstitutionalAgreementContact>>(model.Contacts.Where(m => !m.IsDeleted)),
                        RemoveFileEntityIds = model.Files.Where(m => m.IsDeleted).Select(m => m.EntityId),
                        AddFileEntityIds = model.Files.Where(m => !m.IsDeleted).Select(m => m.EntityId),
                    };
                    _commandHandler.Handle(command);
                    //SetFeedbackMessage(changes > 0
                    //    ? "Institutional agreement was saved successfully."
                    //    : "No changes were saved.");
                    SetFeedbackMessage(command.ChangeCount > 0
                        ? "Institutional agreement was saved successfully."
                        : "No changes were saved.");
                    return RedirectToAction(MVC.InstitutionalAgreements.PublicSearch.Info(model.EntityId));
                    //var hex = agreement.Files.ToList()[0].File.Content.ToHexString();
                }

                // set umbrella options
                model.Umbrella.Options = GetUmbrellaOptions(model.RevisionId);
                return View(model);
            }
            return HttpNotFound();
        }

        [NonAction]
        private void UploadFiles(InstitutionalAgreementForm form)
        {
            foreach (var formFile in form.Files)
            {
                if (formFile.IsDeleted && formFile.EntityId != Guid.Empty && formFile.RevisionId == 0)
                {
                    _fileFactory.Purge(formFile.EntityId, true);
                    formFile.EntityId = Guid.Empty;
                }
                else if (!formFile.IsDeleted && formFile.PostedFile != null && formFile.IsValidPostedFile)
                {
                    var looseFile = Mapper.Map<LooseFile>(formFile.PostedFile);
                    looseFile = _fileFactory.Create(looseFile.Content, looseFile.MimeType, looseFile.Name);
                    Mapper.Map(looseFile, formFile);
                }
            }
            var fileChooser = form.Files.SingleOrDefault(m => m.PostedFile == null && !m.IsDeleted && m.EntityId == Guid.Empty);
            if (fileChooser != null) form.Files.Remove(fileChooser);
        }

        #endregion
        #region File Uploading

        #endregion
        #region Partials

        [HttpGet]
        [ActionName("add-participant")]
        public virtual ActionResult AddParticipant(int agreementId, Guid establishmentId)
        {
            var establishment = _establishments
                .FindOne(By<Establishment>.EntityId(establishmentId)
                    .EagerLoad(e => e.Affiliates.Select(a => a.Person.User))
                    .EagerLoad(e => e.Ancestors.Select(h => h.Ancestor.Affiliates.Select(a => a.Person.User)))
                    .EagerLoad(e => e.Names.Select(n => n.TranslationToLanguage))
                );
            if (establishment != null)
            {
                Expression<Func<Affiliation, bool>> myDefaultAffiliation = affiliation =>
                    affiliation.IsDefault && affiliation.Person.User != null
                        && affiliation.Person.User.Name.Equals(User.Identity.Name,
                            StringComparison.OrdinalIgnoreCase);

                var model = new InstitutionalAgreementParticipantForm
                {
                    EstablishmentOfficialName = establishment.OfficialName,
                    EstablishmentTranslatedNameText = (establishment.TranslatedName != null)
                        ? establishment.TranslatedName.Text : null,
                    EstablishmentEntityId = establishmentId,
                    IsOwner = establishment.Affiliates.AsQueryable().Any(myDefaultAffiliation)
                        || establishment.Ancestors.Any(
                            ancestor => ancestor.Ancestor.Affiliates.AsQueryable().Any(myDefaultAffiliation)),
                };
                return PartialView(GetEditorTemplateViewName(Area, Name,
                    Views.EditorTemplates.InstitutionalAgreementParticipantForm), model);
            }
            return null;
        }

        [HttpGet]
        [ActionName("attach-file")]
        public virtual ActionResult AttachFile(int? agreementId)
        {
            var model = new InstitutionalAgreementFileForm();
            return PartialView(GetEditorTemplateViewName(Area, Name,
                Views.EditorTemplates.InstitutionalAgreementFileForm), model);
        }

        [HttpGet]
        [ActionName("add-contact")]
        public virtual ActionResult AddContact(int? agreementId)
        {
            var model = new InstitutionalAgreementContactForm();
            return PartialView(GetEditorTemplateViewName(Area, Name,
                Views.EditorTemplates.InstitutionalAgreementContactForm), model);
        }

        [HttpPost]
        [ActionName("add-contact")]
        public virtual ActionResult AddContact(InstitutionalAgreementContactForm model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    model.Person.DisplayName = model.Person.EntityId.HasValue
                        //? _people.FindOne(By<Person>.EntityId(model.Person.EntityId.Value)).DisplayName
                        ? _queryProcessor.Execute(new GetPersonByGuidQuery(model.Person.EntityId.Value)).DisplayName
                        //: PersonFactory.DeriveDisplayName(model.Person.LastName, model.Person.FirstName,
                        //    model.Person.MiddleName, model.Person.Salutation, model.Person.Suffix);
                        : _queryProcessor.Execute(
                            new GenerateDisplayNameQuery
                            {
                                FirstName = model.Person.FirstName,
                                LastName = model.Person.LastName,
                                MiddleName = model.Person.MiddleName,
                                Salutation = model.Person.Salutation,
                                Suffix = model.Person.Suffix,
                            });
                    return PartialView(MVC.InstitutionalAgreements.ManagementForms.Views.add_contact, model);
                }
                throw new NotSupportedException("Is client-side validation enabled?");
            }
            return HttpNotFound();
        }

        #endregion
        #region SelectLists

        [NonAction]
        private IEnumerable<SelectListItem> GetUmbrellaOptions(int agreementId)
        {
            var entities = _agreements.GetUmbrellaOptions(agreementId, User).OrderBy(e => e.Title);
            var models = Mapper.Map<IEnumerable<InstitutionalAgreementUmbrellaOption>>(entities);
            return new SelectList(models, "EntityId", "ShortTitle");
        }

        #endregion
        #region Json

        [HttpGet]
        [ActionName("derive-title")]
        public virtual ActionResult DeriveTitle(InstitutionalAgreementDeriveTitleInput model)
        {
            var agreement = Mapper.Map<InstitutionalAgreement>(model);
            if (model.ParticipantEstablishmentIds != null)
            {
                //var person = _people.FindOne(PersonBy.Principal(User));
                var person = _queryProcessor.Execute(new GetMyPersonQuery(User));
                var establishments = _establishments.FindMany(
                    With<Establishment>.EntityIds(model.ParticipantEstablishmentIds)
                        .EagerLoad(e => e.Affiliates.Select(a => a.Person))
                        .EagerLoad(e => e.Ancestors)
                );
                foreach (var establishment in establishments)
                {
                    agreement.Participants.Add(new InstitutionalAgreementParticipant
                    {
                        Establishment = establishment,
                        IsOwner = person.IsAffiliatedWith(establishment), // used to order the participant names
                    });
                }
            }
            model.Title = agreement.DeriveTitle();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [ActionName("autocomplete-establishment-names")]
        public virtual ActionResult AutoCompleteEstablishmentNames(string term, List<Guid> excludeEstablishmentIds)
        {
            // get matching official establishment names and their children
            var data = _establishments
                .FindMany(
                    EstablishmentsWith.AutoCompleteTerm(term, excludeEstablishmentIds, 50)
                        .OrderBy(e => new { e.Ancestors.Count })
                        .OrderBy(e => e.OfficialName)
                );

            // cast into autocomplete options
            var options = data.AsEnumerable().Select(e => new AutoCompleteOption
            {
                value = e.EntityId.ToString(),
                label = (e.TranslatedName != null) ? e.TranslatedName.Text : e.OfficialName,
            });

            // return json
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
