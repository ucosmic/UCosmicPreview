﻿using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public static class UpdateAffiliationProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(UpdateAffiliationProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
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

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateAffiliationForm, UpdateMyAffiliationCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.ChangeCount, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}