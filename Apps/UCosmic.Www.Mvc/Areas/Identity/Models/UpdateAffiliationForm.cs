using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class UpdateAffiliationForm : IReturnUrl
    {
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [Display(Name = JobTitlesDisplayName)]
        [DisplayFormat(NullDisplayText = JobTitlesNullDisplayText)]
        [DataType(DataType.MultilineText)]
        public string JobTitles { get; set; }
        public const string JobTitlesDisplayName = "Job Title(s) & Department(s)";
        public const string JobTitlesNullDisplayText = "[Job Title(s) Unknown]";

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

        [Display(Name = IsClaimingInternationalOfficeDisplayName)]
        public bool IsClaimingInternationalOffice { get; set; }
        public const string IsClaimingInternationalOfficeDisplayName = "I am employed in the international affairs office.";

        [Display(Name = IsClaimingAdministratorDisplayName)]
        public bool IsClaimingAdministrator { get; set; }
        public const string IsClaimingAdministratorDisplayName = "I am an administrator.";

        [Display(Name = IsClaimingFacultyDisplayName)]
        public bool IsClaimingFaculty { get; set; }
        public const string IsClaimingFacultyDisplayName = "I am a faculty member.";

        [Display(Name = IsClaimingStaffDisplayName)]
        public bool IsClaimingStaff { get; set; }
        public const string IsClaimingStaffDisplayName = "I am a staff employee.";

        [UIHint(EmployeeOrStudentAffiliationPropertyName)]
        public EmployeeOrStudentAffiliate? EmployeeOrStudentAffiliation { get; set; }
        public const string EmployeeOrStudentAffiliationPropertyName = "EmployeeOrStudentAffiliation";
    }

    public enum EmployeeOrStudentAffiliate
    {
        EmployeeOnly,
        StudentOnly,
        Both,
        Neither,
    }

    public class UpdateAffiliationValidator : AbstractValidator<UpdateAffiliationForm>
    {
        public const string FailedBecauseEmployeeOrStudentAffiliationWasEmpty =
            "Please indicate which statement best describes this affiliation.";

        public UpdateAffiliationValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EmployeeOrStudentAffiliation)
                // must indicate whether affiliation represents employee, student, neither, or both
                .NotEmpty()
                    .WithMessage(FailedBecauseEmployeeOrStudentAffiliationWasEmpty)
            ;
        }
    }

    public static class UpdateAffiliationProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Affiliation, UpdateAffiliationForm>()
                    .ForMember(d => d.EmployeeOrStudentAffiliation, o => o.ResolveUsing(s =>
                    {
                        if (!s.Establishment.IsInstitution)
                            return EmployeeOrStudentAffiliate.EmployeeOnly;

                        if (!s.IsAcknowledged)
                            return null;

                        if (s.IsClaimingEmployee)
                            if (s.IsClaimingStudent)
                                return EmployeeOrStudentAffiliate.Both;

                        if (s.IsClaimingEmployee)
                            if (!s.IsClaimingStudent)
                                return EmployeeOrStudentAffiliate.EmployeeOnly;

                        if (!s.IsClaimingEmployee)
                            if (s.IsClaimingStudent)
                                return EmployeeOrStudentAffiliate.StudentOnly;

                        return EmployeeOrStudentAffiliate.Neither;
                    }))
                    .ForMember(d => d.ReturnUrl, o => o.Ignore())
                ;
            }
        }

        public class ModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateAffiliationForm, UpdateMyAffiliationCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.ChangeCount, o => o.Ignore())
                ;
            }
        }
    }
}