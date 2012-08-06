using System;

namespace UCosmic.Domain.Activities
{
    public class ActivityValues
    {
        protected internal ActivityValues()
        {
        }

        public string Title { get; protected internal set; }
        public string Content { get; protected internal set; }
        public DateTime? StartsOn { get; protected internal set; }
        public DateTime? EndsOn { get; protected internal set; }
    }
}
