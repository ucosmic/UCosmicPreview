using System;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain.Activities
{
    public class UpdateMyActivityCommand : DraftMyActivityCommand
    {
        public ActivityMode Mode { get; set; }
    }

    public class UpdateMyActivityHandler : IHandleCommands<UpdateMyActivityCommand>
    {
        private readonly ICommandEntities _entities;

        public UpdateMyActivityHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(UpdateMyActivityCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var activity = _entities.Get2<Activity>()
                .EagerLoad(new Expression<Func<Activity, object>>[]
                {
                    t => t.Tags,
                    t => t.DraftedTags,
                }, _entities)
                .ByUserNameAndNumber(command.Principal.Identity.Name, command.Number);

            activity.Mode = command.Mode;
            activity.Values.Title = command.Title;
            activity.Values.Content = command.Content;
            activity.Values.StartsOn = command.StartsOn;
            activity.Values.EndsOn = command.EndsOn;
            activity.DraftedValues.Title = command.Title;
            activity.DraftedValues.Content = command.Content;
            activity.DraftedValues.StartsOn = command.StartsOn;
            activity.DraftedValues.EndsOn = command.EndsOn;

            if (command.Tags != null)
            {
                // remove deleted tags
                foreach (var tagToDelete in command.Tags.Where(t => t.IsDeleted)
                    .Select(deletedTag => activity.Tags
                        .Where(
                            tag => 
                            tag.Text == deletedTag.Text && 
                            tag.DomainType == deletedTag.DomainType && 
                            tag.DomainKey == deletedTag.DomainKey
                        ).ToArray()
                    )
                    .SelectMany(tagsToDelete => tagsToDelete)
                )
                {
                    activity.Tags.Remove(tagToDelete);
                }

                // add new tags
                foreach (var tagToAddOrKeep in 
                    from tagToAddOrKeep in command.Tags.Where(t => !t.IsDeleted) 
                    let tag = activity.Tags
                        .Where(
                            t => 
                            t.Text == tagToAddOrKeep.Text && 
                            t.DomainType == tagToAddOrKeep.DomainType && 
                            t.DomainKey == tagToAddOrKeep.DomainKey
                        ).ToArray()
                    where !tag.Any() 
                    select tagToAddOrKeep)
                {
                    activity.Tags.Add(new ActivityTag
                    {
                        Activity = activity,
                        ActivityNumber = activity.Number,
                        ActivityPersonId = activity.PersonId,
                        Number = activity.Tags.NextNumber(),
                        Text = tagToAddOrKeep.Text,
                        DomainType = tagToAddOrKeep.DomainType,
                        DomainKey = tagToAddOrKeep.DomainKey,
                    });
                }
            }

            // sync drafted tags with updated tags
            activity.DraftedTags.Clear();
            foreach (var tag in activity.Tags)
            {
                activity.DraftedTags.Add(new DraftedTag
                {
                    Activity = tag.Activity,
                    ActivityNumber = tag.ActivityNumber,
                    ActivityPersonId = tag.ActivityPersonId,
                    Number = tag.Number,
                    Text = tag.Text,
                    DomainType = tag.DomainType,
                    DomainKey = tag.DomainKey
                });
            }

            activity.UpdatedOn = DateTime.UtcNow;

            _entities.Update(activity);
        }
    }
}
