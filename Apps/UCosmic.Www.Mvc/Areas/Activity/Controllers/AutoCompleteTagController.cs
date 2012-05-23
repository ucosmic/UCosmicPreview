using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.Places;
using UCosmic.Www.Mvc.Areas.Activity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activity.Controllers
{
    public class AutoCompleteTagServices
    {
        public AutoCompleteTagServices(IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; set; }
    }

    public partial class AutoCompleteTagController : BaseController
    {
        private readonly AutoCompleteTagServices _services;

        public AutoCompleteTagController(AutoCompleteTagServices services)
        {
            _services = services;
        }

        [HttpPost]
        public virtual ActionResult Post(string term, string[] excludes)
        {
            const StringComparison caseInsensitive = StringComparison.OrdinalIgnoreCase;
            const StringMatchStrategy contains = StringMatchStrategy.Contains;
            const int maxResults = 12;

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
            var placeTags = Mapper.Map<AutoCompleteTag[]>(places);
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
            var establishmentTags = Mapper.Map<AutoCompleteTag[]>(establishments);
            foreach (var establishmentTag in establishmentTags.Where(t => !t.TaggedText.Contains(term, caseInsensitive)))
            {
                var establishment = establishments.ById(establishmentTag.RevisionId);
                var matchingName = establishment.Names.AsQueryable().FirstOrDefault
                    (QueryEstablishmentNames.SearchTermMatches(term, contains, caseInsensitive));
                establishmentTag.MatchingText = matchingName != null ? matchingName.Text : null;
            }

            // merge
            var tags = new List<AutoCompleteTag>();
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
            {
                foreach (var exclude in excludes)
                {
                    var excludeTags = tags.Where(t => t.TaggedText.Equals(exclude)).ToArray();
                    foreach (var excludeTag in excludeTags)
                    {
                        tags.Remove(excludeTag);
                    }
                }
            }

            return PartialView(MVC.Activity.Shared.Views._auto_complete_tags, tags);
        }
    }
}
