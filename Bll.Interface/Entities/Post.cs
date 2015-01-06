using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Bll.Interface.Entities
{
    public class Post: IEntity
    {
        public long Id { get; set; }
        public User Author { get; set; }
        public DateTime LastEdit { get; set; }
        public string Message { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public long ResponseTo { get; set; }
        public bool IsBlocked { get; set; }
        public Topic Topic { get; set; }
    }
}
