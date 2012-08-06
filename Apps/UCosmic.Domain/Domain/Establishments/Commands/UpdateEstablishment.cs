using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain.Places;

namespace UCosmic.Domain.Establishments
{
    public class UpdateEstablishment
    {
        public int Id { get; set; }
        public double? CenterLatitude { get; set; }
        public double? CenterLongitude { get; set; }
        public double? NorthLatitude { get; set; }
        public double? EastLongitude { get; set; }
        public double? SouthLatitude { get; set; }
        public double? WestLongitude { get; set; }
        public int? GoogleMapZoomLevel { get; set; }
        public IEnumerable<int> PlaceIds { get; set; }
    }

    public class HandleUpdateEstablishmentCommand : IHandleCommands<UpdateEstablishment>
    {
        private readonly ICommandEntities _entities;
        private readonly IHandleCommands<UpdateEstablishmentHierarchyCommand> _hierarchy;

        public HandleUpdateEstablishmentCommand(ICommandEntities entities
            , IHandleCommands<UpdateEstablishmentHierarchyCommand> hierarchy
        )
        {
            _entities = entities;
            _hierarchy = hierarchy;
        }

        public void Handle(UpdateEstablishment command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var entity = _entities.Get<Establishment>()
                .EagerLoad(_entities, new Expression<Func<Establishment, object>>[]
                {
                    e => e.Location.Places,
                })
                .By(command.Id);

            if (command.CenterLatitude.HasValue && command.CenterLongitude.HasValue)
                entity.Location.Center = new Coordinates(command.CenterLatitude, command.CenterLongitude);

            if (command.NorthLatitude.HasValue && command.EastLongitude.HasValue &&
                command.SouthLatitude.HasValue && command.WestLongitude.HasValue)
                entity.Location.BoundingBox = new BoundingBox(command.NorthLatitude, command.EastLongitude,
                    command.SouthLatitude, command.WestLongitude);

            if (command.GoogleMapZoomLevel.HasValue)
                entity.Location.GoogleMapZoomLevel = command.GoogleMapZoomLevel;

            if (command.PlaceIds != null && command.PlaceIds.Any())
            {
                entity.Location.Places.Clear();
                foreach (var placeId in command.PlaceIds)
                {
                    entity.Location.Places.Add(_entities.Get<Place>().By(placeId));
                }
            }

            _entities.Update(entity);
            _hierarchy.Handle(new UpdateEstablishmentHierarchyCommand(entity));
        }
    }
}
