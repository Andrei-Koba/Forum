using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Implementation;
using Dal.Interface.Entities;
using Dal.Interface.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Dal.Implementation.Concrete
{
    public class PostRepository: IPostRepository
    {

        protected readonly DbContext _entitiesContext;

        public PostRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            _entitiesContext = entitiesContext;
        }

        public IEnumerable<DalPost> GetAll()
        {
            return _entitiesContext.Set<DalPost>().AsEnumerable();
        }

        public IEnumerable<DalPost> Find(params System.Linq.Expressions.Expression<Func<DalPost, bool>>[] predicates)
        {
            IQueryable<DalPost> tmp = _entitiesContext.Set<DalPost>().AsQueryable();
            tmp = predicates.Aggregate(tmp, (current, predicate) => current.Where(predicate));
            return tmp.AsEnumerable();
        }

        public DalPost FindById(long id)
        {
            return _entitiesContext.Set<DalPost>().FirstOrDefault(x => x.Id == id);
        }

        public void Add(DalPost item)
        {
            DbEntityEntry<DalPost> dbEntityEntry = _entitiesContext.Entry<DalPost>(item);
            _entitiesContext.Set<DalPost>().Add(dbEntityEntry.Entity);
        }

        public void Edit(DalPost item)
        {
            DalPost changedItem = _entitiesContext.Set<DalPost>().FirstOrDefault(x => x.Id == item.Id);
            changedItem.AuthorId = item.AuthorId;
            changedItem.Dislikes = item.Dislikes;
            changedItem.IsBlocked = item.IsBlocked;
            changedItem.LastEdit = item.LastEdit;
            changedItem.Likes = item.Likes;
            changedItem.Message = item.Message;
            changedItem.ResponseTo = item.ResponseTo;
            changedItem.TopicId = item.TopicId;
            _entitiesContext.Entry(changedItem).State = EntityState.Modified;
        }

        public void Delete(DalPost item)
        {
            DalPost delItem = _entitiesContext.Set<DalPost>().FirstOrDefault(x => x.Id == item.Id);
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<DalPost>(delItem);
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
