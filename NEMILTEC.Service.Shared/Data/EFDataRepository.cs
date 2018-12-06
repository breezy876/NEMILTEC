using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NEMILTEC.Domain;
using NEMILTEC.Domain.Interfaces;
using NEMILTEC.Interfaces.Service.Shared.Data;
using NEMILTEC.Shared.Classes;
using System.Data.Entity;
using System.Collections;
using System.Reflection;
using NEMILTEC.Interfaces.Service.Domain;

namespace NEMILTEC.Service.Shared.Data
{
    /// <summary>
    /// generic repository data access object
    /// currently uses EF
    /// </summary>
    /// 



    public class EFDataRepository<T> : IDataRepository<T> where T : class, IDataEntity
    {

        public DataRepositoryProperties Properties { get; set; }

        public IDataContext Context
        {
            set
            {
                _context = value;
            }
        }

        private IDataContext _context;

        private Type _type;

        public EFDataRepository(IDataContext context) : this()
        {
            _context = context;
        }

        public EFDataRepository()
        {
            Properties = new DataRepositoryProperties();

            _type = typeof(T);
        }

        #region private methods
        private bool _QueryCondition(IDataEntity item)
        {
            return (Properties.User == null || (item as ITrackable).CreatedBy == Properties.User.Id) &&
                        (!Properties.ExcludeDeleted || !(item as IDeletable).IsDeleted);
        }

        private IQueryable<T> _GetQueryable(IQueryable<T> unFiltered = null)
        {


            IQueryable<T> qResult = unFiltered.IsNullOrEmpty() ? _context.GetSet<T>().AsQueryable() : unFiltered;

            if (Properties.User != null)
            {
                if (typeof(IQueryable<ITrackable>).IsAssignableFrom(qResult.GetType()))
                {
                    qResult = (qResult as IQueryable<ITrackable>).Where(q => q.CreatedBy == Properties.User.Id).Cast<T>();
                }
            }

            if (Properties.ExcludeDeleted)
            {
                if (typeof (IQueryable<IDeletable>).IsAssignableFrom(qResult.GetType()))
                {
                    qResult = (qResult as IQueryable<IDeletable>).Where(q => !q.IsDeleted).Cast<T>();
                }
            }

            if (Properties.IncludeRelated && !Properties.NavigationProperties.IsNullOrEmpty())
            {

                foreach (var navProp in Properties.NavigationProperties)
                {
                    qResult = qResult.Include(navProp as Expression<Func<T, object>>);
                }
            }

            return qResult;
        }

        private IQueryable<T> _GetResults(IQueryable<T> qInput)
        {
            if (Properties.LimitTotal)
            {
                var qResult = qInput as IQueryable<IDataEntity>;
                return qResult.OrderBy(q => q.Id).Skip((int)Properties.StartIndex).Take((int)Properties.TotalRows).Cast<T>();
            }
            return qInput;
        }
        #endregion

        #region static methods

        #endregion


        #region public methods

        #region read methods
        public IQueryable<T> GetAll()
        {
            return _GetResults(_GetQueryable());
        }

        public T Get(long id)
        {
            return _GetQueryable().FirstOrDefault(x => x.Id.Equals(id));
        }

        public IQueryable<T> Get(IEnumerable<long> ids)
        {
            return _GetResults(_GetQueryable().Where(x => ids.Contains(x.Id)));
        }

        public IQueryable<T> Query(Func<T, bool> cond)
        {
            return _GetResults(_GetQueryable().Where(cond).AsQueryable());
        }
        #endregion


        #region write methods
        public IEnumerable<long> AddOrUpdate(IEnumerable<IDataEntity> items)
        {
            var ids = new List<long>();
            foreach (var item in items)
            {
                var newId = AddOrUpdate(item);
                ids.Add(newId);
            }
            return ids;
        }

        public long AddOrUpdate(IDataEntity item)
        {
            long newId;
            if (((long)item.Id) == 0)
            {
                newId = _Add(item);
            }
            else
            {
                var existingItem = (IDataEntity)_context.Set(_type).Find(item.Id);
                if (existingItem != null)
                {
                    newId = _Update(existingItem, item);
                }
                else
                {
                    newId = _Add(item);
                }
            }
            if (Properties.SaveChanges)
            {
                _context.SaveChanges();
            }
            return newId;
        }

        private long _Add(IDataEntity item)
        {

            var set = _context.Set(_type);

            IDataEntity newItem;

            var delItem = (IDeletable)item;
            delItem.IsDeleted = false;

            if (Properties.TrackChanges)
            {
                var trackItem = (ITrackable)item;
                trackItem.DateCreated = DateTime.Now;
                if (Properties.User != null)
                {
                    trackItem.CreatedBy = Properties.User.Id;
                    trackItem.CreatedByUser = Properties.User;
                }
                newItem = (IDataEntity)set.Add(trackItem);
            }
            else
            {
                newItem = (IDataEntity)set.Add(item);
            }
            return newItem.Id;
        }

        private long _Update(IDataEntity existingItem, IDataEntity item)
        {
            if (Properties.TrackChanges)
            {
                var trackItem = (ITrackable) item;
                trackItem.DateModified = DateTime.Now;
                if (Properties.User != null)
                {
                    trackItem.ModifiedBy = Properties.User.Id;
                    trackItem.ModifiedByUser = Properties.User;
                }
                item = (IDataEntity) trackItem;
            }

            var props = _type.GetProperties();

            foreach (var prop in props)
            {
                var newVal = prop.GetValue(item, null);
                if (newVal != null)
                {
                    prop.SetValue(existingItem, newVal, null);
                }
            }

            return item.Id;

        }

        public void Remove(long id)
        {

            var set = _context.Set(_type);

            var existingItem = set.Find(id);

            if (existingItem != null)
            {
                if (Properties.DeletePermanent)
                {
                    set.Remove(existingItem);
                }
                else
                {
                    var delItem = (IDeletable)existingItem;
                    delItem.IsDeleted = true;

                    if (Properties.TrackChanges)
                    {
                        delItem.DateDeleted = DateTime.Now;
                        if (Properties.User != null)
                        {
                            delItem.DeletedBy = Properties.User.Id;
                        }
                    }
                    AddOrUpdate((IDataEntity)delItem);
                }

            }
            if (Properties.SaveChanges)
            {
                _context.SaveChanges();
            }
        }

        public void Remove(IEnumerable<long> ids)
        {
            foreach (var id in ids)
            {
                Remove(id);
            }
        }
        #endregion

        #endregion


    }

   

}