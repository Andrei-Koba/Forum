using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Implementation;
using Bll.Interface.Entities;
using Dal.Interface.Entities;
using Dal.Implementation.Concrete;
using Bll.Implementation.EntityMappers;
using Bll.Interface;
using Dal.Interface.DataAccess;
using System.Linq.Expressions;
using Bll.Interface.DataServices;
using Dal.Interface;

namespace Bll.Implementation.Services
{
    public class PostService : BaseService<DalPost, Post, IEntityRepository<DalPost>, PostMapper>, IPostService
    {

        public PostService(IPostRepository repository, IUnitOfWork uow)
            : base(repository, uow)
        { }

        public IEnumerable<Post> GetByAuthor(long authorId)
        {
                IEnumerable<DalPost> dal = _repository.Find(x => x.User.Id == authorId);
                if (dal.Count() == 0) return new List<Post>();
                IEnumerable<Post> bll = dal.Select(_mapper.GetBll);
                return bll;
        }

        public IEnumerable<Post> GetByTopic(long topicId)
        {
                IEnumerable<DalPost> dal = _repository.Find(x => x.Topic.Id==topicId).ToList();
                if (dal.Count() == 0) return new List<Post>();
                IEnumerable<Post> bll = dal.Select(_mapper.GetBll);
                return bll;
        }
    }
}
