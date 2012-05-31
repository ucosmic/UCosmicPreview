using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

// TODO: shares code with RequiredIfClient attribute
namespace UCosmic.Www.Mvc.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RangeIfClientAttribute : ValidationAttribute, IClientValidatable
    {
        public RangeIfClientAttribute(int minimum, int maximum, string otherProperty, ComparisonType comparisonType, object otherComparisonValue)
        {
            Minimum = minimum;
            Maximum = maximum;
            OtherProperty = otherProperty;
            ComparisonType = comparisonType;
            OtherComparisonValue = otherComparisonValue;
        }

        private int Minimum { get; set; }

        private int Maximum { get; set; }

        private string OtherProperty { get; set; }

        private object OtherComparisonValue { get; set; }

        private ComparisonType ComparisonType { get; set; }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        public override bool IsValid(object value)
        {
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = string.Format(ErrorMessage, metadata.GetDisplayName()),
                ValidationType = "rangeif",
            };

            var viewContext = (ViewContext)context;
            var oldPrefix = viewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

            // only strip prefix when it exists
            viewContext.ViewData.TemplateInfo.HtmlFieldPrefix = string.Empty;
            var otherInputName = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(OtherProperty);
            var thisPropertyName = ".{0}".FormatWith(metadata.PropertyName);
            if (oldPrefix.EndsWith(thisPropertyName))
            {
                viewContext.ViewData.TemplateInfo.HtmlFieldPrefix = oldPrefix.Replace(thisPropertyName, string.Empty);
                otherInputName = viewContext
                    .ViewData
                    .TemplateInfo
                    .GetFullHtmlFieldName(OtherProperty);
            }
            viewContext.ViewData.TemplateInfo.HtmlFieldPrefix = oldPrefix;

            rule.ValidationParameters.Add("minimum", Minimum);
            rule.ValidationParameters.Add("maximum", Maximum);
            rule.ValidationParameters.Add("otherinputname", otherInputName);
            rule.ValidationParameters.Add("comparisontype", ComparisonType);
            rule.ValidationParameters.Add("othercomparisonvalue", OtherComparisonValue);


            yield return rule;
        }
    }
}