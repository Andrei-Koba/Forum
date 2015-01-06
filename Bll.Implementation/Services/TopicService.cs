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

        public TopicService(ITopicRepository repository, IEntityMapper<Topic, DalTopic> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual IEnumerable<Topic> GetAll()
        {
            IEnumerable<DalTopic> dal = _repository.GetAll().ToList();
            IEnumerable<Topic> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual Topic GetById(long id)
        {
            DalTopic dal = _repository.FindById(id);
            return _mapper.GetEntityOne(dal);
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
            _repository.Edit(_mapper.GetEntityTwo(entity));
            _repository.Save();
        }

        public virtual void Delete(Topic entity)
        {
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
    }
}
