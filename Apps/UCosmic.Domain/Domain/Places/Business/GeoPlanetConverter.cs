using AutoMapper;

namespace UCosmic.Domain.Places
{
    internal static class GeoPlanetConverter
    {
        static GeoPlanetConverter()
        {
            Configure();
        }

        internal static void Configure()
        {
            #region Yahoo Place to GeoPlanetPlace Entity

            Mapper.CreateMap<NGeo.Yahoo.GeoPlanet.Place, GeoPlanetPlace>()
                .ForMember(target => target.EnglishName, opt => opt.MapFrom(source => source.Name))
                .ForMember(target => target.Uri, opt => opt.MapFrom(source => source.Uri.ToString()))
                .ForMember(target => target.Country, opt => opt.MapFrom(source => (source.Country != null)
                    ? Mapper.Map<GeoPlanetAdmin>(source.Country) : new GeoPlanetAdmin()))
                .ForMember(target => target.Admin1, opt => opt.MapFrom(source => (source.Admin1 != null)
                    ? Mapper.Map<GeoPlanetAdmin>(source.Admin1) : new GeoPlanetAdmin()))
                .ForMember(target => target.Admin2, opt => opt.MapFrom(source => (source.Admin2 != null)
                    ? Mapper.Map<GeoPlanetAdmin>(source.Admin2) : new GeoPlanetAdmin()))
                .ForMember(target => target.Admin3, opt => opt.MapFrom(source => (source.Admin3 != null)
                    ? Mapper.Map<GeoPlanetAdmin>(source.Admin3) : new GeoPlanetAdmin()))
                .ForMember(target => target.Locality1, opt => opt.MapFrom(source => (source.Locality1 != null)
                    ? Mapper.Map<GeoPlanetLocality>(source.Locality1) : new GeoPlanetLocality()))
                .ForMember(target => target.Locality2, opt => opt.MapFrom(source => (source.Locality2 != null)
                    ? Mapper.Map<GeoPlanetLocality>(source.Locality2) : new GeoPlanetLocality()))
            ;

            #endregion
            #region Yahoo PlaceType to GeoPlanetPlaceType Entity

            Mapper.CreateMap<NGeo.Yahoo.GeoPlanet.PlaceType, GeoPlanetPlaceType>()
                .ForMember(target => target.EnglishName, opt => opt.MapFrom(source => source.Name))
                .ForMember(target => target.EnglishDescription, opt => opt.MapFrom(source => source.Description))
                .ForMember(target => target.Uri, opt => opt.MapFrom(source => source.Uri.ToString()))
            ;

            #endregion
            #region Yahoo Point to Coordinates Complex Type

            Mapper.CreateMap<NGeo.Yahoo.GeoPlanet.Point, Coordinates>();

            #endregion
            #region Yahoo BoundingBox to BoundingBox Complex Type

            Mapper.CreateMap<NGeo.Yahoo.GeoPlanet.BoundingBox, BoundingBox>();

            #endregion
            #region Yahoo Admin to GeoPlanetAdmin Complex Type

            Mapper.CreateMap<NGeo.Yahoo.GeoPlanet.Admin, GeoPlanetAdmin>()
                .ForMember(target => target.TypeName, opt => opt.MapFrom(source => source.Type))
            ;

            #endregion
            #region Yahoo Locality to GeoPlanetLocality Complex Type

            Mapper.CreateMap<NGeo.Yahoo.GeoPlanet.Locality, GeoPlanetLocality>()
                .ForMember(target => target.TypeName, opt => opt.MapFrom(source => source.Type))
            ;

            #endregion
            #region GeoPlanetPlace Entity to Place

            Mapper.CreateMap<GeoPlanetPlace, Place>()
                .ForMember(target => target.OfficialName, opt => opt.ResolveUsing(source => source.EnglishName))
                .ForMember(target => target.IsEarth, opt => opt.ResolveUsing(source =>
                    source.WoeId == 1))
                //.ForMember(target => target.IsContinent, opt => opt.ResolveUsing(source =>
                //    source.Type.Code == 29))
                //.ForMember(target => target.IsCountry, opt => opt.ResolveUsing(source =>
                //    source.Type.Code == 12))
                //.ForMember(target => target.IsAdmin1, opt => opt.ResolveUsing(source =>
                //    source.Type.Code == 8))
                //.ForMember(target => target.IsAdmin2, opt => opt.ResolveUsing(source =>
                //    source.Type.Code == 9))
                //.ForMember(target => target.IsAdmin3, opt => opt.ResolveUsing(source =>
                //    source.Type.Code == 10))
                .ForMember(target => target.Parent, opt => opt.Ignore())
                .ForMember(target => target.Children, opt => opt.Ignore())
                .ForMember(target => target.Ancestors, opt => opt.Ignore())
                .ForMember(target => target.Offspring, opt => opt.Ignore())
            ;

            #endregion
        }

        internal static GeoPlanetPlace ToEntity(this NGeo.Yahoo.GeoPlanet.Place geoPlanetPlace)
        {
            return (geoPlanetPlace != null) ? Mapper.Map<GeoPlanetPlace>(geoPlanetPlace) : null;
        }

        internal static GeoPlanetPlaceType ToEntity(this NGeo.Yahoo.GeoPlanet.PlaceType geoPlanetPlaceType)
        {
            return (geoPlanetPlaceType != null) ? Mapper.Map<GeoPlanetPlaceType>(geoPlanetPlaceType) : null;
        }

        internal static Place ToPlace(this GeoPlanetPlace geoPlanetPlace)
        {
            if (geoPlanetPlace == null) return null;

            var place = Mapper.Map<Place>(geoPlanetPlace);
            place.GeoPlanetPlace = geoPlanetPlace;
            return place;
        }

    }

}