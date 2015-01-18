using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApplication.Models
{
    public class TopicPostsModel: IEntity
    {
        public long Id { get; set; }
        public string TopicName { get; set; }
        public User TopicCreator { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<ViewPost> List { get; set; }
        public int PageNo { get; set; }
        public int CountPage { get; set; }
        public int ItemPerPage { get; set; }

        public TopicPostsModel(IEnumerable<ViewPost> list, int page, int itemPerPage)
        {
            if (itemPerPage == 0)
            {
                itemPerPage = 5;
            }
            ItemPerPage = itemPerPage;

            PageNo = page;
            var count = list.Count();
            CountPage = (int)decimal.Remainder(count, itemPerPage) == 0 ? count / itemPerPage : count / itemPerPage + 1;
            List = list.Skip((PageNo - 1) * itemPerPage).Take(itemPerPage);
        }
    }
}