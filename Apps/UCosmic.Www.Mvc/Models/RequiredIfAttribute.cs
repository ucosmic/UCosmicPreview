using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
        }

        public string DependentProperty { get; private set; }

        public object TargetValue { get; private set; }

        private readonly RequiredAttribute _innerAttribute = new RequiredAttribute();

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        public override bool IsValid(object value)
        {
            return _innerAttribute.IsValid(value);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = string.Format(ErrorMessage, metadata.GetDisplayName()),
                ValidationType = "requiredif",
            };
            
            var viewContext = (ViewContext)context;
            var oldPrefix = viewContext.ViewData.TemplateInfo.HtmlFieldPrefix;
            viewContext.ViewData.TemplateInfo.HtmlFieldPrefix = oldPrefix.Replace(string.Format(".{0}", metadata.PropertyName), string.Empty);
            var depProp = viewContext
                .ViewData
                .TemplateInfo
                .GetFullHtmlFieldId(DependentProperty);
            viewContext.ViewData.TemplateInfo.HtmlFieldPrefix = oldPrefix;

            //var x = viewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", TargetValue);


            yield return rule;
        }

    }
}