using System;
using System.Collections.Generic;
using System.Linq;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Interfaces.Service.Data
{

    public interface IGenericRepository<out T>  where T : class, IDataEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(IEnumerable<long> ids);

        T Get(long id);

        IQueryable<T> Query(Func<T, bool> cond);

        IEnumerable<long> AddOrUpdate(IEnumerable<IDataEntity> items);
        long AddOrUpdate(IDataEntity item);

        void Remove(long id);
        void Remove(IEnumerable<long> ids);

    }


}

