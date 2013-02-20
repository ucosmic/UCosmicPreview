using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.People;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.PublicSearch;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers
{
    public partial class PublicSearchController : BaseController
    {
        private readonly IProcessQueries _queryProcessor;

        public PublicSearchController(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }


        [NonAction]
        private string ConvertEstablishmentUrlFromMy(string establishmentUrl)
        {
            if (establishmentUrl.Equals("my", StringComparison.OrdinalIgnoreCase))
            {
                if (Request.IsAuthenticated)
                {
                    var establishment = _queryProcessor.Execute(new GetEstablishmentByEmailQuery(User.Identity.Name));
                    if (establishment != null)
                        establishmentUrl = establishment.WebsiteUrl;
                }
                else
                {
                    var skin = HttpContext.SkinCookie();
                    if (!string.IsNullOrWhiteSpace(skin))
                        establishmentUrl = skin;
                }
            }
            return establishmentUrl;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetHierarchySelectList(Establishment establishment)
        {
            var rootEstablishment = establishment.Parent == null
                ? establishment
                : establishment.Ancestors.Single(e => e.Ancestor.Parent == null).Ancestor;

            var childEstablishments =
                _queryProcessor.Execute(
                    new FindInstitutionalAgreementsOwnedByEstablishmentQuery(rootEstablishment.EntityId)
                    {
                        EagerLoad = new Expression<Func<InstitutionalAgreement, object>>[]
                        {
                            a => a.Participants.Select(p => p.Establishment.Ancestors),
                        }
                    }
                )
                .SelectMany(a => a.Participants.Where(p => p.IsOwner)).Select(p => p.Establishment)
                .Distinct(new RevisableEntityEqualityComparer()).Cast<Establishment>()
                .OrderBy(e => e.Ancestors.Count).ThenBy(e => e.OfficialName);

            var selectList = childEstablishments.Select(e => new SelectListItem
            {
                Text = e.OfficialName,
                Value = e.WebsiteUrl,
            });

            return selectList;
        }

        public virtual ActionResult ChangeOwner(string newEstablishmentUrl, string keyword)
        {
            return RedirectToAction(MVC.InstitutionalAgreements.PublicSearch.Index(newEstablishmentUrl, keyword));
        }

        [OpenTopTab(TopTabName.InstitutionalAgreements)]
        public virtual ActionResult Index(string establishmentUrl, string keyword = null)
        {
            // return 404 when there is no establishment url
            if (string.IsNullOrWhiteSpace(establishmentUrl)) return HttpNotFound();

            // redirect to canonical route when keyword is in the query string
            if (Request.QueryString.AllKeys.Contains("keyword", new CaseInsensitiveStringComparer()))
                return RedirectToRoute(new
                {
                    area = Area,
                    controller = Name,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.Index,
                    establishmentUrl,
                    keyword,
                });

            // try to derive the establishment url for current user
            establishmentUrl = ConvertEstablishmentUrlFromMy(establishmentUrl);

            // when request is not authenticated, present list of establishments to choose from
            if (establishmentUrl.Equals("my", StringComparison.OrdinalIgnoreCase))
            {
                var owners = _queryProcessor.Execute(
                    new FindEstablishmentsWithInstitutionalAgreementsQuery
                    {
                        OrderBy = new Dictionary<Expression<Func<Establishment, object>>, OrderByDirection>
                        {
                            { e => e.OfficialName, OrderByDirection.Ascending },
                        }
                    }
                );
                var ownerModels = Mapper.Map<IEnumerable<EstablishmentInfo>>(owners);
                return View(Views.no_context, ownerModels);
            }

            // load the establishment by url as the context for the results
            var context = _queryProcessor.Execute(new GetEstablishmentByUrlQuery(establishmentUrl)
            {
                EagerLoad = new Expression<Func<Establishment, object>>[]
                {
                    e => e.Names.Select(n => n.TranslationToLanguage),
                    e => e.Location,
                    e => e.Ancestors,
                }
            });

            // determine whether or not the current user is an affiliate of the establishment
            var isAffiliate = HasDefaultAffiliate(context, User);

            // determine whether or not the current user is an agreement supervisor or manager
            var isSupervisor = isAffiliate && User.IsInRole(RoleName.InstitutionalAgreementSupervisor);
            var isManager = isSupervisor || (isAffiliate && User.IsInRole(RoleName.InstitutionalAgreementManager));

            var agreements = _queryProcessor.Execute(
                new FindInstitutionalAgreementsByKeywordQuery
                {
                    EstablishmentId = context.RevisionId,
                    Keyword = keyword,
                    EagerLoad = new Expression<Func<InstitutionalAgreement, object>>[]
                    {
                        a => a.Participants.Select(p => p.Establishment.Names),
                        a => a.Participants.Select(p => p.Establishment.Location),
                    },
                    OrderBy = new Dictionary<Expression<Func<InstitutionalAgreement, object>>, OrderByDirection>
                    {
                        { a => a.StartsOn, OrderByDirection.Descending },
                    },
                }
            ).ToList();

            foreach (var agreement in agreements.ToArray())
            {
                // remove agreements that are not visible
                if (agreement.Visibility == InstitutionalAgreementVisibility.Protected && !isAffiliate)
                    agreements.Remove(agreement);
                else if (agreement.Visibility == InstitutionalAgreementVisibility.Private && !isSupervisor && !isManager)
                    agreements.Remove(agreement);
            }

            var partners = agreements.SelectMany(a => a.Participants).Where(a => !a.IsOwner)
                .Select(p => p.Establishment).Distinct(new RevisableEntityEqualityComparer())
                .Cast<Establishment>();

            var model = new SearchResults
            {
                ContextEstablishment = Mapper.Map<SearchResults.EstablishmentInfo>(context),
                Establishments = Mapper.Map<SearchResults.EstablishmentInfo[]>(partners),
                Agreements = Mapper.Map<SearchResults.AgreementInfo[]>(agreements),
                //CountryCount = countryCount,
                IsManager = isManager,
                IsSupervisor = isSupervisor,
                Keyword = keyword,
            };

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var places = _queryProcessor.Execute(
                    new FindPlacesWithNameQuery
                    {
                        Term = keyword,
                        TermMatchStrategy = StringMatchStrategy.StartsWith,
                    });
                if (places.Length == 1)
                    model.MapBoundingBox = Mapper.Map<BoundingBoxModel>(places.Single().BoundingBox);
            }

            model.HierarchySelectList = GetHierarchySelectList(context).ToArray();
            model.EstablishmentUrl = model.ContextEstablishment.WebsiteUrl;
            return View(model);
        }

        [NonAction]
        private static bool HasDefaultAffiliate(Establishment establishment, IPrincipal principal)
        {
            if (establishment == null) throw new ArgumentNullException("establishment");
            if (principal == null) throw new ArgumentNullException("principal");

            Func<Affiliation, bool> defaultAffiliation = a =>
                a.IsDefault && a.Person.User != null
                && a.Person.User.Name.Equals(principal.Identity.Name, StringComparison.OrdinalIgnoreCase);
            return establishment.Affiliates.Any(defaultAffiliation)
                || establishment.Ancestors.Any(n => n.Ancestor.Affiliates.Any(defaultAffiliation));
        }

        [NullLayoutOnAjaxRequest]
        [OpenTopTab(TopTabName.InstitutionalAgreements)]
        [ReturnUrlReferrer("my/institutional-agreements")]
        public virtual ActionResult Info(Guid agreementId)
        {
            if (agreementId == Guid.Empty) return HttpNotFound();

            var agreement = _queryProcessor.Execute(new GetInstitutionalAgreementByGuidQuery(agreementId));
            if (agreement == null) return HttpNotFound();

            var owners = agreement.Participants.Where(p => p.IsOwner).Select(p => p.Establishment).ToList();
            var isAffiliate = owners.Aggregate(false, (current, owner) => current || HasDefaultAffiliate(owner, User));
            var isManager = isAffiliate && User.IsInAnyRoles(RoleName.InstitutionalAgreementManagers);

            // hide from the public
            if (agreement.Visibility == InstitutionalAgreementVisibility.Protected && !isAffiliate)
            {
                return HttpNotFound();
            }
            if (agreement.Visibility == InstitutionalAgreementVisibility.Private && !isManager)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<AgreementInfo>(agreement);
            model.IsAffiliate = isAffiliate;
            model.IsManager = isManager;
            var distinctEmailDomains = model.Owners.SelectMany(o => o.EmailDomains.Select(e => e.Value)).Distinct().ToList();
            foreach (var owner in owners)
                foreach (var ancestor in owner.Ancestors.Select(n => n.Ancestor))
                    foreach (var domain in ancestor.EmailDomains)
                        if (!distinctEmailDomains.Contains(domain.Value))
                            distinctEmailDomains.Add(domain.Value);
            model.DistinctEmailDomains = distinctEmailDomains.OrderBy(s => s);

            return View(model);
        }

        #region File Attachments

        [HttpGet]
        [ActionName("display-file")]
        public virtual ActionResult DisplayFile(Guid fileId, string fileName)
        {
            if (fileId != Guid.Empty)
            {
                // find agreement
                var agreement = _queryProcessor.Execute(new GetMyInstitutionalAgreementByFileGuidQuery(User, fileId));

                // make sure user owns this agreement
                if (agreement != null && agreement.Files != null && agreement.Files.Count > 0)
                {
                    if (agreement.Visibility == InstitutionalAgreementVisibility.Private &&
                        !User.IsInRole(RoleName.InstitutionalAgreementManager) &&
                        !User.IsInRole(RoleName.InstitutionalAgreementSupervisor))
                    {
                        return HttpNotFound();
                    }
                    var file = agreement.Files.SingleOrDefault(f => f.EntityId.Equals(fileId));
                    if (file != null)
                    {
                        //Response.AddHeader("Content-Disposition", string.Format("inline; filename={0}", file.Name));
                        return File(file.Content, file.MimeType);
                    }
                }
            }
            return HttpNotFound();
        }

        [HttpGet]
        [ActionName("download-file")]
        public virtual ActionResult DownloadFile(Guid fileId, string fileName)
        {
            if (fileId != Guid.Empty)
            {
                // find agreement
                var agreement = _queryProcessor.Execute(new GetMyInstitutionalAgreementByFileGuidQuery(User, fileId));

                // make sure user owns this agreement
                if (agreement != null && agreement.Files != null && agreement.Files.Count > 0)
                {
                    if (agreement.Visibility == InstitutionalAgreementVisibility.Private &&
                        !User.IsInRole(RoleName.InstitutionalAgreementManager) &&
                        !User.IsInRole(RoleName.InstitutionalAgreementSupervisor))
                    {
                        return HttpNotFound();
                    }
                    var file = agreement.Files.SingleOrDefault(f => f.EntityId.Equals(fileId));
                    if (file != null)
                    {
                        return File(file.Content, file.MimeType, file.Name);
                    }
                }
            }
            return HttpNotFound();
        }

        #endregion

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult AutoCompleteKeyword(string establishmentUrl, string term)
        {
            const int maxResults = 15;
            var places = _queryProcessor.Execute(
                new FindPlacesWithNameQuery
                    {
                        Term = term,
                        MaxResults = maxResults,
                        TermMatchStrategy = StringMatchStrategy.StartsWith,
                        EagerLoad = new Expression<Func<Place, object>>[]
                        {
                            p => p.Names.Select(n => n.TranslationToLanguage)
                        }
                    });
            var placeNames = places.Select(p => (p.OfficialName.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                ? p.OfficialName
                : p.Names.First(n => n.TranslationToLanguage != null && n.TranslationToLanguage.TwoLetterIsoCode.Equals(
                        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)).Text
            ).OrderBy(s => s);

            var establishments = _queryProcessor.Execute(new FindEstablishmentsWithNameQuery
            {
                Term = term,
                TermMatchStrategy = StringMatchStrategy.Contains,
                MaxResults = maxResults,
                OrderBy = new Dictionary<Expression<Func<Establishment, object>>, OrderByDirection>
                {
                    { e => e.OfficialName, OrderByDirection.Ascending },
                },
            });
            var establishmentNames = establishments.Select(e => e.OfficialName);

            // todo: should be able to select contacts from encapsulated operation
            var contacts = _queryProcessor.Execute(new FindInstitutionalAgreementsOwnedByEstablishmentQuery(establishmentUrl))
                .SelectMany(a => a.Contacts).Where(c =>
                    (c.Person.FirstName != null && c.Person.FirstName.StartsWith(term, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Person.LastName != null && c.Person.LastName.StartsWith(term, StringComparison.OrdinalIgnoreCase)) ||
                    c.Person.DisplayName.StartsWith(term, StringComparison.OrdinalIgnoreCase));
            var contactNames = contacts.Select(c => c.Person.DisplayName).OrderBy(s => s);

            var autoCompletes = placeNames
                .Union(establishmentNames)
                .Union(contactNames)
                .Distinct()
                .Take(maxResults);

            var options = autoCompletes.Select(p => new AutoCompleteOption(p)).ToList();
            return Json(options);
        }

        [HttpGet]
        [ChildActionOnly]
        public virtual ActionResult GetChildEstablishmentsWithAgreements(Guid parentId)
        {
            var owners = _queryProcessor.Execute(
                new FindEstablishmentsWithInstitutionalAgreementsQuery(parentId)
                {
                    OrderBy = new Dictionary<Expression<Func<Establishment, object>>, OrderByDirection>
                    {
                        { e => e.OfficialName, OrderByDirection.Ascending },
                    }
                }
            );
            var ownerModels = Mapper.Map<IEnumerable<EstablishmentInfo>>(owners);
            return PartialView(Views._no_context, ownerModels);
        }
    }

    public static class PublicSearchRouter
    {
        private static readonly string Area = MVC.InstitutionalAgreements.Name;
        private static readonly string Controller = MVC.InstitutionalAgreements.PublicSearch.Name;

        public class IndexRoute : MvcRoute
        {
            public IndexRoute()
            {
                Order = 1; // otherwise this conflicts with institutional-agreements/configure
                Url = "{establishmentUrl}/institutional-agreements/{keyword}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.Index,
                    establishmentUrl = "my",
                    keyword = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class GetChildEstablishmentsWithAgreementsRoute : MvcRoute
        {
            public GetChildEstablishmentsWithAgreementsRoute()
            {
                Url = "{establishmentUrl}/institutional-agreements/under-parent/{parentId}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.GetChildEstablishmentsWithAgreements,
                    establishmentUrl = "my",
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    parentId = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class ChangeOwnerRoute : MvcRoute
        {
            public ChangeOwnerRoute()
            {
                Url = "institutional-agreements/change-owner";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.ChangeOwner,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class InfoRoute : MvcRoute
        {
            public InfoRoute()
            {
                Url = "institutional-agreements/{agreementId}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.Info,
                    establishmentUrl = "my",
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    agreementId = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class DisplayFileRoute : MvcRoute
        {
            public DisplayFileRoute()
            {
                Url = "institutional-agreements/files/{fileId}/display/{fileName}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.DisplayFile,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class DownloadFileRoute : MvcRoute
        {
            public DownloadFileRoute()
            {
                Url = "institutional-agreements/files/{fileId}/download/{fileName}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.DownloadFile,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class AutoCompleteKeywordRoute : MvcRoute
        {
            public AutoCompleteKeywordRoute()
            {
                Url = "institutional-agreements/autocomplete/search/keyword/{establishmentUrl}";
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.AutoCompleteKeyword,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
