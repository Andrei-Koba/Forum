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
    public class PostMapper: IEntityMapper<Post,DalPost>
    {

        protected IEntityRepository<DalTopic> _topics;
        protected IEntityRepository<DalUser> _users;
        protected IEntityMapper<User, DalUser> _userMapper;
        protected IEntityMapper<Topic, DalTopic> _topicMapper;

        public PostMapper(ITopicRepository topicRepository, IUserRepository userRepository,
            IEntityMapper<User,DalUser> userMapper, IEntityMapper<Topic, DalTopic> topicMapper)
        {
            _topics = topicRepository;
            _users = userRepository;
            _userMapper = userMapper;
            _topicMapper = topicMapper;

        }

        public Post GetEntityOne(DalPost dalEntity)
        {
           DalTopic dalTopic = _topics.FindById(dalEntity.TopicId);
           Topic topic = _topicMapper.GetEntityOne(dalTopic);
           DalUser dalUser = _users.FindById(dalEntity.AuthorId);
           User user = _userMapper.GetEntityOne(dalUser);
            return new Post()
            {
                Id = dalEntity.Id,
                Topic = topic,
                ResponseTo = dalEntity.ResponseTo,
                Author = user,
                Likes = dalEntity.Likes,
                Dislikes = dalEntity.Dislikes,
                IsBlocked = dalEntity.IsBlocked,
                LastEdit = dalEntity.LastEdit,
                Message = dalEntity.Message

            };
        }

        public DalPost GetEntityTwo(Post bllEntity)
        {
            DalUser author = _users.Find(x => x.Name==bllEntity.Author.Name).Single();
            DalTopic topic = _topics.Find(x => x.Name==bllEntity.Topic.Name).Single();
            return new DalPost()
            {
                Id = bllEntity.Id,
                Message = bllEntity.Message,
                IsBlocked = bllEntity.IsBlocked,
                Likes = bllEntity.Likes,
                Dislikes = bllEntity.Dislikes,
                LastEdit = bllEntity.LastEdit,
                ResponseTo =  bllEntity.ResponseTo,
                AuthorId = author.Id,
                TopicId = topic.Id
            };
        }


        public void Dispose()
        {
            _topics.Dispose();
            _users.Dispose();
            _userMapper.Dispose();
            _topicMapper.Dispose();
        }
    }
}
