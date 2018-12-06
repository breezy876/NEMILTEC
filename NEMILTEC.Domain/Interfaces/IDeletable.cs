using System;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Domain.Interfaces
{
    public interface IDeletable : IDataEntity
    {
        DateTime? DateDeleted { get; set; }

        bool IsDeleted { get; set; }
        long? DeletedBy { get; set; }

        User DeletedByUser { get; set; }
    }
}
