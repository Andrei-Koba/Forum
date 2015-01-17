using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using Interfaces;

namespace ForumWebApplication.Models
{
    public class ViewPost: IEntity
    {
        public long Id { get; set; }
        public User Author { get; set; }
        public DateTime LastEdit { get; set; }
        public string Message { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool IsBlocked { get; set; }
        public Post ResponseTo { get; set; }
        public Topic Topic { get; set; }

    }
}