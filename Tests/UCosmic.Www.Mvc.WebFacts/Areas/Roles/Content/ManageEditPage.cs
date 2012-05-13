using System.Collections.Generic;
using OpenQA.Selenium;
using UCosmic.Www.Mvc.Areas.Roles.Models;

namespace UCosmic.Www.Mvc.Areas.Roles
{
    public class ManageEditPage : Page
    {
        public ManageEditPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Role Edit"; } }
        public override string Path
        {
            get
            {
                const string slug = "role-name";
                return MVC.Roles.Roles.Form(slug).AsPath()
                    .Replace(slug, UrlPathVariableToken);
            }
        }

        public const string MembersLabel = "Members";
        public const string MemberSearchLabel = "Member Search";
        public const string RoleNameLabel = "Role Name";
        public const string RoleDescriptionLabel = "Role Description";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { MemberSearchLabel, "textarea#member_search" },
                { MemberSearchLabel.AutoCompleteMenuKey(), ".MemberSearch-field .autocomplete-menu ul" },

                { MembersLabel.CollectionItemTextKey(), "ul#members_list li.member .role-member" },
                { MembersLabel.CollectionItemRemoveKey("any1@suny.edu"), "ul#members_list li.member .role-member a.remove-button"},

                { RoleNameLabel, "input#RoleName" },

                { RoleDescriptionLabel, "textarea#Description" },
                { RoleDescriptionLabel.ErrorKey(), ".field-validation-error[data-valmsg-for=Description]" },
                { RoleDescriptionLabel.ErrorTextKey("Required"),
                    RoleForm.DescriptionRequiredErrorFormat
                        .FormatWith(RoleForm.DescriptionDisplayName) },
            };

        public override IDictionary<string, string> Fields
        {
            get
            {
                return FieldCss;
            }
        }
    }
}
