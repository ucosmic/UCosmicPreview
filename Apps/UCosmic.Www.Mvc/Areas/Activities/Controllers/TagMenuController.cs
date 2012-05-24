using System;
using System.Collections.Generic;
using System.Linq;
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
        public virtual PartialViewResult Post(string term, string[] excludes)
        {
            const StringComparison caseInsensitive = StringComparison.OrdinalIgnoreCase;
            const StringMatchStrategy contains = StringMatchStrategy.Contains;
            const int maxResults = 50;

            // get places
            var places = _services.QueryProcessor.Execute(
                new FindPlacesWithNameQuery
                {
                    Term = term,
                    TermMatchStrategy = contains,
                    MaxResults = maxResults,
                }
            );

            // explain place matches
            var placeTags = Mapper.Map<TagMenuItem[]>(places);
            foreach (var placeTag in placeTags.Where(t => !t.TaggedText.Contains(term, caseInsensitive)))
            {
                var matchingName = places.ById(placeTag.RevisionId).Names.AsQueryable().FirstOrDefault
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
                }
            );

            // explain establishment matches
            var establishmentTags = Mapper.Map<TagMenuItem[]>(establishments);
            foreach (var establishmentTag in establishmentTags.Where(t => !t.TaggedText.Contains(term, caseInsensitive)))
            {
                var establishment = establishments.ById(establishmentTag.RevisionId);
                var matchingName = establishment.Names.AsQueryable().FirstOrDefault
                    (QueryEstablishmentNames.SearchTermMatches(term, contains, caseInsensitive));
                establishmentTag.MatchingText = matchingName != null ? matchingName.Text : null;
            }

            // merge
            var tags = new List<TagMenuItem>();
            tags.AddRange(placeTags);
            tags.AddRange(establishmentTags);
            tags = tags.OrderBy(t => t.TaggedText).Take(maxResults).ToList();

            // place exact match(es) at the top
            var exacts = tags.Where(t => t.TaggedText.Equals(term, caseInsensitive)).ToArray();
            foreach (var exact in exacts)
            {
                tags.Remove(exact);
                tags.Insert(0, exact);
            }

            // remove all excluded tags
            if (excludes != null && excludes.Any())
                foreach (var exclude in excludes)
                    foreach (var excludeTag in tags.Where(t => t.TaggedText.Equals(exclude)).ToArray())
                        tags.Remove(excludeTag);

            return PartialView(MVC.Activities.Shared.Views._tag_menu, tags);
        }
    }

    public static class TagMenuRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.TagMenu.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(TagMenuRouter), context, Area, Controller);
            TagMenuItemProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class Post
        {
            public const string Route = "activities/tags/menu";
            private static readonly string Action = MVC.Activities.TagMenu.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
