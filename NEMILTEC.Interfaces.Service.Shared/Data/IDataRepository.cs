using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections;
using NEMILTEC.Interfaces.Service.Data;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Interfaces.Service.Shared.Data
{

    public interface IDataRepository<out T> :  IGenericRepository<T> where T : class, IDataEntity
    {
        IDataContext Context { set; }

        DataRepositoryProperties Properties { get; set; }
    }
}
