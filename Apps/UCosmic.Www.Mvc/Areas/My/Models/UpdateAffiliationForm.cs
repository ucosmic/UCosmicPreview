using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class UpdateAffiliationForm : IReturnUrl
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

        [HiddenInput(DisplayValue = false)]
        public string EstablishmentOfficialName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool EstablishmentIsInstitution { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsAcknowledged { get; set; }

        public bool IsClaimingEmployee
        {
            get
            {
                return EmployeeOrStudentAffiliation == EmployeeOrStudentAffiliate.EmployeeOnly
                    || EmployeeOrStudentAffiliation == EmployeeOrStudentAffiliate.Both;
            }
        }

        public bool IsClaimingStudent
        {
            get
            {
                return EmployeeOrStudentAffiliation == EmployeeOrStudentAffiliate.StudentOnly
                    || EmployeeOrStudentAffiliation == EmployeeOrStudentAffiliate.Both;
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

        public const string EmployeeOrStudentAffiliationPropertyName = "EmployeeOrStudentAffiliation";
        [UIHint(EmployeeOrStudentAffiliationPropertyName)]
        public EmployeeOrStudentAffiliate? EmployeeOrStudentAffiliation { get; set; }
    }

    public enum EmployeeOrStudentAffiliate
    {
        EmployeeOnly,
        StudentOnly,
        Both,
        Neither,
    }
}