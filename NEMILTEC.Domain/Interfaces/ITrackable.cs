using System;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Domain.Interfaces
{
    public interface ITrackable : IDataEntity
    {
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }

        long? CreatedBy { get; set; }
        long? ModifiedBy { get; set; }

        User CreatedByUser { get; set; }
        User ModifiedByUser { get; set; }
    }
}
