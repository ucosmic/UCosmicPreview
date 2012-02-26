using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Roles
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NoDuplicateGrantsAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, _duplicateUserName);
        }

        private string _duplicateUserName;

        public override bool IsValid(object value)
        {
            // cast user input to model
            var model = value as IList<RoleForm.RoleGrantForm>;
            if (model != null)
            {
                // foreach converted to LINQ expression
                //foreach (var grant in model)
                //{
                //    var matches = model.Where(m => m.UserId == grant.UserId && !m.IsDeleted);
                //    if (matches.Count() < 2) continue;
                //    _duplicateUserName = grant.UserUserName;
                //    return false;
                //}
                foreach (var grant in from grant in model
                                      let matches = model.Where(m =>
                                                                m.User.EntityId == grant.User.EntityId && !m.IsDeleted)
                                      where matches.Count() > 1
                                      select grant)
                {
                    _duplicateUserName = grant.User.UserName;
                    return false;
                }
                return true;
            }
            if (value == null)
            {
                return true;
            }
            throw new NotImplementedException(
                "The NoDuplicateGrantsAttribute only operates on IList<RoleGrantForm> values.");
        }
    }
}