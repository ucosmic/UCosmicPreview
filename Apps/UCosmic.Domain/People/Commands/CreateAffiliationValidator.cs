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
                .Must(MatchEstablishment).WithMessage(
                    FailedBecauseEstablishmentIdDoesNotMatchEstablishment, 
                        p => p.EstablishmentId)
            ;

            RuleFor(p => p.PersonId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(MatchPerson).WithMessage(
                    FailedBecausePersonIdDoesNotMatchPerson, 
                        p => p.PersonId)
                .Must(NotAlreadyBeAffiliatedWithEstablishment).WithMessage(
                    FailedBecausePersonIsAlreadyAffiliatedWithEstablishment, 
                        p => p.PersonId, p => p.EstablishmentId)
            ;
        }

        internal const string FailedBecauseEstablishmentIdDoesNotMatchEstablishment = "Could not find establishment with id '{0}'.";

        private bool MatchEstablishment(int establishmentId)
        {
            var establishment = _queryProcessor.Execute(
                new GetEstablishmentByIdQuery
                {
                    Id = establishmentId,
                }
            );

            // return true (valid) if there is a person
            return establishment != null;
        }

        internal const string FailedBecausePersonIdDoesNotMatchPerson = "Could not find person with id '{0}'.";

        internal const string FailedBecausePersonIsAlreadyAffiliatedWithEstablishment = "Person '{0}' is already affiliated with establishment '{1}'.";

        private Person _person;

        private bool MatchPerson(int personId)
        {
            _person = _queryProcessor.Execute(
                new GetPersonByIdQuery
                {
                    Id = personId,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Affiliations.Select(a => a.Establishment)
                    },
                }
            );

            // return true (valid) if there is a person
            return _person != null;
        }

        private bool NotAlreadyBeAffiliatedWithEstablishment(CreateAffiliationCommand command, int personId)
        {
            // return true (valid) if person does not have matching affiliation
            return _person.GetAffiliation(command.EstablishmentId) == null;
        }
    }
}
