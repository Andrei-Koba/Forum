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
    public class PostService: IPostService
    {
        protected readonly IPostRepository _repository;
        protected readonly IEntityMapper<Post, DalPost> _mapper;
        protected readonly ITopicService _topics;

        public PostService(IPostRepository repository, IEntityMapper<Post, DalPost> mapper, ITopicService topics)
        {
            _repository = repository;
            _mapper = mapper;
            _topics = topics;

        }


        public virtual IEnumerable<Post> GetAll()
        {
            IEnumerable<DalPost> dal = _repository.GetAll().ToList();
            IEnumerable<Post> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual IEnumerable<Post> GetByAuthor(User author)
        {
            IEnumerable<DalPost> dal = _repository.Find(x => x.AuthorId == author.Id).ToList();
            IEnumerable<Post> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual IEnumerable<Post> GetByTopic(Topic topic)
        {
            IEnumerable<DalPost> dal = _repository.Find(x => x.TopicId == topic.Id).ToList();
            IEnumerable<Post> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual IEnumerable<Post> GetBlocked()
        {
            IEnumerable<DalPost> dal = _repository.Find(x => x.IsBlocked == true).ToList();
            IEnumerable<Post> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual IEnumerable<Post> GetNotBlocked()
        {
            IEnumerable<DalPost> dal = _repository.Find(x => x.IsBlocked == false).ToList();
            IEnumerable<Post> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual IEnumerable<Post> GetAllResponse(Post post)
        {
            IEnumerable<DalPost> dal = _repository.Find(x => x.ResponseTo == post.Id).ToList();
            IEnumerable<Post> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual Post GetById(long id)
        {
            DalPost dal = _repository.FindById(id);
            return _mapper.GetEntityOne(dal);
        }

        public virtual void Add(Post entity)
        {
            _repository.Add(_mapper.GetEntityTwo(entity));
            entity.Topic.PostCount++;
            _topics.Edit(entity.Topic);
            _repository.Save();
        }

        public virtual void Edit(Post entity)
        {
            _repository.Edit(_mapper.GetEntityTwo(entity));
            _repository.Save();
        }

        public virtual void Delete(Post entity)
        {
            entity.Topic.PostCount--;
            _topics.Edit(entity.Topic);
            _repository.Delete(_mapper.GetEntityTwo(entity));
            _repository.Save();
        }

        public virtual void Save()
        {
            _repository.Save();
        }

        public void Dispose()
        {
            _repository.Dispose();
            _mapper.Dispose();
        }

        public IEnumerable<Post> Find(Expression<Func<Post, bool>> predicates)
        {
            IEnumerable<DalPost> dals = _repository.Find(x => predicates.Compile().Invoke(_mapper.GetEntityOne(x)));
            IEnumerable<Post> blls = dals.Select(_mapper.GetEntityOne);
            return blls;
        }
    }
}
