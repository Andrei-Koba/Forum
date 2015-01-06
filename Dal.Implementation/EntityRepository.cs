using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.DataAccess;
using Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Dal.Implementation
{
    public class EntityRepository<T> :IEntityRepository<T> where T : class, IEntity, new()
    {
        protected readonly DbContext _entitiesContext;

        public EntityRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            _entitiesContext = entitiesContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _entitiesContext.Set<T>().AsEnumerable();
        }

        public IEnumerable<T> Find(params System.Linq.Expressions.Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> tmp = _entitiesContext.Set<T>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            return tmp.AsEnumerable();
        }

        public T FindById(long id)
        {
            return _entitiesContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(T item)
        {
            DbEntityEntry<T> dbEntityEntry = _entitiesContext.Entry<T>(item);
            _entitiesContext.Set<T>().Add(dbEntityEntry.Entity);
        }

        public void Edit(T item)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(item);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(item);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Save()
        {
            _entitiesContext.SaveChanges();
        }

        public void Dispose()
        {
            _entitiesContext.Dispose();
        }
    }
}
