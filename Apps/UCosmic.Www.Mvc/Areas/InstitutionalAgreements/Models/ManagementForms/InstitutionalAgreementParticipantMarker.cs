using System;
using System.Collections.Generic;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Models;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementParticipantMarker
    {
        public string ForEstablishmentOfficialName { get; set; }
        public string ForEstablishmentTranslatedName { get; set; }
        public string EstablishmentTranslatedThenOfficialName
        {
            get
            {
                var name = ForEstablishmentOfficialName;
                if (!ForEstablishmentOfficialName.Equals(ForEstablishmentTranslatedName))
                {
                    name = string.Format("{0} ({1})", ForEstablishmentTranslatedName, ForEstablishmentOfficialName);
                }
                return name;
            }
        }
        public Guid ForEstablishmentEntityId { get; set; }
        public double? CenterLatitude { get; set; }
        public double? CenterLongitude { get; set; }
        public ICollection<InstitutionalAgreementMapSearchResult> Agreements { get; set; }
    }

    public static class InstitutionalAgreementParticipantMarkerProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(InstitutionalAgreementParticipantMarkerProfiler));
        }

        internal class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EstablishmentLocation, InstitutionalAgreementParticipantMarker>()
                    .ForMember(t => t.Agreements, o => o.Ignore())
                ;
            }
        }
    }
}