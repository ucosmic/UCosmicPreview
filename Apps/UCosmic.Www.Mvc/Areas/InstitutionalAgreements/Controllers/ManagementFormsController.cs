using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Files;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers
{
    [Authorize(Roles = RoleName.InstitutionalAgreementManagers)]
    public partial class ManagementFormsController : BaseController
    {
        #region Construction & DI

        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<CreateOrUpdateInstitutionalAgreementCommand> _commandHandler;
        private readonly IHandleCommands<CreateLooseFileCommand> _createFileHandler;
        private readonly IHandleCommands<PurgeLooseFileCommand> _purgeFileHandler;

        public ManagementFormsController(IProcessQueries queryProcessor
            , IHandleCommands<CreateOrUpdateInstitutionalAgreementCommand> commandHandler
            , IHandleCommands<CreateLooseFileCommand> createFileHandler
            , IHandleCommands<PurgeLooseFileCommand> purgeFileHandler
        )
        {
            _queryProcessor = queryProcessor;
            _commandHandler = commandHandler;
            _createFileHandler = createFileHandler;
            _purgeFileHandler = purgeFileHandler;
        }

        #endregion
        #region List

        [HttpGet]
        [ActionName("browse")]
        public virtual ActionResult Browse()
        {
            var agreementEntities = _queryProcessor.Execute(
                new FindMyInstitutionalAgreementsQuery(User)
                {
                    OrderBy = new Dictionary<Expression<Func<InstitutionalAgreement, object>>, OrderByDirection>
                    {
                        { e => e.Title, OrderByDirection.Ascending },
                    },
                }
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
        [ReturnUrlReferrer(ManagementFormsRouter.BrowseRoute.BrowseUrl)]
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
                //var agreement = _agreements
                //    .FindOne(By<InstitutionalAgreement>.EntityId(entityId.Value))
                //    .OwnedBy(User);
                var agreement = _queryProcessor.Execute(
                    new GetMyInstitutionalAgreementByGuidQuery(User, entityId.Value)
                );
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
                    //var agreement = _agreements
                    //    .FindOne(By<InstitutionalAgreement>.EntityId(model.EntityId)
                    //).OwnedBy(User);
                    var agreement = _queryProcessor.Execute(
                        new GetMyInstitutionalAgreementByGuidQuery(User, model.EntityId));
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
                    var command = new CreateOrUpdateInstitutionalAgreementCommand(User);
                    Mapper.Map(model, command);
                    _commandHandler.Handle(command);
                    SetFeedbackMessage(command.ChangeCount > 0
                        ? "Institutional agreement was saved successfully."
                        : "No changes were saved.");
                    return RedirectToAction(MVC.InstitutionalAgreements.PublicSearch.Info(command.EntityId));
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
                    //_fileFactory.Purge(formFile.EntityId, true);
                    _purgeFileHandler.Handle(new PurgeLooseFileCommand(formFile.EntityId));
                    formFile.EntityId = Guid.Empty;
                }
                else if (!formFile.IsDeleted && formFile.PostedFile != null && formFile.IsValidPostedFile)
                {
                    //var looseFile = Mapper.Map<LooseFile>(formFile.PostedFile);
                    //looseFile = _fileFactory.Create(looseFile.Content, looseFile.MimeType, looseFile.Name);
                    //command = new CreateLooseFileCommand
                    //{
                    //    Content = looseFile.Content,
                    //    MimeType = looseFile.MimeType,
                    //    Name = looseFile.Name,
                    //};
                    var command = Mapper.Map<CreateLooseFileCommand>(formFile.PostedFile);
                    _createFileHandler.Handle(command);
                    var looseFile = command.CreatedLooseFile;
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
            //var establishment = _establishments
            //    .FindOne(By<Establishment>.EntityId(establishmentId)
            //        .EagerLoad(e => e.Affiliates.Select(a => a.Person.User))
            //        .EagerLoad(e => e.Ancestors.Select(h => h.Ancestor.Affiliates.Select(a => a.Person.User)))
            //        .EagerLoad(e => e.Names.Select(n => n.TranslationToLanguage))
            //    );
            var establishment = _queryProcessor.Execute(new EstablishmentByGuid(establishmentId)
            {
                EagerLoad = new Expression<Func<Establishment, object>>[]
                {
                    e => e.Affiliates.Select(a => a.Person.User),
                    e => e.Ancestors.Select(h => h.Ancestor.Affiliates.Select(a => a.Person.User)),
                    e => e.Names.Select(n => n.TranslationToLanguage),
                },
            });
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
            //var entities = _agreements.GetUmbrellaOptions(agreementId, User).OrderBy(e => e.Title);
            var entities = _queryProcessor.Execute(
                new FindInstitutionalAgreementUmbrellaCandidatesQuery(User)
                {
                    ForInstitutionalAgreementRevisionId = agreementId,
                    OrderBy = new Dictionary<Expression<Func<InstitutionalAgreement, object>>, OrderByDirection>
                    {
                        { a => a.Title, OrderByDirection.Ascending },
                    }
                });
            var models = Mapper.Map<IEnumerable<InstitutionalAgreementUmbrellaOption>>(entities);
            return new SelectList(models, "EntityId", "ShortTitle");
        }

        #endregion
        #region Json

        [HttpGet]
        [ActionName("derive-title")]
        public virtual ActionResult DeriveTitle(InstitutionalAgreementDeriveTitleInput model)
        {
            var query = new GenerateTitleQuery(User);
            Mapper.Map(model, query);
            model.Title = _queryProcessor.Execute(query);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //[ActionName("derive-title")]
        //public virtual ActionResult DeriveTitle0(InstitutionalAgreementDeriveTitleInput model)
        //{
        //    var agreement = Mapper.Map<InstitutionalAgreement>(model);
        //    if (model.ParticipantEstablishmentIds != null)
        //    {
        //        var person = _queryProcessor.Execute(new GetMyPersonQuery(User));
        //        var establishments = _queryProcessor.Execute(
        //            new FindEstablishmentsWithGuidsQuery(model.ParticipantEstablishmentIds)
        //            {
        //                EagerLoad = new Expression<Func<Establishment, object>>[]
        //                {
        //                    e => e.Affiliates.Select(a => a.Person),
        //                    e => e.Ancestors,
        //                },
        //            });
        //        foreach (var establishment in establishments)
        //        {
        //            agreement.Participants.Add(new InstitutionalAgreementParticipant
        //            {
        //                Establishment = establishment,
        //                IsOwner = person.IsAffiliatedWith(establishment), // used to order the participant names
        //            });
        //        }
        //    }
        //    model.Title = agreement.DeriveTitle();
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        [ValidateInput(false)]
        [ActionName("autocomplete-establishment-names")]
        public virtual ActionResult AutoCompleteEstablishmentNames(string term, List<Guid> excludeEstablishmentIds)
        {
            // get matching official establishment names and their children
            var data = _queryProcessor.Execute(new FindEstablishmentsWithNameQuery
            {
                Term = term,
                TermMatchStrategy = StringMatchStrategy.Contains,
                OrderBy = new Dictionary<Expression<Func<Establishment, object>>, OrderByDirection>
                {
                    { e => new { e.Ancestors.Count }, OrderByDirection.Ascending },
                    { e => e.OfficialName, OrderByDirection.Ascending },
                },
            });

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

    public static class ManagementFormsRouter
    {
        private static readonly string Area = MVC.InstitutionalAgreements.Name;
        private static readonly string Controller = MVC.InstitutionalAgreements.ManagementForms.Name;

        private static RouteValueDictionary GetDataTokens()
        {
            return new RouteValueDictionary(new
            {
                Namespaces = new[] { string.Format("{0}.*", typeof(InstitutionalAgreementsAreaRegistration).Namespace) },
                area = Area,
                UseNamespaceFallback = true,
            });
        }

        //public static void RegisterRoutes(AreaRegistrationContext context)
        //{
        //    RootActionRouter.RegisterRoutes(typeof(ManagementFormsRouteMapper), context, Area, Controller);
        //}

        public class BrowseRoute : Route
        {
            public BrowseRoute()
                : base(BrowseUrl, new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Browse,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }

            public const string BrowseUrl = "my/institutional-agreements/v1";
        }

        //public static class Browse
        //{
        //    public const string Route = "my/institutional-agreements/v1";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Browse;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}

        public class GetEditRoute : Route
        {
            public GetEditRoute()
                : base("my/institutional-agreements/v1/{entityId}/edit", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    entityId = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class GetNewRoute : Route
        {
            public GetNewRoute()
                : base("my/institutional-agreements/v1/new", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PostRoute : Route
        {
            public PostRoute()
                : base("my/institutional-agreements", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        //public static class Post
        //{
        //    public const string RouteForPost = "my/institutional-agreements";
        //    //public static readonly string[] RoutesForGet =
        //    //{
        //    //    "my/institutional-agreements/v1/{entityId}/edit",
        //    //    "my/institutional-agreements/v1/new",
        //    //};
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Post;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        //var constraintsForEdit = new
        //        //{
        //        //    entityId = new NonEmptyGuidRouteConstraint(),
        //        //    httpMethod = new HttpMethodConstraint("GET"),
        //        //};
        //        //context.MapRoute(null, RoutesForGet[0], defaults, constraintsForEdit);

        //        //var constraintsForNew = new
        //        //{
        //        //    httpMethod = new HttpMethodConstraint("GET")
        //        //};
        //        //context.MapRoute(null, RoutesForGet[1], defaults, constraintsForNew);

        //        var constraintsForPost = new { httpMethod = new HttpMethodConstraint("POST") };
        //        context.MapRoute(null, RouteForPost, defaults, constraintsForPost);
        //    }
        //}

        public class AddParticipantRoute : Route
        {
            public AddParticipantRoute()
                : base("my/institutional-agreements/manage/add-participant.partial.html", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AddParticipant,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        //public static class AddParticipant
        //{
        //    public const string Route = "my/institutional-agreements/manage/add-participant.partial.html";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AddParticipant;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new
        //        {
        //            httpMethod = new HttpMethodConstraint("GET"),
        //        };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}

        public class AttachFileRoute : Route
        {
            public AttachFileRoute()
                : base("my/institutional-agreements/manage/attach-file.partial.html", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AttachFile,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        //public static class AttachFile
        //{
        //    public const string Route = "my/institutional-agreements/manage/attach-file.partial.html";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AttachFile;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new
        //        {
        //            httpMethod = new HttpMethodConstraint("GET"),
        //        };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}

        public class AddContactRoute : Route
        {
            public AddContactRoute()
                : base("my/institutional-agreements/manage/add-contact-form.partial.html", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AddContact,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET", "POST"),
                });
            }
        }

        //public static class AddContact
        //{
        //    public const string Route = "my/institutional-agreements/manage/add-contact-form.partial.html";
        //    //public const string RouteForPost = "my/institutional-agreements/manage/add-contact-item.partial.html";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AddContact;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new
        //        {
        //            httpMethod = new HttpMethodConstraint("GET", "POST"),
        //        };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}

        public class DeriveTitleRoute : Route
        {
            public DeriveTitleRoute()
                : base("my/institutional-agreements/derive-title.json", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.DeriveTitle,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        //public static class DeriveTitle
        //{
        //    public const string Route = "my/institutional-agreements/derive-title.json";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.DeriveTitle;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new
        //        {
        //            httpMethod = new HttpMethodConstraint("GET"),
        //        };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}

        public class AutoCompleteEstablishmentNamesRoute : Route
        {
            public AutoCompleteEstablishmentNamesRoute()
                : base("institutional-agreements/autocomplete/official-name.json", new MvcRouteHandler())
            {
                DataTokens = GetDataTokens();
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AutoCompleteEstablishmentNames,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        //public static class AutoCompleteEstablishmentNames
        //{
        //    public const string Route = "institutional-agreements/autocomplete/official-name.json";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AutoCompleteEstablishmentNames;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new
        //        {
        //            httpMethod = new HttpMethodConstraint("GET"),
        //        };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}
    }
}
