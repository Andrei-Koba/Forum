using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.DataAccess;
using Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Dal.Interface;

namespace Dal.Implementation
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        protected readonly DbContext _entitiesContext;

        public EntityRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("EntitiesContext is null");
            }
            _entitiesContext = context;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> res = _entitiesContext.Set<T>().AsEnumerable().ToList();
            return res;
        }

        public IEnumerable<T> Find(params System.Linq.Expressions.Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> tmp = _entitiesContext.Set<T>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            IEnumerable<T> res = tmp.AsEnumerable().ToList();
            return res;
        }

        public T FindById(long id)
        {
            T res = _entitiesContext.Set<T>().FirstOrDefault(x => x.Id == id);
            return res;
        }

        public void Add(T item)
        {
            try
            {
                DbEntityEntry<T> dbEntityEntry = _entitiesContext.Entry<T>(item);
                _entitiesContext.Set<T>().Add(dbEntityEntry.Entity);
            }
            catch (Exception e)
            {
                throw new RepositoryExceptions("Addition to database failed", e);
            }
        }

        public void Edit(T item)
        {
            try
            {
                T forEdit = FindById(item.Id);
                _entitiesContext.Entry<T>(forEdit).CurrentValues.SetValues(item);
                _entitiesContext.Entry(forEdit).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new RepositoryExceptions("Edition the database failed", e);
            }
        }

        public void Delete(T item)
        {
            try
            {
                T forDel = FindById(item.Id);
                DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(forDel);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception e)
            {
                throw new RepositoryExceptions("Deleting from database failed", e);
            }
        }

        public void Save()
        {
            try
            {
                _entitiesContext.SaveChanges();
            }
            catch (Exception e)
            {
                _entitiesContext.Dispose();
                throw new Exception("Dbcontext is unavalable", e);
            }
        }
    }
}
