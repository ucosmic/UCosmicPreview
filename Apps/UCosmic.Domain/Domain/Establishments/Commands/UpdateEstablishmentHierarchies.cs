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
        private readonly ICommandEntities _entities;

        public UpdateEstablishmentHierarchiesHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(UpdateEstablishmentHierarchiesCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get all root-level establishments with children
            var establishments = _entities.Get<Establishment>()
                .EagerLoad(_entities, new Expression<Func<Establishment, object>>[]
                {
                    e => e.Offspring.Select(o => o.Ancestor.Parent),
                    e => e.Offspring.Select(o => o.Offspring.Parent),
                    e => e.Offspring.Select(o => o.Ancestor.Children),
                    e => e.Offspring.Select(o => o.Offspring.Children),
                    e => e.Children.Select(c => c.Children.Select(g => g.Children)),
                    e => e.Children.Select(c => c.Ancestors.Select(a => a.Ancestor))
                })
                .IsRoot()
                .WithAnyChildren()
                .ToArray()
            ;

            // derive nodes for each parent
            foreach (var parent in establishments)
                new UpdateEstablishmentHierarchyHandler(_entities)
                    .Handle(new UpdateEstablishmentHierarchyCommand(parent));
        }
    }
}
