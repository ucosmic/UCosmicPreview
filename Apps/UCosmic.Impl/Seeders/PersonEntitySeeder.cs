using System;
using System.Linq;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Impl.Seeders
{
    public class PersonEntitySeeder : BasePersonEntitySeeder
    {
        private readonly IProcessQueries _queryProcessor;

        public PersonEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreatePersonCommand> createPerson
            , IHandleCommands<CreateAffiliationCommand> createAffiliation
            , IUnitOfWork unitOfWork
        )
            : base(queryProcessor, createPerson, createAffiliation, unitOfWork)
        {
            _queryProcessor = queryProcessor;
        }

        public override void Seed()
        {
            var suny = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.suny.edu"));
            var uc = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.uc.edu"));
            var lehigh = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.lehigh.edu"));
            var usil = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.usil.edu.pe"));
            var collegeBoard = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.collegeboard.org"));
            var terraDotta = _queryProcessor.Execute(new GetEstablishmentByUrlQuery("www.terradotta.com"));

            Seed(suny.RevisionId, new CreatePersonCommand
            {
                FirstName = "Mitch",
                LastName = "Leventhal",
                UserName = "Mitch.Leventhal@suny.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Mitch.Leventhal@suny.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                },
            });

            Seed(suny.RevisionId, new CreatePersonCommand
            {
                FirstName = "Sally",
                LastName = "Crimmins Villela",
                UserName = "Sally.Crimmins@suny.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Sally.Crimmins@suny.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                },
            });

            Seed(uc.RevisionId, new CreatePersonCommand
            {
                FirstName = "Ron",
                LastName = "Cushing",
                UserName = "Ronald.Cushing@uc.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Ronald.Cushing@uc.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Ronald.Cushing@ucmail.uc.edu",
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "cushinrb@uc.edu",
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "cushinrb@ucmail.uc.edu",
                        IsConfirmed = true,
                    },
                },
            });

            Seed(uc.RevisionId, new CreatePersonCommand
            {
                FirstName = "Mary",
                LastName = "Watkins",
                UserName = "Mary.Watkins@uc.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Mary.Watkins@uc.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Mary.Watkins@ucmail.uc.edu",
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "watkinml@uc.edu",
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "watkinml@ucmail.uc.edu",
                        IsConfirmed = true,
                    },
                },
            });

            Seed(lehigh.RevisionId, new CreatePersonCommand
            {
                FirstName = "Debra",
                LastName = "Nyby",
                UserName = "Debra.Nyby@lehigh.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Debra.Nyby@lehigh.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "dhn0@lehigh.edu",
                        IsConfirmed = true,
                    },
                },
            });

            Seed(lehigh.RevisionId, new CreatePersonCommand
            {
                FirstName = "Gary",
                LastName = "Lutz",
                UserName = "Gary.Lutz@lehigh.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Gary.Lutz@lehigh.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "jgl3@lehigh.edu",
                        IsConfirmed = true,
                    },
                },
            });

            Seed(lehigh.RevisionId, new CreatePersonCommand
            {
                FirstName = "Mohamed",
                LastName = "El-Aasser",
                UserName = "mohamed.el-aasser@lehigh.edu",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "mohamed.el-aasser@lehigh.edu",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "mse0@lehigh.edu",
                        IsConfirmed = true,
                    },
                },
            });

            Seed(usil.RevisionId, new CreatePersonCommand
            {
                FirstName = "Dora",
                LastName = "Ballen Uriarte",
                UserName = "DBallen@usil.edu.pe",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "DBallen@usil.edu.pe",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                },
            });

            Seed(collegeBoard.RevisionId, new CreatePersonCommand
            {
                FirstName = "Clay",
                LastName = "Hensley",
                UserName = "chensley@collegeboard.org",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "chensley@collegeboard.org",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                },
            });

            Seed(terraDotta.RevisionId, new CreatePersonCommand
            {
                FirstName = "Brandon",
                LastName = "Lee",
                UserName = "Brandon@terradotta.com",
                UserIsRegistered = true,
                EmailAddresses = new[]
                {
                    new CreatePersonCommand.EmailAddress
                    {
                        Value = "Brandon@terradotta.com",
                        IsDefault = true,
                        IsConfirmed = true,
                    },
                },
            });
        }

    }

    public abstract class BasePersonEntitySeeder : BaseDataSeeder
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly IHandleCommands<CreatePersonCommand> _createPerson;
        private readonly IHandleCommands<CreateAffiliationCommand> _createAffiliation;
        private readonly IUnitOfWork _unitOfWork;

        protected BasePersonEntitySeeder(IProcessQueries queryProcessor
            , IHandleCommands<CreatePersonCommand> createPerson
            , IHandleCommands<CreateAffiliationCommand> createAffiliation
            , IUnitOfWork unitOfWork
        )
        {
            _queryProcessor = queryProcessor;
            _createPerson = createPerson;
            _createAffiliation = createAffiliation;
            _unitOfWork = unitOfWork;
        }

        protected Person Seed(int? establishmentId, CreatePersonCommand command)
        {
            // make sure entity does not already exist
            var person = _queryProcessor.Execute(new GetPersonByEmailQuery
            {
                Email = command.EmailAddresses.First().Value,
            });
            if (person != null) return person;

            if (string.IsNullOrWhiteSpace(command.DisplayName))
            {
                command.DisplayName = string.Format("{0} {1}", command.FirstName, command.LastName);
            }

            _createPerson.Handle(command);
            _unitOfWork.SaveChanges();
            person = command.CreatedPerson;

            if (establishmentId.HasValue)
            {
                _createAffiliation.Handle(new CreateAffiliationCommand
                {
                    EstablishmentId = establishmentId.Value,
                    PersonId = person.RevisionId,
                    IsClaimingEmployee = true,
                    IsClaimingStudent = false,
                });
                _unitOfWork.SaveChanges();
            }
            else
            {
                throw new NotSupportedException("Why is the person not affiliated with an employer?");
            }

            return person;
        }
    }
}
