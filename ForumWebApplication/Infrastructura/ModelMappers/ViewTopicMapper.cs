using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Implementation.EntityMappers;
using Bll.Interface.Entities;
using Bll.Interface.DataServices;
using Interfaces;
using ForumWebApplication.Models;

namespace ForumWebApplication.Infrastructura.ModelMappers
{
    public class ViewTopicMapper : IEntityMapper<TopicViewModel,Topic>
    {
        private IPostService _posts;

        public ViewTopicMapper(IPostService posts)
        {
            _posts = posts;
        }

        public TopicViewModel GetBll(Topic dalEntity)
        {
            List<Post> topicPosts = _posts.GetByTopic(dalEntity.Id).ToList();
            return new TopicViewModel() 
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                CreationDate = dalEntity.CreationDate,
                Creator = dalEntity.Creator,
                PostsCount = topicPosts.Count
            };
        }

        public Topic GetDal(TopicViewModel bllEntity)
        {
            return new Topic()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                Creator = bllEntity.Creator,
                CreationDate = bllEntity.CreationDate,
            };
        }
    }
}