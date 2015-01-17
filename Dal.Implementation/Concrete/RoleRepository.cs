using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.DataAccess;
using Dal.Implementation.Concrete;
using Dal.Interface.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Dal.Implementation.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly DbContext _entitiesContext;

        public RoleRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            _entitiesContext = entitiesContext;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _entitiesContext.Set<DalRole>().AsEnumerable();
        }

        public IEnumerable<DalRole> Find(params System.Linq.Expressions.Expression<Func<DalRole, bool>>[] predicates)
        {
            IQueryable<DalRole> tmp = _entitiesContext.Set<DalRole>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            return tmp.AsEnumerable();
        }

        public DalRole FindById(long id)
        {
            return _entitiesContext.Set<DalRole>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(DalRole item)
        {
            DbEntityEntry<DalRole> dbEntityEntry = _entitiesContext.Entry<DalRole>(item);
            _entitiesContext.Set<DalRole>().Add(dbEntityEntry.Entity);
        }

        public void Edit(DalRole item)
        {
            DalRole changedItem = _entitiesContext.Set<DalRole>().FirstOrDefault(x => x.Id == item.Id);
            changedItem.Name = item.Name;
            _entitiesContext.Entry(changedItem).State = EntityState.Modified;
        }

        public void Delete(DalRole item)
        {
            DalRole delItem = _entitiesContext.Set<DalRole>().FirstOrDefault(x => x.Id == item.Id);
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<DalRole>(delItem);
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
