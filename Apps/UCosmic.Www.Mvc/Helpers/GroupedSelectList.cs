using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace UCosmic.Www.Mvc
{
    public class GroupedSelectList : SelectList, IEnumerable<GroupedSelectListItem>
    {
        public GroupedSelectList(IEnumerable<GroupedSelectListItem> items, object selectedValue = null)
            : base(items, "Value", "Text", ToEnumerable(selectedValue))
        {
            SelectedValue = selectedValue;
            DataGroupKeyField = "GroupKey";
            DataGroupNameField = "GroupName";
        }

        public new object SelectedValue { get; private set; }

        public string DataGroupKeyField
        {
            get;
            private set;
        }

        public string DataGroupNameField
        {
            get;
            private set;
        }

        private static IEnumerable ToEnumerable(object selectedValue)
        {
            return (selectedValue != null) ? new[] { selectedValue } : null;
        }


        public new IEnumerator<GroupedSelectListItem> GetEnumerator()
        {
            return GetListItems().GetEnumerator();
        }

        private IEnumerable<GroupedSelectListItem> GetListItems()
        {
            return (!string.IsNullOrEmpty(DataValueField)) ?
                GetListItemsWithValueField() :
                GetListItemsWithoutValueField();
        }

        private IList<GroupedSelectListItem> GetListItemsWithValueField()
        {
            var selectedValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (SelectedValues != null)
            {
                selectedValues.UnionWith(from object value in SelectedValues select Convert.ToString(value, CultureInfo.CurrentCulture));
            }

            var listItems = from object item in Items
                            let value = Eval(item, DataValueField)
                            select new GroupedSelectListItem
                            {
                                Value = value,
                                Text = Eval(item, DataTextField),
                                Selected = selectedValues.Contains(value),
                                GroupKey = Eval(item, DataGroupKeyField),
                                GroupName = Eval(item, DataGroupNameField),
                            };
            return listItems.ToList();
        }

        private IList<GroupedSelectListItem> GetListItemsWithoutValueField()
        {
            var selectedValues = new HashSet<object>();
            if (SelectedValues != null)
            {
                selectedValues.UnionWith(SelectedValues.Cast<object>());
            }

            var listItems = from object item in Items
                            select new GroupedSelectListItem
                            {
                                Text = Eval(item, DataTextField),
                                Selected = selectedValues.Contains(item),
                                GroupKey = Eval(item, DataGroupKeyField),
                                GroupName = Eval(item, DataGroupNameField),
                            };
            return listItems.ToList();
        }

        private static string Eval(object container, string expression)
        {
            var value = container;
            if (!string.IsNullOrEmpty(expression))
            {
                value = DataBinder.Eval(container, expression);
            }
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        }
    }
}