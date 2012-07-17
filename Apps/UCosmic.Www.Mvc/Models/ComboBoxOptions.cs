using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Models
{
    public class ComboBoxOptions
    {
        public bool strict { get; set; }
        public IEnumerable<AutoCompleteOption> source { get; set; }
        public string buttonTitle { get; set; }
    }
}