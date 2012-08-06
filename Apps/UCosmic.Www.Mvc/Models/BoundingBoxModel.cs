using AutoMapper;
using UCosmic.Domain.Places;

namespace UCosmic.Www.Mvc.Models
{
    public class BoundingBoxModel
    {
        public CoordinatesModel Northeast { get; set; }
        public CoordinatesModel Southwest { get; set; }

        public bool HasValue
        {
            get
            {
                return Northeast != null && Southwest != null
                    && Northeast.HasValue && Southwest.HasValue;
            }
        }
    }

    public static class BoundingBoxModelProfiler
    {
        public class ValueObjectToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<BoundingBox, BoundingBoxModel>()
                    .ForMember(d => d.Northeast, o => o.ResolveUsing(
                        s => Mapper.Map<CoordinatesModel>(s.Northeast)))
                    .ForMember(d => d.Southwest, o => o.ResolveUsing(
                        s => Mapper.Map<CoordinatesModel>(s.Southwest)))
                    .ForMember(d => d.HasValue, o => o.Ignore())
                ;
            }
        }
    }
}