//using System;
//using System.Collections.Generic;
//using System.Linq;
//using NEMILTEC.Interfaces.Data;

//namespace NEMILTEC.MVC.Code
//{

//    public class ListRepository : IGenericRepository
//    {

//        private List<IDataEntity> _data;

//        private IDataEntity _Find(long id)
//        {
//            return _data.FirstOrDefault(x => x.Id.Equals(id));
//        }

//        private IEnumerable<IDataEntity> _WhereIDs(IEnumerable<long> ids)
//        {
//            return _data.Where(x => ids.Contains(x.Id));
//        }

//        public ListRepository()
//        {
//            _data = new List<IDataEntity>();
//        }

//        public ListRepository(List<IDataEntity> data)
//        {
//            _data = data;
//        }


//        public long AddOrUpdate(IDataEntity item)
//        {
//            var existingItem = _Find(item.Id);

//            if (existingItem != null)
//            {
//                _data.Remove(existingItem);
//                _data.Add(item);
//            }
//            else
//            {
//                _data.Add(item);
//            }
//            return item.Id;
//        }

//        public IEnumerable<long> AddOrUpdate(IEnumerable<IDataEntity> items)
//        {
//            var idList = new List<long>();
//            foreach (var item in items)
//            {
//                var itemId = AddOrUpdate(item);
//                idList.Add(itemId);
//            }
//            return idList;
//        }

//        public IEnumerable<IDataEntity> Get(IEnumerable<long> ids)
//        {
//            return _WhereIDs(ids);
//        }

//        public IDataEntity Get(long id)
//        {
//            return _Find(id);
//        }

//        public IEnumerable<IDataEntity> GetAll()
//        {
//            return _data;
//        }

//        public IEnumerable<IDataEntity> Query(Func<IDataEntity, bool> cond)
//        {
//            return _data.Where(cond);
//        }

//        public void Remove(IEnumerable<long> ids)
//        {
//            var itemsToRemove = _WhereIDs(ids).ToArray();
//            foreach (var item in itemsToRemove)
//            {
//                _data.Remove(item);
//            }
//        }

//        public void Remove(long id)
//        {
//            IDataEntity item = _Find(id);
//            _data.Remove(item);
//        }

//        public void RemoveBy(Func<IDataEntity, bool> cond)
//        {
//            var itemsToRemove = _data.Where(cond);
//            foreach (var item in itemsToRemove)
//            {
//                _data.Remove(item);
//            }
//        }
//    }



//    public class ListRepository<T> : ListRepository, IGenericRepository<T> where T : class, IDataEntity
//    {
//        private List<T> _data;

//        private T _Find(long id)
//        {
//            return _data.FirstOrDefault(x => x.Id.Equals(id));
//        }

//        private IEnumerable<T> _WhereIDs(IEnumerable<long> ids)
//        {
//            return _data.Where(x => ids.Contains(x.Id));
//        }

//        public ListRepository()
//        {
//            _data = new List<T>();
//        }

//        public ListRepository(List<T> data)
//        {
//            _data = data;
//        }


//        public long AddOrUpdate(T item)
//        {
//            var existingItem = _Find(item.Id);

//            if (existingItem != null)
//            {
//                _data.Remove(existingItem);
//                _data.Add(item);
//            }
//            else
//            {
//                _data.Add(item);
//            }
//            return item.Id;
//        }

//        public IEnumerable<long> AddOrUpdate(IEnumerable<T> items)
//        {
//            var idList = new List<long>();
//            foreach (var item in items)
//            {
//                var itemId = AddOrUpdate(item);
//                idList.Add(itemId);
//            }
//            return idList;
//        }

//        public IEnumerable<T> Get(IEnumerable<long> ids)
//        {
//            return _WhereIDs(ids);
//        }

//        public T Get(long id)
//        {
//            return _Find(id);
//        }

//        public IEnumerable<T> GetAll()
//        {
//            return _data;
//        }

//        public IEnumerable<T> Query(Func<T, bool> cond)
//        {
//            return _data.Where(cond);
//        }

//        public void Remove(IEnumerable<long> ids)
//        {
//            var itemsToRemove = _WhereIDs(ids).ToArray();
//            foreach (var item in itemsToRemove)
//            {
//                _data.Remove(item);
//            }
//        }

//        public void Remove(long id)
//        {
//            T item = _Find(id);
//            _data.Remove(item);
//        }

//        public void RemoveBy(Func<T, bool> cond)
//        {
//            var itemsToRemove = _data.Where(cond);
//            foreach (var item in itemsToRemove)
//            {
//                _data.Remove(item);
//            }
//        }
//    }
//}
