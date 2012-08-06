using System;
using System.Linq;
using UCosmic.Domain.Languages;
using UCosmic.Domain.Places;

namespace UCosmic.Domain.Establishments
{
    public class CreateEstablishment
    {
        public string OfficialName { get; set; }
        public bool IsMember { get; set; }
        public int? ParentId { get; set; }
        public int TypeId { get; set; }
        public string UCosmicCode { get; set; }
        public string OfficialWebsiteUrl { get; set; }
        public string[] EmailDomains { get; set; }
        public double? CenterLatitude { get; set; }
        public double? CenterLongitude { get; set; }
        public bool FindPlacesByCoordinates { get; set; }
        public NonOfficialName[] NonOfficialNames { get; set; }
        public NonOfficialUrl[] NonOfficialUrls { get; set; }
        public Address[] Addresses { get; set; }
        public ContactInfo PublicContactInfo { get; set; }
        public Establishment CreatedEstablishment { get; internal set; }

        public class NonOfficialName
        {
            public string Text { get; set; }
            public bool IsDefunct { get; set; }
            public int? TranslationToLanguageId { get; set; }
        }

        public class Address
        {
            public string Text { get; set; }
            public int? TranslationToLanguageId { get; set; }
        }

        public class NonOfficialUrl
        {
            public string Value { get; set; }
            public bool IsDefunct { get; set; }
        }

        public class ContactInfo
        {
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
        }
    }

    public class HandleCreateEstablishmentCommand : IHandleCommands<CreateEstablishment>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IHandleCommands<UpdateEstablishmentHierarchyCommand> _hierarchy;

        public HandleCreateEstablishmentCommand(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IHandleCommands<UpdateEstablishmentHierarchyCommand> hierarchy
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _hierarchy = hierarchy;
        }

        public void Handle(CreateEstablishment command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var parent = command.ParentId.HasValue
                ? _entities.FindByPrimaryKey<Establishment>(command.ParentId.Value)
                : null;

            var type = _entities.FindByPrimaryKey<EstablishmentType>(command.TypeId);

            var entity = new Establishment
            {
                OfficialName = command.OfficialName,
                Parent = parent,
                WebsiteUrl = command.OfficialWebsiteUrl,
                IsMember = command.IsMember,
                Type = type,
                Location = new EstablishmentLocation(),
            };

            // add official name to list
            entity.Names.Add(new EstablishmentName
            {
                Text = command.OfficialName,
                IsOfficialName = true,
            });

            // add non-official names
            if (command.NonOfficialNames != null && command.NonOfficialNames.Any())
                foreach (var nonOfficialName in command.NonOfficialNames)
                    entity.Names.Add(new EstablishmentName
                    {
                        Text = nonOfficialName.Text,
                        IsFormerName = nonOfficialName.IsDefunct,
                        TranslationToLanguage = nonOfficialName.TranslationToLanguageId.HasValue
                            ? _entities.FindByPrimaryKey<Language>(nonOfficialName.TranslationToLanguageId.Value)
                            : null,
                    });

            // add official url to list
            if (!string.IsNullOrWhiteSpace(command.OfficialWebsiteUrl))
                entity.Urls.Add(new EstablishmentUrl
                {
                    Value = command.OfficialWebsiteUrl,
                    IsOfficialUrl = true,
                });

            // add non-official URL's
            if (command.NonOfficialUrls != null && command.NonOfficialUrls.Any())
                foreach (var nonOfficialUrl in command.NonOfficialUrls)
                    entity.Urls.Add(new EstablishmentUrl
                    {
                        Value = nonOfficialUrl.Value,
                        IsFormerUrl = nonOfficialUrl.IsDefunct,
                    });

            // add email domains
            if (command.EmailDomains != null && command.EmailDomains.Any())
                foreach (var emailDomain in command.EmailDomains)
                    entity.EmailDomains.Add(new EstablishmentEmailDomain
                    {
                        Value = emailDomain,
                    });

            // apply coordinates
            if (command.FindPlacesByCoordinates &&
                command.CenterLatitude.HasValue && command.CenterLongitude.HasValue)
            {
                var woeId = _queryProcessor.Execute(new WoeIdByCoordinates(
                        command.CenterLatitude.Value, command.CenterLongitude.Value));
                var place = _queryProcessor.Execute(
                    new GetPlaceByWoeIdQuery { WoeId = woeId });
                var places = place.Ancestors.OrderByDescending(n => n.Separation)
                    .Select(a => a.Ancestor).ToList();
                places.Add(place);
                entity.Location.Center = new Coordinates(command.CenterLatitude, command.CenterLongitude);
                entity.Location.BoundingBox = place.BoundingBox;
                entity.Location.Places = places;
            }

            // apply addresses
            if (command.Addresses != null && command.Addresses.Any())
            {
                foreach (var address in command.Addresses)
                {
                    entity.Location.Addresses.Add(new EstablishmentAddress
                    {
                        Text = address.Text,
                        TranslationToLanguage = _entities.FindByPrimaryKey<Language>(address.TranslationToLanguageId),
                    });
                }
            }

            // apply contact info
            if (command.PublicContactInfo != null)
            {
                entity.PublicContactInfo = new EstablishmentContactInfo
                {
                    Email = command.PublicContactInfo.Email,
                    Phone = command.PublicContactInfo.Phone,
                    Fax = command.PublicContactInfo.Fax,
                };
            }

            // apply CEEB / UCosmic code
            if (!string.IsNullOrWhiteSpace(command.UCosmicCode))
                entity.UCosmicCode = command.UCosmicCode;

            _entities.Create(entity);
            _hierarchy.Handle(new UpdateEstablishmentHierarchyCommand(entity));
            command.CreatedEstablishment = entity;
        }
    }
}
