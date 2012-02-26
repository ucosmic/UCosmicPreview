using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable IntroduceOptionalParameters.Global
    public static class GroupedSelectExtensions
    {
        // DropDownList

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name)
        {
            return GroupedDropDownList(htmlHelper, name, null /* selectList */, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name, string optionLabel)
        {
            return GroupedDropDownList(htmlHelper, name, null /* selectList */, optionLabel, null /* htmlAttributes */);
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList)
        {
            return GroupedDropDownList(htmlHelper, name, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList, object htmlAttributes)
        {
            return GroupedDropDownList(htmlHelper, name, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return GroupedDropDownList(htmlHelper, name, selectList, null /* optionLabel */, htmlAttributes);
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList, string optionLabel)
        {
            return GroupedDropDownList(htmlHelper, name, selectList, optionLabel, null /* htmlAttributes */);
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return GroupedDropDownList(htmlHelper, name, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString GroupedDropDownList(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, name, selectList, optionLabel, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList)
        {
            return GroupedDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, object htmlAttributes)
        {
            return GroupedDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return GroupedDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel)
        {
            return GroupedDropDownListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return GroupedDropDownListFor(htmlHelper, expression, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return DropDownListHelper(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
        }

        private static MvcHtmlString DropDownListHelper(HtmlHelper htmlHelper, string expression,
            IEnumerable<GroupedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, optionLabel, expression, selectList, false /* allowMultiple */, htmlAttributes);
        }

        // ListBox

        public static MvcHtmlString GroupedListBox(this HtmlHelper htmlHelper, string name)
        {
            return GroupedListBox(htmlHelper, name, null /* selectList */, null /* htmlAttributes */);
        }

        public static MvcHtmlString GroupedListBox(this HtmlHelper htmlHelper, string name, IEnumerable<GroupedSelectListItem> selectList)
        {
            return GroupedListBox(htmlHelper, name, selectList, null);
        }

        public static MvcHtmlString GroupedListBox(this HtmlHelper htmlHelper, string name, IEnumerable<GroupedSelectListItem> selectList, object htmlAttributes)
        {
            return GroupedListBox(htmlHelper, name, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString GroupedListBox(this HtmlHelper htmlHelper, string name,
            IEnumerable<GroupedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return ListBoxHelper(htmlHelper, name, selectList, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList)
        {
            return GroupedListBoxFor(htmlHelper, expression, selectList, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, object htmlAttributes)
        {
            return GroupedListBoxFor(htmlHelper, expression, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString GroupedListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IEnumerable<GroupedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return ListBoxHelper(htmlHelper,
                                 ExpressionHelper.GetExpressionText(expression),
                                 selectList,
                                 htmlAttributes);
        }

        private static MvcHtmlString ListBoxHelper(HtmlHelper htmlHelper, string name, IEnumerable<GroupedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, null /* optionLabel */, name, selectList, true /* allowMultiple */, htmlAttributes);
        }

        // Helper methods

        private static IEnumerable<GroupedSelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(name);
            }
            if (o == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        //MvcResources.HtmlHelper_MissingSelectData,
                        "There is no ViewData item of type '{1}' that has the key '{0}'.",
                        name,
                        "IEnumerable<GroupedSelectListItem>"));
            }
            var selectList = o as IEnumerable<GroupedSelectListItem>;
            if (selectList == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        //MvcResources.HtmlHelper_WrongSelectDataType,
                        "The ViewData item that has the key '{0}' is of type '{1}' but must be of type '{2}'.",
                        name,
                        o.GetType().FullName,
                        "IEnumerable<GroupedSelectListItem>"));
            }
            return selectList;
        }

        private static string ListItemToOption(GroupedSelectListItem item)
        {
            var builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, string optionLabel, string name,
            IEnumerable<GroupedSelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentException("Value cannot be null or empty.", "name");

            var usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectData(fullName);
                usedViewData = true;
            }

            //object defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(fullName, typeof(string[])) : htmlHelper.GetModelStateValue(fullName, typeof(string));
            var defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(fullName, typeof(string[])) : htmlHelper.GetModelStateValue(fullName, typeof(string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData-supplied value before using the parameter-supplied value.
            if (!usedViewData)
            {
                if (defaultValue == null)
                {
                    defaultValue = htmlHelper.ViewData.Eval(fullName);
                }
            }

            if (defaultValue != null)
            {
                var defaultValues = (allowMultiple) ? defaultValue as IEnumerable : new[] { defaultValue };
                var values = from object value in defaultValues select Convert.ToString(value, CultureInfo.CurrentCulture);
                var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
                var newSelectList = new List<GroupedSelectListItem>();

                // ReSharper disable PossibleMultipleEnumeration
                foreach (var item in selectList)
                {
                    item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                    newSelectList.Add(item);
                }
                // ReSharper restore PossibleMultipleEnumeration
                selectList = newSelectList;
            }

            // Convert each ListItem to an <option> tag
            var listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                listItemBuilder.AppendLine(ListItemToOption(new GroupedSelectListItem { Text = optionLabel, Value = string.Empty, Selected = false }));
            }

            // ReSharper disable PossibleMultipleEnumeration
            foreach (var group in selectList.GroupBy(i => i.GroupKey))
            {
                var groupBuilder = new TagBuilder("optgroup");
                groupBuilder.Attributes["value"] = group.Key;
                var groupName = selectList.Where(i => i.GroupKey == group.Key).Select(it => it.GroupName).FirstOrDefault();
                if (groupName != null)
                {
                    groupBuilder.Attributes["label"] = groupName;
                }
                listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.StartTag));
                foreach (var item in group)
                {
                    listItemBuilder.AppendLine(ListItemToOption(item));
                }
                listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.EndTag));
            }
            // ReSharper restore PossibleMultipleEnumeration


            var tagBuilder = new TagBuilder("select")
            {
                InnerHtml = listItemBuilder.ToString()
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", fullName, true /* replaceExisting */);
            tagBuilder.GenerateId(fullName);
            if (allowMultiple)
            {
                tagBuilder.MergeAttribute("multiple", "multiple");
            }

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name));

            //return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        private static object GetModelStateValue(this HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }
    }
    // ReSharper restore IntroduceOptionalParameters.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore MemberCanBePrivate.Global
}
