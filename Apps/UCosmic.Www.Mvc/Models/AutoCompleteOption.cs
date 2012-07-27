 namespace UCosmic.Www.Mvc.Models
{
    public class AutoCompleteOption
    {
        public AutoCompleteOption()
        {
        }

        public AutoCompleteOption(string labelAndValue)
        {
            label = labelAndValue;
            value = labelAndValue;
        }

        public string label { get; set; }
        public string value { get; set; }
    }
}