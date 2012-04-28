using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    public class CreateAffiliationCommand
    {
        public int PersonId { get; set; }
        public int EstablishmentId { get; set; }
        public bool IsClaimingStudent { get; set; }
        public bool IsClaimingEmployee { get; set; }
    }

    public class CreateAffiliationHandler : IHandleCommands<CreateAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public CreateAffiliationHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(CreateAffiliationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the person
            var person = _queryProcessor.Execute(
                new GetPersonByIdQuery
                {
                    Id = command.PersonId,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Affiliations,
                    },
                }
            );

            // construct the affiliation
            var affiliation = new Affiliation
            {
                EstablishmentId = command.EstablishmentId,
                IsClaimingStudent = command.IsClaimingStudent,
                IsClaimingEmployee = command.IsClaimingEmployee,
                IsDefault = !person.Affiliations.Any(a => a.IsDefault),
            };
            person.Affiliations.Add(affiliation);

            // store
            _entities.Create(affiliation);
        }
    }

    public class CreateAffiliationValidator : AbstractValidator<CreateAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public CreateAffiliationValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.EstablishmentId)

                // establishment id must exist in database
                .Must(ValidateEstablishmentIdMatchesEntity).WithMessage(
                    ValidateEstablishment.FailedBecauseIdMatchedNoEntity,
                        p => p.EstablishmentId)
            ;

            RuleFor(p => p.IsClaimingStudent)

                // cannot claim student unless affiliation establishment is an academic institution
                .Must(ValidateAffiliationEstablishmentIsInstitutionWhenIsClaimingStudent).WithMessage(
                    ValidateAffiliation.FailedBecauseIsClaimingStudentButEstablishmentIsNotInstitution,
                        p => p.EstablishmentId)
            ;

            RuleFor(p => p.PersonId)

                // person id must exist in database
                .Must(ValidatePersonIdMatchesEntity).WithMessage(
                    ValidatePerson.FailedBecauseIdMatchedNoEntity,
                        p => p.PersonId)

                // cannot create a duplicate affiliation
                .Must(ValidatePersonIsNotAlreadyAffiliatedWithEstablishment).WithMessage(
                    ValidatePerson.FailedBecausePersonIsAlreadyAffiliatedWithEstablishment,
                        p => p.PersonId, p => p.EstablishmentId)
            ;
        }

        private Person _person;
        private Establishment _establishment;

        private bool ValidateEstablishmentIdMatchesEntity(int establishmentId)
        {
            return ValidateEstablishment.IdMatchesEntity(establishmentId, _queryProcessor,
                new Expression<Func<Establishment, object>>[]
                {
                    e => e.Type.Category,
                },
                out _establishment
            );
        }

        private bool ValidatePersonIdMatchesEntity(int personId)
        {
            return ValidatePerson.IdMatchesEntity(personId, _queryProcessor,
                new Expression<Func<Person, object>>[]
                {
                    p => p.Affiliations.Select(a => a.Establishment)
                },
                out _person
            );
        }

        private bool ValidateAffiliationEstablishmentIsInstitutionWhenIsClaimingStudent(bool isClaimingStudent)
        {
            return ValidateAffiliation.EstablishmentIsInstitutionWhenIsClaimingStudent(isClaimingStudent, _establishment);
        }

        private bool ValidatePersonIsNotAlreadyAffiliatedWithEstablishment(CreateAffiliationCommand command, int personId)
        {
            return ValidatePerson.IsNotAlreadyAffiliatedWithEstablishment(_person, command.EstablishmentId);
        }
    }
}
