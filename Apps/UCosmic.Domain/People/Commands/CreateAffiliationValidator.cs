using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    public class CreateAffiliationValidator : AbstractValidator<CreateAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public CreateAffiliationValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.EstablishmentId)
                .Cascade(CascadeMode.StopOnFirstFailure)

                // establishment id must exist in database
                .Must(ValidateEstablishmentIdMatchesEntity).WithMessage(
                    ValidateEstablishment.FailedBecauseIdMatchedNoEntity,
                        p => p.EstablishmentId)
            ;

            RuleFor(p => p.IsClaimingStudent)
                .Cascade(CascadeMode.StopOnFirstFailure)

                // cannot claim student unless affiliation establishment isn an academic institution
                .Must(ValidateAffiliationEstablishmentIsInstitutionWhenIsClaimingStudent).WithMessage(
                    ValidateAffiliation.FailedBecauseIsClaimingStudentButEstablishmentIsNotInstitution,
                        p => p.EstablishmentId)
            ;

            RuleFor(p => p.PersonId)
                .Cascade(CascadeMode.StopOnFirstFailure)

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
