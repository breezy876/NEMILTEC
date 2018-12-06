using System.Collections.Generic;
using System.Linq;

namespace NEMILTEC.Shared.Classes
{
    public class KeyValuePairEqualityComparer
    {

        public IDictionary<string, object> KeyValuePair;

        public override int GetHashCode()
        {
            if (KeyValuePair.IsNullOrEmpty())
                return 0;

            return (int)KeyValuePair.Select(kvp => kvp.Key.GetHashCode() ^ kvp.Value.GetHashCode()).Aggregate((x, y) => x ^ y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            var kvp = (KeyValuePairEqualityComparer)obj;

            if (KeyValuePair.IsNullOrEmpty() || kvp.KeyValuePair.IsNullOrEmpty())
                return false;
   
            return this.GetHashCode() == kvp.GetHashCode();

        }
    }
}
