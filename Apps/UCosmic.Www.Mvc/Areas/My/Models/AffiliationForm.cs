using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class AffiliationForm : IReturnUrl
    {
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        public const string JobTitlesDisplayName = "Job Title(s) & Department(s)";
        public const string JobTitlesNullDisplayText = "[Job Title(s) Unknown]";
        [Display(Name = JobTitlesDisplayName)]
        [DisplayFormat(NullDisplayText = JobTitlesNullDisplayText)]
        [DataType(DataType.MultilineText)]
        public string JobTitles { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int EstablishmentId { get; set; }

        public EstablishmentInfo Establishment { get; set; }
        public class EstablishmentInfo
        {
            [HiddenInput(DisplayValue = false)]
            public string OfficialName { get; set; }

            [HiddenInput(DisplayValue = false)]
            public bool IsInstitution { get; set; }
        }

        [HiddenInput(DisplayValue = false)]
        public bool IsAcknowledged { get; set; }

        public bool IsClaimingEmployee
        {
            get
            {
                return EmployeeOrStudent == EmployeeOrStudentAnswer.EmployeeOnly
                    || EmployeeOrStudent == EmployeeOrStudentAnswer.Both;
            }
        }

        public bool IsClaimingStudent
        {
            get
            {
                return EmployeeOrStudent == EmployeeOrStudentAnswer.StudentOnly
                    || EmployeeOrStudent == EmployeeOrStudentAnswer.Both;
            }
        }

        public const string IsClaimingInternationalOfficeDisplayName = "I am employed in the international affairs office.";
        [Display(Name = IsClaimingInternationalOfficeDisplayName)]
        public bool IsClaimingInternationalOffice { get; set; }

        public const string IsClaimingAdministratorDisplayName = "I am an administrator.";
        [Display(Name = IsClaimingAdministratorDisplayName)]
        public bool IsClaimingAdministrator { get; set; }

        public const string IsClaimingFacultyDisplayName = "I am a faculty member.";
        [Display(Name = IsClaimingFacultyDisplayName)]
        public bool IsClaimingFaculty { get; set; }

        public const string IsClaimingStaffDisplayName = "I am a staff employee.";
        [Display(Name = IsClaimingStaffDisplayName)]
        public bool IsClaimingStaff { get; set; }

        public EmployeeOrStudentAnswer? EmployeeOrStudent { get; set; }
    }

    public enum EmployeeOrStudentAnswer
    {
        EmployeeOnly,
        StudentOnly,
        Both,
        Neither,
    }
}