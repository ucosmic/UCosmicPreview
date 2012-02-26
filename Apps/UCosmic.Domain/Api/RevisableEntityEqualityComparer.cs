using System.Collections.Generic;

namespace UCosmic.Domain
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