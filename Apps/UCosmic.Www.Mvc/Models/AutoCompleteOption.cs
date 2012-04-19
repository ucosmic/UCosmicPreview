
namespace UCosmic.Www.Mvc.Models
{
    public class AutoCompleteOption
    {
        public AutoCompleteOption()
        {
        }

        // ReSharper disable UnusedMember.Global
        public AutoCompleteOption(string labelAndValue)
        // ReSharper restore UnusedMember.Global
        {
            label = labelAndValue;
            value = labelAndValue;
        }

        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public string label { get; set; }
        public string value { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore InconsistentNaming
    }
}