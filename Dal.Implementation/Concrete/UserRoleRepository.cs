using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Interface.Entities;
using Dal.Implementation;
using Dal.Interface.DataAccess;
using System.Data.Entity.Infrastructure;

namespace Dal.Implementation.Concrete
{
    public class UserRoleRepository : IUserRoleRepository
    {
        protected readonly DbContext _entitiesContext;

        public UserRoleRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            _entitiesContext = entitiesContext;
        }

        public IEnumerable<DalUserRole> GetAll()
        {
            return _entitiesContext.Set<DalUserRole>().AsEnumerable();
        }

        public IEnumerable<DalUserRole> Find(params System.Linq.Expressions.Expression<Func<DalUserRole, bool>>[] predicates)
        {
            IQueryable<DalUserRole> tmp = _entitiesContext.Set<DalUserRole>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            return tmp.AsEnumerable();
        }

        public DalUserRole FindById(long id)
        {
            return _entitiesContext.Set<DalUserRole>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(DalUserRole item)
        {
            DbEntityEntry<DalUserRole> dbEntityEntry = _entitiesContext.Entry<DalUserRole>(item);
            _entitiesContext.Set<DalUserRole>().Add(dbEntityEntry.Entity);
        }

        public void Edit(DalUserRole item)
        {
            DalUserRole changedItem = _entitiesContext.Set<DalUserRole>().FirstOrDefault(x => x.Id == item.Id);
            changedItem.RoleId = item.RoleId;
            changedItem.UserId = item.UserId;
            _entitiesContext.Entry(changedItem).State = EntityState.Modified;
        }

        public void Delete(DalUserRole item)
        {
            DalUserRole delItem = _entitiesContext.Set<DalUserRole>().FirstOrDefault(x => x.Id == item.Id);
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<DalUserRole>(delItem);
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
