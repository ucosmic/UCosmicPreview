using System;
using System.Collections.Generic;

namespace UCosmic.Www.Mvc
{
    public class CaseInsensitiveKeyComparer : IEqualityComparer<KeyValuePair<string, object>>
    {
        public bool Equals(KeyValuePair<string, object> x, KeyValuePair<string, object> y)
        {
            if (string.Compare(x.Key, y.Key, StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (x.Value == null && y.Value == null) return true;
                if (x.Value != null && x.Value.Equals(y.Value)) return true;
            }
            return false;
        }

        public int GetHashCode(KeyValuePair<string, object> obj)
        {
            return obj.GetHashCode();
        }
    }
}
