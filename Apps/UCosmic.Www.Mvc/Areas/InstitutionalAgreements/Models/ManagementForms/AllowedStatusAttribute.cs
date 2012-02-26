using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UCosmic.Orm;
using UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Controllers;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowedStatusAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, _userStatus);
        }

        private string _userStatus;

        public override bool IsValid(object value)
        {
            _userStatus = value as string;
            if (_userStatus != null)
            {
                // open a new database connection
                using (var context = new UCosmicContext())
                {
                    var configuration = context.ForCurrentUserDefaultAffiliation(true);
                    if (configuration != null)
                    {
                        if (!configuration.IsCustomStatusAllowed && !configuration.AllowedStatusValues.Select(o => o.Text).Contains(_userStatus))
                            return false;
                    }
                } //close the database connection
            }
            return true;
        }
    }
}