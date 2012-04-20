using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public static class AffiliationProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(AffiliationProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Affiliation, AffiliationForm>()
                    .ForMember(d => d.EmployeeOrStudent, o => o.ResolveUsing(s =>
                        {
                            if (s.IsAcknowledged)
                            {
                                if (s.IsClaimingEmployee && !s.IsClaimingStudent)
                                    return EmployeeOrStudentAnswer.EmployeeOnly;

                                if (s.IsClaimingEmployee && s.IsClaimingStudent)
                                    return EmployeeOrStudentAnswer.Both;

                                if (!s.IsClaimingEmployee && !s.IsClaimingStudent)
                                    return EmployeeOrStudentAnswer.Neither;

                                if (!s.IsClaimingEmployee && s.IsClaimingStudent)
                                    return EmployeeOrStudentAnswer.StudentOnly;
                            }
                            return null;
                        }))
                    .ForMember(d => d.ReturnUrl, o => o.Ignore())
                ;

                CreateMap<Establishment, AffiliationForm.EstablishmentInfo>()
                ;
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<AffiliationForm, UpdateMyAffiliationCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.ChangeCount, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}