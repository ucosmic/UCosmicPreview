using System;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    public class PersonInfoModel
    {
        public Guid EntityId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string DefaultEmail { get; set; }
    }
}