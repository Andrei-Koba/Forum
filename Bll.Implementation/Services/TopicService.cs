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
    public class TopicService : BaseService<DalTopic, Topic, IEntityRepository<DalTopic>, TopicMapper>, ITopicService
    {
        public TopicService(ITopicRepository repository, IUnitOfWork uow)
            : base(repository, uow) { }

        public IEnumerable<Topic> GetByCreator(long creatorId)
        {
                IEnumerable<DalTopic> dal = _repository.Find(x => x.User.Id == creatorId);
                if (dal.Count() == 0) return new List<Topic>();
                IEnumerable<Topic> bll = dal.Select(_mapper.GetBll);
                return bll;
        }

        public Topic GetByName(string name)
        {
            DalTopic dal = _repository.Find(x => x.Name == name).FirstOrDefault();
            if (dal == null) return null;
            return _mapper.GetBll(dal);
        }
    }
}
