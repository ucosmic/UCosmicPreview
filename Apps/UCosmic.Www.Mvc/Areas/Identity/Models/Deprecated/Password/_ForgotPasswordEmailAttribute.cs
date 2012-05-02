//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Globalization;
//using System.Web.Security;
//using UCosmic.Domain;
//using UCosmic.Domain.People;

//namespace UCosmic.Www.Mvc.Areas.Identity.Models.Password
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
//    public class ForgotPasswordEmailAttribute : ValidationAttribute
//    {
//        public override string FormatErrorMessage(string name)
//        {
//            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, _userEnteredEmailAddress);
//        }

//        private string _userEnteredEmailAddress;

//        public override bool IsValid(object value)
//        {
//            // cast user input to string
//            var emailAddress = value as string;
//            _userEnteredEmailAddress = emailAddress;
//            if (emailAddress != null)
//            {
//                var queryEntities = DependencyInjector.Current.GetService<IQueryEntities>();
//                var finder = new PersonFinder(queryEntities);
//                var person = finder.FindOne(PersonBy.EmailAddress(emailAddress));
//                if (person != null && person.User != null)
//                {
//                    return Membership.FindUsersByName(emailAddress).Count > 0;
//                }
//            }
//            return false;
//        }
//    }
//}