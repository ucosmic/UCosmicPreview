using System.Collections.Generic;
using OpenQA.Selenium;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    public class PersonalAffiliationPage : Page
    {
        public PersonalAffiliationPage(IWebDriver browser) : base(browser) { }
        public override string Title { get { return "Personal Affiliation"; } }
        public override string Path
        {
            get
            {
                const int number = 1;
                return MVC.Identity.UpdateAffiliation.Get(number).AsPath()
                    .Replace(number.ToObjectString(), UrlPathVariableToken);
            }
        }

        public const string AffiliationTypeLabel = "Affiliation Type";
        public const string AdditionalEmployeeDetailsLabel = "'Additional Employee Details'";
        public const string InternationalOfficeEmployeeLabel = "'International Office Employee'";
        public const string AdministratorEmployeeLabel = "'Administrator Employee'";
        public const string FacultyEmployeeLabel = "'Faculty Employee'";
        public const string StaffEmployeeLabel = "'Staff Employee'";
        public const string JobTitlesLabel = "Job Titles & Departments";

        private static readonly IDictionary<string, string> FieldCss =
            new Dictionary<string, string>
            {
                { AffiliationTypeLabel.RadioKey("I am an employee."), "input#EmployeeOrStudentAffiliation_EmployeeOnly" },
                { AffiliationTypeLabel.RadioKey("I am a student."), "input#EmployeeOrStudentAffiliation_StudentOnly" },
                { AffiliationTypeLabel.RadioKey("I am both an employee and a student."), "input#EmployeeOrStudentAffiliation_Both" },
                { AffiliationTypeLabel.RadioKey("I am neither an employee nor a student."), "input#EmployeeOrStudentAffiliation_Neither" },

                { AdditionalEmployeeDetailsLabel, "#employee_form" },

                { InternationalOfficeEmployeeLabel, "input#IsClaimingInternationalOffice" },
                { AdministratorEmployeeLabel, "input#IsClaimingAdministrator" },
                { FacultyEmployeeLabel, "input#IsClaimingFaculty" },
                { StaffEmployeeLabel, "input#IsClaimingStaff" },

                { JobTitlesLabel, "textarea#JobTitles" },
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
