
namespace UCosmic.Domain.People
{
    public class ChangeEmailSpellingCommand
    {
        public string UserName { get; set; }
        public int Number { get; set; }
        public string NewValue { get; set; }
        public bool ChangedState { get; internal set; }
    }
}
