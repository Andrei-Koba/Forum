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
    public class TopicMapper: IEntityMapper<Topic,DalTopic>
    {

        protected IEntityRepository<DalUser> _users;
        protected IEntityMapper<User, DalUser> _userMapper;

        public TopicMapper(IUserRepository userRepository, IEntityMapper<User,DalUser> userMapper)
        {
            _users = userRepository;
            _userMapper = userMapper;
        }

        public Topic GetEntityOne(DalTopic dalEntity)
        {
            DalUser dalUser = _users.FindById(dalEntity.CreatorId);
            User user = _userMapper.GetEntityOne(dalUser);
            return new Topic()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                CreationDate = dalEntity.CreationDate,
                Creator = user
            };
        }

        public DalTopic GetEntityTwo(Topic bllEntity)
        {
            DalUser user = _users.FindById(bllEntity.Creator.Id);
            return new DalTopic()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                CreatorId = user.Id,
                CreationDate = bllEntity.CreationDate
            };
        }

        public void Dispose()
        {
            _users.Dispose();
            _userMapper.Dispose();
        }
    }
}
