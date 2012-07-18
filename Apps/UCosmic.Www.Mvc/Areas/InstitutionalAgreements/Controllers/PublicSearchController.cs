using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.PublicSearch;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers
{
    public partial class PublicSearchController : BaseController
    {
        private readonly InstitutionalAgreementFinder _agreements;
        private readonly EstablishmentFinder _establishments;
        //private readonly PlaceFinder _places;
        private readonly IProcessQueries _queryProcessor;

        public PublicSearchController(IQueryEntities entityQueries, IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            _agreements = new InstitutionalAgreementFinder(entityQueries);
            _establishments = new EstablishmentFinder(entityQueries);
            //_places = new PlaceFinder(entityQueries);
        }

        [NonAction]
        private string ConvertEstablishmentUrlFromMy(string establishmentUrl)
        {
            if (establishmentUrl.Equals("my", StringComparison.OrdinalIgnoreCase))
            {
                if (Request.IsAuthenticated)
                {
                    var establishment = _establishments.FindOne(EstablishmentBy.EmailDomain(User.Identity.Name));
                    if (establishment != null)
                        establishmentUrl = establishment.WebsiteUrl;
                }
                else
                {
                    var httpCookie = Request.Cookies["skin"];
                    if (httpCookie != null && !string.IsNullOrWhiteSpace(httpCookie.Value))
                        establishmentUrl = httpCookie.Value;
                }
            }
            return establishmentUrl;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetHierarchySelectList(Establishment establishment)
        {
            //var rootEstablishment = _establishments.FindOne(EstablishmentBy.WebsiteUrl(contextEstablishmentUrl)
            //);
            var rootEstablishment = establishment.Parent == null
                ? establishment
                : establishment.Ancestors.Single(e => e.Ancestor.Parent == null).Ancestor;
            //while (rootEstablishment.Parent != null)
            //    rootEstablishment = rootEstablishment.Parent;

            var childEstablishments = _agreements.FindMany(With<InstitutionalAgreement>.DefaultCriteria()
                    .EagerLoad(a => a.Participants.Select(p => p.Establishment.Ancestors)))
                .Where(a => a.Participants.Any(p => p.IsOwner &&
                    (p.Establishment.EntityId == rootEstablishment.EntityId ||
                    p.Establishment.Ancestors.Any(h => h.Ancestor.EntityId == rootEstablishment.EntityId))))
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
                var owners = _agreements.FindMany(With<InstitutionalAgreement>.DefaultCriteria())
                    .SelectMany(a => a.Participants).Where(p => p.IsOwner)
                    .Select(p => p.Establishment).Where(e => e.Ancestors.Count == 0 && !string.IsNullOrWhiteSpace(e.WebsiteUrl))
                    .Distinct(new RevisableEntityEqualityComparer()).Cast<Establishment>()
                    .OrderBy(e => e.OfficialName);
                var ownerModels = Mapper.Map<IEnumerable<EstablishmentInfo>>(owners);
                return View(Views.no_context, ownerModels);
            }

            // load the establishment by url as the context for the results
            var context = _establishments.FindOne(EstablishmentBy.WebsiteUrl(establishmentUrl)
                .EagerLoad(e => e.Names.Select(n => n.TranslationToLanguage))
                .EagerLoad(e => e.Location)
                .EagerLoad(e => e.Ancestors)
            );

            // determine whether or not the current user is an affiliate of the establishment
            var isAffiliate = context.HasDefaultAffiliate(User);

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

            #region old code

            //// load up all agreements owned by this establishment
            //var agreements = _agreements.FindMany(
            //    InstitutionalAgreementsWith.OwnedByEstablishmentUrl(establishmentUrl)
            //    //.EagerLoad(a => a.Participants.Select(p => p.Establishment.Location.Places.Select(l => l.Names))) this takes WAY too long, don't even think about it
            //    .EagerLoad(a => a.Participants.Select(p => p.Establishment.Location.Places))
            //    .EagerLoad(a => a.Participants.Select(p => p.Establishment.Names.Select(n => n.TranslationToLanguage)))
            //    .EagerLoad(a => a.Contacts)
            //);

            //// apply keyword to search
            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
            //    var isOwner = PredicateBuilder.True<InstitutionalAgreement>()
            //        .And(a => a.Participants.Any(p => p.IsOwner));
            //    var establishmentUrlMatches = PredicateBuilder.False<InstitutionalAgreement>()
            //        .Or(a => a.Participants.Any(p => establishmentUrl.Equals(p.Establishment.WebsiteUrl, StringComparison.OrdinalIgnoreCase)));
            //    establishmentUrlMatches = establishmentUrlMatches
            //        .Or(a => a.Participants.Any(p => p.Establishment.Ancestors.Any(h => establishmentUrl.Equals(h.Ancestor.WebsiteUrl, StringComparison.OrdinalIgnoreCase))));

            //    var ownedByEstablishmentUrl = PredicateBuilder.True<InstitutionalAgreement>()
            //        .And(a => a.Participants.Any(p => p.IsOwner && (establishmentUrl.Equals(p.Establishment.WebsiteUrl, StringComparison.OrdinalIgnoreCase)) ||
            //        p.Establishment.Ancestors.Any(h => establishmentUrl.Equals(h.Ancestor.WebsiteUrl, StringComparison.OrdinalIgnoreCase))));

            //    //var ownedByEstablishmentUrl = isOwner.And(establishmentUrlMatches.Expand());

            //    var placeNameMatchesKeyword = PredicateBuilder.False<InstitutionalAgreement>()
            //        .Or(a => a.Participants.Any(p => p.Establishment.Location.Places.Any(
            //            l => l.OfficialName.Contains(keyword)
            //    )));
            //    placeNameMatchesKeyword = placeNameMatchesKeyword.Or(a => a.Participants.Any(p => p.Establishment.Location.Places.Any(
            //        l => l.Names.Any(
            //            n =>
            //                n.Text.Contains(keyword) &&
            //                n.TranslationToLanguage != null &&
            //                n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            //            )
            //    )));
            //    placeNameMatchesKeyword = placeNameMatchesKeyword.Or(a => a.Participants.Any(p => p.Establishment.Location.Places.Any(
            //        l => l.Names.Any(
            //            n =>
            //                n.AsciiEquivalent != null &&
            //                n.AsciiEquivalent.Contains(keyword) &&
            //                n.TranslationToLanguage != null &&
            //                n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            //            )
            //    )));

            //    //var notOwnerAndPlaceNameMatchesKeyword = PredicateBuilder.True<InstitutionalAgreement>()
            //    //    .And(a => a.Participants.Any(p => !p.IsOwner)).And(placeNameMatchesKeyword.Expand());

            //    //ownedByEstablishmentUrl = ownedByEstablishmentUrl.And(notOwnerAndPlaceNameMatchesKeyword.Expand());

            //    ownedByEstablishmentUrl = ownedByEstablishmentUrl.And(a => a.Participants.Any(p => !p.IsOwner)).And(placeNameMatchesKeyword.Expand());

            //    using (var dbContext = new UCosmicContext(null))
            //    {
            //        //var byCountry2 = dbContext.InstitutionalAgreements.AsExpandable().Where(ownedByEstablishmentUrl.Expand());
            //        //var byCountry2List = byCountry2.ToList();

            //        var agreements2 = dbContext.InstitutionalAgreements
            //            .Include(a => a.Participants.Select(p => p.Establishment))
            //            .OwnedBy(1)
            //            .MatchingPlaceParticipantOrContact(keyword)
            //            .OrderByDescending(a => a.StartsOn);
            //        var agreements2List = agreements2.ToList();
            //    }


            //    var byCountry = agreements.Where(a =>
            //        a.Participants.Any(p =>
            //            !p.IsOwner &&
            //            (
            //                p.Establishment.Location.Places.Any(l =>
            //                    l.OfficialName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            //                ||
            //                p.Establishment.Location.Places.Any(l =>
            //                    l.Names.Any(n =>
            //                        n.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 &&
            //                        n.TranslationToLanguage != null &&
            //                        n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
            //                    )
            //                ||
            //                p.Establishment.Location.Places.Any(l =>
            //                    l.Names.Any(n =>
            //                        n.AsciiEquivalent != null &&
            //                        n.AsciiEquivalent.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 &&
            //                        n.TranslationToLanguage != null &&
            //                        n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
            //                    )
            //            )
            //        )
            //    );
            //    var byPartner = agreements.Where(a =>
            //        a.Participants.Any(p =>
            //            (
            //                p.Establishment.OfficialName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
            //                ||
            //                p.Establishment.Names.Any(n =>
            //                    n.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 &&
            //                    n.TranslationToLanguage != null &&
            //                    n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            //                )
            //                ||
            //                p.Establishment.Names.Any(n =>
            //                    n.AsciiEquivalent != null &&
            //                    n.AsciiEquivalent.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 &&
            //                    n.TranslationToLanguage != null &&
            //                    n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName
            //                )
            //            )
            //        )
            //    );
            //    var byContact = agreements.Where(a =>
            //        a.Contacts.Any(c =>
            //            (c.Person.FirstName != null && c.Person.FirstName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            //            ||
            //            (c.Person.LastName != null && c.Person.LastName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            //            ||
            //            (c.Person.DisplayName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            //        )
            //    );

            //    agreements = byCountry.Union(byPartner).Union(byContact)
            //        .Distinct(new RevisableEntityEqualityComparer())
            //        .Cast<InstitutionalAgreement>().ToList();
            //}
            //agreements = agreements.OrderByDescending(a => a.StartsOn).ToList();

            #endregion

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
            //var countryCount = partners.SelectMany(e => e.Location.Places).Where(l => l.IsCountry)
            //    .Distinct(new RevisableEntityEqualityComparer()).Count(); // degrades performance

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
            //foreach (var agreement in model.Agreements)
            //    agreement.IsOwnedByPrincipal = agreements.Single(a => a.EntityId == agreement.EntityId).IsOwnedBy(User) &&
            //        (isSupervisor || isManager);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                //var places = _places.FindMany(PlacesWith.AutoCompleteTerm(keyword));
                var places = _queryProcessor.Execute(
                    new FindPlacesWithNameQuery
                    {
                        Term = keyword,
                        TermMatchStrategy = StringMatchStrategy.StartsWith,
                    });
                if (places.Length == 1)
                    model.MapBoundingBox = places.Single().BoundingBox;
            }

            model.HierarchySelectList = GetHierarchySelectList(context).ToArray();
            model.EstablishmentUrl = model.ContextEstablishment.WebsiteUrl;
            return View(model);
        }

        [NullLayoutOnAjaxRequest]
        [OpenTopTab(TopTabName.InstitutionalAgreements)]
        [ReturnUrlReferrer("my/institutional-agreements")]
        public virtual ActionResult Info(Guid agreementId)
        {
            if (agreementId == Guid.Empty) return HttpNotFound();

            var agreement = _agreements.FindOne(By<InstitutionalAgreement>.EntityId(agreementId)
                //.EagerLoad(a => a.Participants.Select(p => p.Establishment.Affiliates.Select(f => f.Person.User)))
                //.EagerLoad(a => a.Participants.Select(p => p.Establishment.Location.Places.Select(l => l.GeoPlanetPlace.Type)))
                //.EagerLoad(a => a.Participants.Select(p => p.Establishment.Location.Places.Select(l => l.Ancestors)))
                //.EagerLoad(a => a.Participants.Select(p => p.Establishment.Names.Select(n => n.TranslationToLanguage)))
                //.EagerLoad(a => a.Participants.Select(p => p.Establishment.EmailDomains))
                //.EagerLoad(a => a.Contacts.Select(c => c.Person))
                //.EagerLoad(a => a.Files)
            );
            if (agreement == null) return HttpNotFound();

            var owners = agreement.Participants.Where(p => p.IsOwner).Select(p => p.Establishment).ToList();
            var isAffiliate = owners.Aggregate(false, (current, owner) => current || owner.HasDefaultAffiliate(User));
            var isManager = isAffiliate && (User.IsInRole(RoleName.InstitutionalAgreementManager)
                || User.IsInRole(RoleName.InstitutionalAgreementSupervisor));

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
                var agreement = _agreements.FindOne(InstitutionalAgreementBy.FileEntityId(fileId));

                // make sure user owns this agreement
                //var person = _people.FindOne(PersonBy.Principal(User));
                //if (agreement != null && agreement.Files != null && agreement.Files.Count > 0
                //    && agreement.Participants.Where(p => p.IsOwner).Any(p => person.IsAffiliatedWith(p.Establishment)))
                if (agreement != null && agreement.Files != null && agreement.Files.Count > 0
                    && agreement.IsOwnedBy(User))
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
                var agreement = _agreements.FindOne(InstitutionalAgreementBy.FileEntityId(fileId));

                // make sure user owns this agreement
                //var person = _people.FindOne(PersonBy.Principal(User));
                //if (agreement != null && agreement.Files != null && agreement.Files.Count > 0
                //    && agreement.Participants.Where(p => p.IsOwner).Any(p => person.IsAffiliatedWith(p.Establishment)))
                if (agreement != null && agreement.Files != null && agreement.Files.Count > 0
                    && agreement.IsOwnedBy(User))
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
            //var places = _places.FindMany(PlacesWith.AutoCompleteTerm(term, maxResults)
            //    .EagerLoad(p => p.Names.Select(n => n.TranslationToLanguage)));
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

            var establishments = _establishments.FindMany(EstablishmentsWith.AutoCompleteTerm(term, maxResults).OrderBy(e => e.OfficialName));
            var establishmentNames = establishments.Select(e => e.OfficialName);

            var contacts = _agreements.FindMany(InstitutionalAgreementsWith.OwnedByEstablishmentUrl(establishmentUrl))
                .SelectMany(a => a.Contacts).Where(c =>
                    (c.Person.FirstName != null && c.Person.FirstName.StartsWith(term, StringComparison.OrdinalIgnoreCase)) ||
                    (c.Person.LastName != null && c.Person.LastName.StartsWith(term, StringComparison.OrdinalIgnoreCase)) ||
                    c.Person.DisplayName.StartsWith(term, StringComparison.OrdinalIgnoreCase));
            var contactNames = contacts.Select(c => c.Person.DisplayName).OrderBy(s => s);

            var autoCompletes = placeNames.Union(establishmentNames).Union(contactNames)
                .Distinct().Take(maxResults);

            var options = autoCompletes.Select(p => new AutoCompleteOption(p)).ToList();
            return Json(options);
        }

        [HttpGet]
        [ChildActionOnly]
        public virtual ActionResult GetChildEstablishmentsWithAgreements(Guid parentId)
        {
            var owners = _agreements.FindMany(With<InstitutionalAgreement>.DefaultCriteria())
                .SelectMany(a => a.Participants).Where(p => p.IsOwner)
                .Select(p => p.Establishment).Where(e => e.Parent != null && e.Parent.EntityId == parentId && !string.IsNullOrWhiteSpace(e.WebsiteUrl))
                .Distinct(new RevisableEntityEqualityComparer()).Cast<Establishment>()
                .OrderBy(e => e.OfficialName);
            var ownerModels = Mapper.Map<IEnumerable<EstablishmentInfo>>(owners);
            return PartialView(Views._no_context, ownerModels);
        }
    }
}
