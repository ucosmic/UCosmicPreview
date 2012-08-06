using AutoMapper;
using UCosmic.Domain.Places;

namespace UCosmic.Www.Mvc.Models
{
    public class CoordinatesModel
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool HasValue
        {
            get { return Latitude.HasValue && Longitude.HasValue; }
        }
    }

    public static class CoordinatesModelProfiler
    {
        public class ValueObjectToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Coordinates, CoordinatesModel>()
                    .ForMember(d => d.HasValue, o => o.Ignore())
                ;
            }
        }
    }
}