using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.Entities;
using Bll.Interface.Entities;
using Dal.Interface.DataAccess;

namespace Bll.Implementation.EntityMappers
{
    public class TopicMapper : IEntityMapper<Topic, DalTopic>
    {

        private readonly IEntityMapper<User, DalUser> _userMapper;

        public TopicMapper()
        {
            _userMapper = new UserMapper();
        }

        public Topic GetBll(DalTopic dalEntity)
        {
            Topic bll = new Topic();
            bll.CreationDate = dalEntity.CreationDate;
            bll.Creator = _userMapper.GetBll(dalEntity.User);
            bll.Id = dalEntity.Id;
            bll.Name = dalEntity.Name;
            return bll;
        }

        public DalTopic GetDal(Topic bllEntity)
        {
            DalTopic dal = new DalTopic();
            dal.CreationDate = bllEntity.CreationDate;
            dal.UserId = bllEntity.Creator.Id;
            dal.Id = bllEntity.Id;
            dal.Name = bllEntity.Name;
            return dal;
        }
    }
}
