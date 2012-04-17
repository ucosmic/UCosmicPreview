
namespace UCosmic.Domain.People
{
    public class ChangeEmailAddressSpellingCommand
    {
        public string UserName { get; set; }
        public int Number { get; set; }
        public string NewValue { get; set; }
        public bool ChangedState { get; internal set; }
    }
}
