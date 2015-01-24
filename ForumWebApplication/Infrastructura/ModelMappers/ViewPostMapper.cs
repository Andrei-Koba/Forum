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
    public class ViewPostMapper: IEntityMapper<ViewPost, Post>
    {

        protected readonly IPostService _posts;

        public ViewPostMapper(IPostService posts)
        {
            _posts = posts;
        }

        public ViewPost GetBll(Post dalEntity)
        {
            Post response = null;
            if (dalEntity.ResponseTo != 0)
            {
                response = _posts.GetById(dalEntity.ResponseTo);
            }
            return new ViewPost()
            {
                Id = dalEntity.Id,
                Dislikes= dalEntity.Dislikes,
                Likes =dalEntity.Likes,
                Message = dalEntity.Message,
                Topic = dalEntity.Topic,
                LastEdit = dalEntity.LastEdit,
                Author = dalEntity.Author,
                IsBlocked = dalEntity.IsBlocked,
                ResponseTo = response
            };
        }

        public Post GetDal(ViewPost bllEntity)
        {
            long resp = 0;
            if (bllEntity.ResponseTo != null)
            {
                resp = bllEntity.ResponseTo.Id;
            }
            return new Post() 
            {
                Id = bllEntity.Id,
                Author = bllEntity.Author,
                Dislikes = bllEntity.Dislikes,
                Likes = bllEntity.Likes,
                IsBlocked = bllEntity.IsBlocked,
                LastEdit = bllEntity.LastEdit,
                Message = bllEntity.Message,
                Topic = bllEntity.Topic,
                ResponseTo = resp
            };
        }

     
    }
}