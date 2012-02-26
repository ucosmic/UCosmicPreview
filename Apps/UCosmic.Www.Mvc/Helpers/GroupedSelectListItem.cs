using System.Web.Mvc;

namespace UCosmic.Www.Mvc
{
    public class GroupedSelectListItem : SelectListItem
    {
        public string GroupKey { get; set; }
        public string GroupName { get; set; }
    }

}