using System.Collections.Generic;
using NEMILTEC.Shared.Classes;

namespace NEMILTEC.Service.Data.Objects.Queries
{
    public class DataQueryRowGrouping
    {
        public IDictionary<string, object> Projections { get; set; }

        public KeyValuePairEqualityComparer Comparer { get; set; }

        public override int GetHashCode()
        {
            return Comparer.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return this.GetHashCode() == ((DataQueryRowGrouping)obj).GetHashCode();

        }
    }
}
