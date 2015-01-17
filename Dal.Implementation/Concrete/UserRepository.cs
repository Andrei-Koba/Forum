using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Interface.Entities;
using Dal.Implementation;
using Dal.Interface.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Dal.Implementation.Concrete
{
    public class UserRepository: IUserRepository
    {
        protected readonly DbContext _entitiesContext;

        public UserRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            _entitiesContext = entitiesContext;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _entitiesContext.Set<DalUser>().AsEnumerable();
        }

        public IEnumerable<DalUser> Find(params System.Linq.Expressions.Expression<Func<DalUser, bool>>[] predicates)
        {
            IQueryable<DalUser> tmp = _entitiesContext.Set<DalUser>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            return tmp.AsEnumerable();
        }

        public DalUser FindById(long id)
        {
            return _entitiesContext.Set<DalUser>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(DalUser item)
        {
            DbEntityEntry<DalUser> dbEntityEntry = _entitiesContext.Entry<DalUser>(item);
            _entitiesContext.Set<DalUser>().Add(dbEntityEntry.Entity);
        }

        public void Edit(DalUser item)
        {
            DalUser changedItem = _entitiesContext.Set<DalUser>().FirstOrDefault(x => x.Id == item.Id);
            changedItem.Avatar = item.Avatar;
            changedItem.Login = item.Login;
            changedItem.Mail = item.Mail;
            changedItem.Name = item.Name;
            changedItem.Pass = item.Pass;
            changedItem.RegistrationDate = item.RegistrationDate;
            _entitiesContext.Entry(changedItem).State = EntityState.Modified;
        }

        public void Delete(DalUser item)
        {
            DalUser delItem = _entitiesContext.Set<DalUser>().FirstOrDefault(x => x.Id == item.Id);
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<DalUser>(delItem);
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
