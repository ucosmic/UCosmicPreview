using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NoDuplicateValuesAttribute : ValidationAttribute
    {
        public bool IgnoreCase { get; set; }

        private string _duplicateValue = "unknown";

        public NoDuplicateValuesAttribute()
        {
            IgnoreCase = true;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, _duplicateValue);
        }

        public override bool IsValid(object value)
        {
            // expect value to be an enumeration of strings
            var valueAsIHaveTexts = value as IEnumerable<IHaveText>;
            if (valueAsIHaveTexts != null)
            {
                // convert enumerable to list
                var iHaveTexts = valueAsIHaveTexts.ToList();

                // foreach converted into LINQ expression
                //foreach (var iHaveText in iHaveTexts)
                //{
                //    //var tempText = iHaveText.Text; // avoid modified closures
                //    var matches = iHaveTexts.Where(t => string.Compare(t.Text, iHaveText.Text, IgnoreCase) == 0);
                //    if (matches.Count() < 2) continue; // less than 2 matches indicate no duplication for this value
                //    _duplicateValue = iHaveText.Text; // otherwise, this value was duplicated
                //    return false; // duplicates indicate failed validation
                //}
                foreach (var iHaveText in from iHaveText in iHaveTexts // check each list item for duplicates
                                          // query the list for all items matching current value
                                          let matches = iHaveTexts.Where(t => string.Compare(t.Text, iHaveText.Text, IgnoreCase) == 0)
                                          where matches.Count() > 1 // if there is more than 1 match, there is a duplicate 
                                          select iHaveText) // select the duplicate into variable
                {
                    _duplicateValue = iHaveText.Text; // this text value was duplicated
                    return false; // duplicates indicate failed validation
                }

                return true;
            }

            // if value is null, it cannot possibly have duplicates
            if (value == null)
            {
                return true;
            }


            // value could not be converted to an expected type
            throw new NotImplementedException("The NoDuplicateValuesAttribute only operates on IEnumerable<IHaveText> values.");
        }

    }
}