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
    public class TopicRepository : ITopicRepository
    {

        protected readonly DbContext _entitiesContext;

        public TopicRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            _entitiesContext = entitiesContext;
        }

        public IEnumerable<DalTopic> GetAll()
        {
            return _entitiesContext.Set<DalTopic>().AsEnumerable();
        }

        public IEnumerable<DalTopic> Find(params System.Linq.Expressions.Expression<Func<DalTopic, bool>>[] predicates)
        {
            IQueryable<DalTopic> tmp = _entitiesContext.Set<DalTopic>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            return tmp.AsEnumerable();
        }

        public DalTopic FindById(long id)
        {
            return _entitiesContext.Set<DalTopic>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(DalTopic item)
        {
            DbEntityEntry<DalTopic> dbEntityEntry = _entitiesContext.Entry<DalTopic>(item);
            _entitiesContext.Set<DalTopic>().Add(dbEntityEntry.Entity);
        }

        public void Edit(DalTopic item)
        {
            DalTopic changedItem = _entitiesContext.Set<DalTopic>().FirstOrDefault(x => x.Id == item.Id);
            changedItem.CreationDate = item.CreationDate;
            changedItem.CreatorId = item.CreatorId;
            changedItem.Name = item.Name;
            changedItem.PostsCount = item.PostsCount;
            _entitiesContext.Entry(changedItem).State = EntityState.Modified;
        }

        public void Delete(DalTopic item)
        {
            DalTopic delItem = _entitiesContext.Set<DalTopic>().FirstOrDefault(x => x.Id == item.Id);
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<DalTopic>(delItem);
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
