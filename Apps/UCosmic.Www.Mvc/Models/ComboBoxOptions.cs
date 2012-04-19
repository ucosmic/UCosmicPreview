using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Models
{
    public class ComboBoxOptions
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public bool strict { get; set; }
        public IEnumerable<AutoCompleteOption> source { get; set; }
        public string buttonTitle { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore InconsistentNaming
    }
}