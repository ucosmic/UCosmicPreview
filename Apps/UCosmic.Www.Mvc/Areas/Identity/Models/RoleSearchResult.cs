using System;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class RoleSearchResult
    {
        public int RevisionId { get; set; }
        public Guid EntityId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}