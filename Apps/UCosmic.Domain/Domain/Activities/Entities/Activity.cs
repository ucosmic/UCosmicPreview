using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Activities
{
    public class Activity : Entity, IAmNumbered
    {
        protected internal Activity()
        {
            EntityId = Guid.NewGuid();
            Mode = ActivityMode.Draft;
            DraftedValues = new ActivityValues();
            Values = new ActivityValues();
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            DraftedTags = new Collection<DraftedTag>();
            Tags = new Collection<ActivityTag>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public int PersonId { get; protected internal set; }
        public virtual Person Person { get; protected internal set; }
        public int Number { get; protected internal set; }

        public Guid EntityId { get; private set; }

        public string ModeText { get; protected set; }
        public ActivityMode Mode
        {
            get { return ModeText.AsEnum<ActivityMode>(); }
            protected internal set { ModeText = value.AsSentenceFragment(); }
        }

        public ActivityValues Values { get; private set; }
        public ActivityValues DraftedValues { get; private set; }

        public virtual ICollection<ActivityTag> Tags { get; protected set; }
        public virtual ICollection<DraftedTag> DraftedTags { get; protected set; }

        public DateTime CreatedOn { get; protected internal set; }
        public DateTime UpdatedOn { get; protected internal set; }
    }
}
