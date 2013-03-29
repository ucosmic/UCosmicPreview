using System.Collections.Generic;
using UCosmic.Domain;

namespace UCosmic
{
    public class RevisableEntityEqualityComparer : IEqualityComparer<RevisableEntity>
    {
        public bool Equals(RevisableEntity x, RevisableEntity y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return (x.RevisionId == y.RevisionId);
        }

        public int GetHashCode(RevisableEntity obj)
        {
            return obj.RevisionId.GetHashCode();
        }
    }
}