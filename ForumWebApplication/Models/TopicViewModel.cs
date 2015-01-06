using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using Interfaces;

namespace ForumWebApplication.Models
{
    public class TopicViewModel: IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public User Creator { get; set; }
        public int PostsCount { get; set; }
    }
}