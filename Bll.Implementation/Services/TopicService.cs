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
using Dal.Interface;
using Bll.Interface.DataServices;
using Bll.Interface;
using Dal.Interface.DataAccess;
using System.Linq.Expressions;

namespace Bll.Implementation.Services
{
    public class TopicService: ITopicService
    {
        protected readonly ITopicRepository _repository;
        protected readonly IEntityMapper<Topic, DalTopic> _mapper;
        protected readonly IPostRepository _posts;

        public TopicService(ITopicRepository repository, IEntityMapper<Topic, DalTopic> mapper, IPostRepository posts)
        {
            _repository = repository;
            _mapper = mapper;
            _posts = posts;
        }

        public virtual IEnumerable<Topic> GetAll()
        {
            IEnumerable<DalTopic> dal = _repository.GetAll().ToList();
            IEnumerable<Topic> bll = dal.Select(GetBll).ToList();
            return bll;
        }

        public virtual Topic GetById(long id)
        {
            DalTopic dal = _repository.FindById(id);
            return GetBll(dal);
        }

        public virtual Topic GetByName(string name)
        {
            DalTopic dal = _repository.Find(x => x.Name == name).Single();
            return _mapper.GetEntityOne(dal);
        }

        public virtual IEnumerable<Topic> GetByCreator(User creator)
        {
            IEnumerable<DalTopic> dal = _repository.Find(x => x.CreatorId == creator.Id).ToList();
            IEnumerable<Topic> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual void Add(Topic entity)
        {
            _repository.Add(_mapper.GetEntityTwo(entity));
            _repository.Save();
        }

        public virtual void Edit(Topic entity)
        {
            DalTopic dal = GetDal(entity);
            _repository.Edit(dal);
            _repository.Save();
        }

        public virtual void Delete(Topic entity)
        {
            _repository.Delete(_mapper.GetEntityTwo(entity));
            IEnumerable<DalPost> topicPosts = _posts.Find(x => x.TopicId == entity.Id);
            foreach (var item in topicPosts)
            {
                _posts.Delete(item);
            }
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

        public IEnumerable<Topic> Find(Expression<Func<Topic, bool>> predicates)
        {
            IEnumerable<DalTopic> dals = _repository.Find(x => predicates.Compile().Invoke(_mapper.GetEntityOne(x)));
            List<Topic> blls = new List<Topic>();
            foreach (var item in dals)
	        {
		        blls.Add(_mapper.GetEntityOne(item));
	        }
            return blls;
        }

        public IEnumerable<Topic> GetMostPopular(int count)
        {
            DalTopic[] dals = _repository.GetAll().OrderByDescending(x => x.PostsCount).ToArray();
            List<Topic> blls = new List<Topic>();
            int col = count;
            if (dals.Length < count) col = dals.Length;
            for (int i = 0; i < col; i++)
            {
                blls.Add(_mapper.GetEntityOne(dals[i]));
            }
            return blls;
        }

        protected DalTopic GetDal(Topic entity)
        {
            var res = _mapper.GetEntityTwo(entity);
            return res;
        }
        protected Topic GetBll(DalTopic entity)
        {
            var res = _mapper.GetEntityOne(entity);
            return res;
        }
    }
}
