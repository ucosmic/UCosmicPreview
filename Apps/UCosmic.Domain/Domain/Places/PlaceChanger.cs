using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace UCosmic.Domain.Places
{
    public class PlaceChanger
    {
        private readonly ICommandObjects _commander;

        public PlaceChanger(ICommandObjects objectCommander)
        {
            if (objectCommander == null) 
                throw new ArgumentNullException("objectCommander");
            _commander = objectCommander;
        }

        public int Create(Place entity, bool saveChanges = false)
        {
            DeriveNodes(entity);
            return _commander.Insert(entity, saveChanges);
        }

        public int Modify(Place entity)
        {
            return 0;
        }

        public int Purge(Place entity)
        {
            return 0;
        }

        private void DeriveNodes(Place entity)
        {
            entity.Ancestors = entity.Ancestors ?? new Collection<PlaceNode>();
            entity.Offspring = entity.Offspring ?? new Collection<PlaceNode>();

            entity.Ancestors.ToList().ForEach(node =>
                _commander.Delete(node));

            var separation = 1;
            var parent = entity.Parent;
            while (parent != null)
            {
                entity.Ancestors.Add(new PlaceNode
                {
                    Ancestor = parent,
                    Separation = separation++,
                });
                parent = parent.Parent;
            }
        }
    }
}
