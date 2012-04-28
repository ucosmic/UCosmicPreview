using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    public static class ValidateAffiliation
    {
        #region Cannot claim student affiliation unless establishment is an academic institution

        public const string FailedBecauseIsClaimingStudentButEstablishmentIsNotInstitution =
            "Affiliation cannot claim student because establishment '{0}' is not an academic institution.";

        public static bool EstablishmentIsInstitutionWhenIsClaimingStudent(bool isClaimingStudent, Establishment establishment)
        {
            // return false when user is claiming student but establishment is not an academic institution
            return establishment != null && (establishment.IsInstitution || isClaimingStudent == false);
        }

        #endregion
    }
}
