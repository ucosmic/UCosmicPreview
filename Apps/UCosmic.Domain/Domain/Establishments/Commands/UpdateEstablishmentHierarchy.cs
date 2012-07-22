using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    internal class UpdateEstablishmentHierarchyCommand
    {
        internal UpdateEstablishmentHierarchyCommand(Establishment establishment)
        {
            if (establishment == null) throw new ArgumentNullException("establishment");
            Establishment = establishment;
        }

        internal Establishment Establishment { get; private set; }
    }

    internal class UpdateEstablishmentHierarchyHandler
    {
        private readonly ICommandEntities _entities;

        internal UpdateEstablishmentHierarchyHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        internal void Handle(UpdateEstablishmentHierarchyCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var parent = command.Establishment;
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
                _entities.Purge(parent.Offspring.First());

            // operate recursively over children
            foreach (var child in parent.Children)
            {
                // ensure that the child's ancestor nodes are not null
                child.Ancestors = child.Ancestors ?? new List<EstablishmentNode>();

                // delete each of the child's ancestor nodes
                while (child.Ancestors.FirstOrDefault() != null)
                    _entities.Purge(child.Ancestors.First());

                // run this method again on the child
                ClearNodesRecursive(child);
            }
        }

        private static void BuildNodesRecursive(Establishment parent)
        {
            // operate recursively over children
            foreach (var child in parent.Children)
            {
                // create & add ancestor node for this child
                var node = new EstablishmentNode
                {
                    Ancestor = parent,
                    Offspring = child,
                    Separation = 1,
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
                        Ancestor = ancestor.Ancestor,
                        Offspring = child,
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
