
namespace UCosmic.Domain.Identity
{
    public class Preference : Entity
    {
        protected internal Preference()
        {
        }

        public int UserId { get; protected internal set; }
        public virtual User User { get; protected internal set; }
        public string CategoryText { get; protected set; }
        public PreferenceCategory Category
        {
            get { return CategoryText.AsEnum<PreferenceCategory>(); }
            protected internal set { CategoryText = value.AsSentenceFragment(); }
        }
        public string Key { get; protected internal set; }
        public string Value { get; protected internal set; }
    }
}
