using System;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public class UpdateEstablishmentHierarchiesCommand
    {
    }

    public class UpdateEstablishmentHierarchiesHandler : IHandleCommands<UpdateEstablishmentHierarchiesCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<UpdateEstablishmentHierarchyCommand> _hierarchyHandler;

        public UpdateEstablishmentHierarchiesHandler(IProcessQueries queryProcessor
            , IHandleCommands<UpdateEstablishmentHierarchyCommand> hierarchyHandler
        )
        {
            _queryProcessor = queryProcessor;
            _hierarchyHandler = hierarchyHandler;
        }

        public void Handle(UpdateEstablishmentHierarchiesCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get all root-level establishments with children
            var establishments = _queryProcessor.Execute(
                new FindRootEstablishmentsWithChildrenQuery
                {
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.Offspring.Select(o => o.Ancestor.Parent),
                        e => e.Offspring.Select(o => o.Offspring.Parent),
                        e => e.Offspring.Select(o => o.Ancestor.Children),
                        e => e.Offspring.Select(o => o.Offspring.Children),
                        e => e.Children.Select(c => c.Children.Select(g => g.Children)),
                        e => e.Children.Select(c => c.Ancestors.Select(a => a.Ancestor))
                    }
                }
            );

            // derive nodes for each parent
            foreach (var parent in establishments)
                //DeriveNodes(parent);
                _hierarchyHandler.Handle(new UpdateEstablishmentHierarchyCommand(parent));
        }

        //private void DeriveNodes(Establishment establishment)
        //{
        //    var parent = establishment;
        //    while (parent.Parent != null)
        //        parent = parent.Parent;

        //    ClearNodesRecursive(parent);
        //    BuildNodesRecursive(parent);
        //}

        //private void ClearNodesRecursive(Establishment parent)
        //{
        //    // ensure that the offspring and children properties are not null
        //    parent.Offspring = parent.Offspring ?? new List<EstablishmentNode>();
        //    parent.Children = parent.Children ?? new List<Establishment>();

        //    // delete all of this parent's offspring nodes
        //    while (parent.Offspring.FirstOrDefault() != null)
        //        _entities.Purge(parent.Offspring.First());

        //    // operate recursively over children
        //    foreach (var child in parent.Children.Current())
        //    {
        //        // ensure that the child's ancestor nodes are not null
        //        child.Ancestors = child.Ancestors ?? new List<EstablishmentNode>();

        //        // delete each of the child's ancestor nodes
        //        while (child.Ancestors.FirstOrDefault() != null)
        //            _entities.Purge(child.Ancestors.First());

        //        // run this method again on the child
        //        ClearNodesRecursive(child);
        //    }
        //}

        //private static void BuildNodesRecursive(Establishment parent)
        //{
        //    // operate recursively over children
        //    foreach (var child in parent.Children.Current())
        //    {
        //        // create & add ancestor node for this child
        //        var node = new EstablishmentNode
        //        {
        //            Ancestor = parent,
        //            Offspring = child,
        //            Separation = 1,
        //        };
        //        child.Ancestors.Add(node);

        //        // ensure the parent's ancestors nodes are not null
        //        parent.Ancestors = parent.Ancestors ?? new List<EstablishmentNode>();

        //        // loop over the parent's ancestors
        //        foreach (var ancestor in parent.Ancestors)
        //        {
        //            // create & add ancestor node for this child
        //            node = new EstablishmentNode
        //            {
        //                Ancestor = ancestor.Ancestor,
        //                Offspring = child,
        //                Separation = ancestor.Separation + 1,
        //            };
        //            child.Ancestors.Add(node);
        //        }

        //        // run this method again on the child
        //        BuildNodesRecursive(child);
        //    }
        //}
    }
}
