using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class AffiliationForm : IReturnUrl
    {
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [Display(Name = "Job Title(s) & Department(s)")]
        [DisplayFormat(NullDisplayText = "[Job Title(s) Unknown]")]
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

        public bool IsClaimingStudent { get; set; }
        
        public bool IsClaimingEmployee { get; set; }

        [Display(Name = "I am employed in the international affairs office.")]
        public bool IsClaimingInternationalOffice { get; set; }

        [Display(Name = "I am an administrator.")]
        public bool IsClaimingAdministrator { get; set; }

        [Display(Name = "I am a faculty member.")]
        public bool IsClaimingFaculty { get; set; }

        [Display(Name = "I am a staff employee.")]
        public bool IsClaimingStaff { get; set; }

        [Required(ErrorMessage = "Please indicate which statement best describes this affiliation.")]
        public EmployeeOrStudentAnswer? EmployeeOrStudent { get; set; }

        public void ApplyEmployeeOrStudentAnswer()
        {
            IsClaimingStudent = false;
            IsClaimingEmployee = true;
            if (!EmployeeOrStudent.HasValue) return;
            IsClaimingStudent = EmployeeOrStudent == EmployeeOrStudentAnswer.Both
                                || EmployeeOrStudent == EmployeeOrStudentAnswer.StudentOnly;
            IsClaimingEmployee = EmployeeOrStudent == EmployeeOrStudentAnswer.Both
                                 || EmployeeOrStudent == EmployeeOrStudentAnswer.EmployeeOnly;
            if (EmployeeOrStudent == EmployeeOrStudentAnswer.Neither)
            {
                IsClaimingEmployee = false;
            }
        }
    }

    public enum EmployeeOrStudentAnswer
    {
        EmployeeOnly,
        StudentOnly,
        Both,
        Neither,
    }
}