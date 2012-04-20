using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    internal static class ValidateAffiliation
    {
        #region Cannot claim student affiliation unless establishment is an academic institution

        internal const string FailedBecauseIsClaimingStudentButEstablishmentIsNotInstitution =
            "Affiliation cannot claim student because establishment '{0}' is not an academic institution.";

        internal static bool EstablishmentIsInstitutionWhenIsClaimingStudent(bool isClaimingStudent, Establishment establishment)
        {
            // return false when user is claiming student but establishment is not an academic institution
            return establishment != null && (establishment.IsInstitution || isClaimingStudent == false);
        }

        #endregion
    }
}
