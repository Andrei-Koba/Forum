using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;

namespace ForumWebApplication.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}