using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApplication.Models
{
    public class TopicViewModel: IEntity
    {
        public long Id { get; set; }

        [Display(Name = "Название темы")]
        public string Name { get; set; }

        [Display(Name = "Дата создания темы")]
        public DateTime CreationDate { get; set; }

        public User Creator { get; set; }

        [Display(Name = "Количество постов")]
        public int PostsCount { get; set; }
    }
}