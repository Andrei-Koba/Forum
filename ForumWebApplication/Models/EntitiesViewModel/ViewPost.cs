using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApplication.Models
{
    public class ViewPost: IEntity
    {
        public long Id { get; set; }

        [Display(Name = "Автор")]
        public User Author { get; set; }

        [Display(Name = "Дата последнего редактирования")]
        public DateTime LastEdit { get; set; }

        [Display(Name = "Сообщение")]
        public string Message { get; set; }

        [Display(Name = "Понравилось")]
        public int Likes { get; set; }

        [Display(Name = "Не понравилось")]
        public int Dislikes { get; set; }

        public bool IsBlocked { get; set; }
        public Post ResponseTo { get; set; }
        public Topic Topic { get; set; }

    }
}