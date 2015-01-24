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
    public class PostMapper : IEntityMapper<Post, DalPost>
    {

        private readonly IEntityMapper<User, DalUser> _userMapper;
        private readonly IEntityMapper<Topic, DalTopic> _topicMapper;

        public PostMapper()
        {
            _userMapper = new UserMapper();
            _topicMapper = new TopicMapper();
        }

        public Post GetBll(DalPost dalEntity)
        {
            Post bll = new Post();
            bll.Author = _userMapper.GetBll(dalEntity.User);
            bll.Dislikes = dalEntity.Dislikes;
            bll.Id = dalEntity.Id;
            bll.IsBlocked = dalEntity.IsBlocked;
            bll.LastEdit = dalEntity.LastEdit;
            bll.Likes = dalEntity.Likes;
            bll.Message = dalEntity.Message;
            bll.ResponseTo = dalEntity.ResponseTo;
            bll.Topic = _topicMapper.GetBll(dalEntity.Topic);
            return bll;
        }

        public DalPost GetDal(Post bllEntity)
        {
            DalPost dal = new DalPost();
            dal.UserId = bllEntity.Author.Id;
            dal.Dislikes = bllEntity.Dislikes;
            dal.Id = bllEntity.Id;
            dal.IsBlocked = bllEntity.IsBlocked;
            dal.LastEdit = bllEntity.LastEdit;
            dal.Likes = bllEntity.Likes;
            dal.Message = bllEntity.Message;
            dal.ResponseTo = bllEntity.ResponseTo;
            dal.TopicId = bllEntity.Topic.Id;
            return dal;
        }
    }
}
