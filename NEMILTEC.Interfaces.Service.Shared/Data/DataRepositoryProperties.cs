using System.Collections.Generic;
using System.Linq.Expressions;

namespace NEMILTEC.Interfaces.Service.Shared.Data
{
    public class DataRepositoryProperties
    {
        public DataRepositoryProperties()
        {
            IncludeRelated = true;
            ExcludeDeleted = true;
            SaveChanges = true;
            DeletePermanent = false;
        }

        public NEMILTEC.Domain.User User { get; set; }

        public bool ExcludeDeleted { get; set; }
        public bool TrackChanges { get; set; }
        public bool SaveChanges { get; set; }
        public bool DeletePermanent { get; set; }
        public bool IncludeRelated { get; set; }
        public bool LimitTotal { get; set; }

        public long StartIndex { get; set; }
        public long TotalRows { get; set; }

        public IEnumerable<object> NavigationProperties { get; set; }
    }
}
