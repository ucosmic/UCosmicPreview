using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class TagMenuServices
    {
        public TagMenuServices(IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class TagMenuController : BaseController
    {
        private readonly TagMenuServices _services;

        public TagMenuController(TagMenuServices services)
        {
            _services = services;
        }

        [HttpPost]
        [OutputCache(Duration = 1800, VaryByParam = "*")]
        public virtual PartialViewResult Post(string term, string[] excludes)
        {
            const StringComparison caseInsensitive = StringComparison.OrdinalIgnoreCase;
            const StringMatchStrategy contains = StringMatchStrategy.Contains;
            const int maxResults = 30;

            // get places
            var places = _services.QueryProcessor.Execute(
                new FindPlacesWithNameQuery
                {
                    Term = term,
                    TermMatchStrategy = contains,
                    MaxResults = maxResults,
                    EagerLoad = new Expression<Func<Place, object>>[]
                    {
                        p => p.GeoPlanetPlace.Type,
                        p => p.Ancestors.Select(a => a.Ancestor),
                        p => p.Names.Select(n => n.TranslationToLanguage),
                    },
                }
            );

            // explain place matches
            var placeTags = Mapper.Map<TagMenuItem[]>(places);
            foreach (var placeTag in placeTags.Where(t => !t.Text.Contains(term, caseInsensitive)))
            {
                var matchingName = places.By(placeTag.DomainKey).Names.AsQueryable().FirstOrDefault
                    (QueryPlaceNames.SearchTermMatches(term, contains, caseInsensitive));
                placeTag.MatchingText = matchingName != null ? matchingName.Text : null;
            }

            // get establishments
            var establishments = _services.QueryProcessor.Execute(
                new FindEstablishmentsWithNameQuery
                {
                    Term = term,
                    TermMatchStrategy = contains,
                    MaxResults = maxResults,
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.Names.Select(n => n.TranslationToLanguage),
                        e => e.Ancestors.Select(a => a.Ancestor),
                        e => e.Location.Places,
                        e => e.Type,
                    },
                }
            );

            // explain establishment matches
            var establishmentTags = Mapper.Map<TagMenuItem[]>(establishments);
            foreach (var establishmentTag in establishmentTags.Where(t => !t.Text.Contains(term, caseInsensitive)))
            {
                var establishment = establishments.By(establishmentTag.DomainKey);
                var matchingName = establishment.Names.AsQueryable().FirstOrDefault
                    (QueryEstablishmentNames.SearchTermMatches(term, contains, caseInsensitive));
                establishmentTag.MatchingText = matchingName != null ? matchingName.Text : null;
            }

            // merge
            var tags = new List<TagMenuItem>();
            tags.AddRange(placeTags);
            tags.AddRange(establishmentTags);
            tags = tags.OrderBy(t => t.Text).Take(maxResults).ToList();

            // place exact match(es) at the top
            var exacts = tags.Where(t => t.Text.Equals(term, caseInsensitive)).ToArray();
            foreach (var exact in exacts)
            {
                tags.Remove(exact);
                tags.Insert(0, exact);
            }

            // remove all excluded tags
            if (excludes != null && excludes.Any())
                foreach (var exclude in excludes)
                    foreach (var excludeTag in tags.Where(t => t.Text.Equals(exclude)).ToArray())
                        tags.Remove(excludeTag);

            return PartialView(MVC.Activities.Shared.Views._tag_menu, tags);
        }
    }

    public static class TagMenuRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.TagMenu.Name;

        public class PostRoute : Route
        {
            public PostRoute()
                : base("activities/tags/menu", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.TagMenu.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
