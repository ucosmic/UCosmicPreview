using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentChanger
    {
        private readonly ICommandObjects _commander;
        private readonly IQueryEntities _entityQueries;

        public EstablishmentChanger(ICommandObjects objectCommander, IQueryEntities entityQueries)
        {
            if (objectCommander == null)
                throw new ArgumentNullException("objectCommander");

            if (entityQueries == null)
                throw new ArgumentNullException("entityQueries");

            _commander = objectCommander;
            _entityQueries = entityQueries;
        }

        public int Create(Establishment entity)
        {
            // when creating a new establishment, there will be no children.
            // however, there may be a parent and other ancestors.
            // each ancestor must have its offspring and children updated.
            return 0;
        }

        public int Modify(Establishment entity)
        {
            // when changing an existing establishment, there may be ancestors and offspring.
            // however, they only change when the establishment's parent has changed.
            // each ancestor in the old tree must have its offspring and children updated.
            // each ancestor and offspring in the new tree must have its offspring and ancestors updated.
            return 0;
        }

        public int Purge(Establishment entity)
        {
            return 0;
        }

        public void DeriveNodes()
        {
            var finder = new EstablishmentFinder(_entityQueries);
            var establishments = finder.FindMany(EstablishmentsWith.NoParentButWithChildren()
                .EagerLoad(e => e.Offspring.Select(o => o.Ancestor.Parent))
                .EagerLoad(e => e.Offspring.Select(o => o.Offspring.Parent))
                .EagerLoad(e => e.Offspring.Select(o => o.Ancestor.Children))
                .EagerLoad(e => e.Offspring.Select(o => o.Offspring.Children))
                .EagerLoad(e => e.Children.Select(c => c.Children.Select(g => g.Children)))
                .EagerLoad(e => e.Children.Select(c => c.Ancestors.Select(a => a.Ancestor)))
            );

            foreach (var parent in establishments)
                DeriveNodes(parent);
        }

        public void DeriveNodes(Establishment establishment, Establishment previousParent)
        {
            DeriveNodes(establishment);
            if (previousParent != null &&
                (establishment.Parent == null || establishment.Parent.EntityId != previousParent.EntityId))
                DeriveNodes(previousParent);
        }

        private void DeriveNodes(Establishment establishment)
        {
            if (establishment == null)
                throw new ArgumentNullException("establishment");

            var parent = establishment;
            while (parent.Parent != null)
                parent = parent.Parent;

            ClearNodesRecursive(parent);
            BuildNodesRecursive(parent);
        }

        private void ClearNodesRecursive(Establishment parent)
        {
            // ensure that the offspring and children properties are not null
            parent.Offspring = parent.Offspring ?? new List<EstablishmentNode>();
            parent.Children = parent.Children ?? new List<Establishment>();

            // delete all of this parent's offspring nodes
            while (parent.Offspring.FirstOrDefault() != null)
                _commander.Delete(parent.Offspring.FirstOrDefault());

            // operate recursively over children
            foreach (var child in parent.Children.Current())
            {
                // ensure that the child's ancestor nodes are not null
                child.Ancestors = child.Ancestors ?? new List<EstablishmentNode>();

                // delete each of the child's ancestor nodes
                while (child.Ancestors.FirstOrDefault() != null)
                    _commander.Delete(child.Ancestors.First());

                // run this method again on the child
                ClearNodesRecursive(child);
            }
        }

        private static void BuildNodesRecursive(Establishment parent)
        {
            // operate recursively over children
            foreach (var child in parent.Children.Current())
            {
                // create & add ancestor node for this child
                var node = new EstablishmentNode
                {
                    Ancestor = parent, Offspring = child, Separation = 1,
                };
                child.Ancestors.Add(node);

                // ensure the parent's ancestors nodes are not null
                parent.Ancestors = parent.Ancestors ?? new List<EstablishmentNode>();

                // loop over the parent's ancestors
                foreach (var ancestor in parent.Ancestors)
                {
                    // create & add ancestor node for this child
                    node = new EstablishmentNode
                    {
                        Ancestor = ancestor.Ancestor, Offspring = child, 
                        Separation = ancestor.Separation + 1,
                    };
                    child.Ancestors.Add(node);
                }

                // run this method again on the child
                BuildNodesRecursive(child);
            }
        }
    }
}
