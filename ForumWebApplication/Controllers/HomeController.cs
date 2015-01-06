using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.DataServices;
using Bll.Interface.Entities;
using ForumWebApplication.Models;
using ForumWebApplication.Infrastructura.ModelMappers;

namespace ForumWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private ITopicService _topics;
        private ViewTopicMapper _topicMapper;

        public HomeController(ITopicService topics, ViewTopicMapper topicMapper)
        {
            _topics = topics;
            _topicMapper = topicMapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Topics()
        {
            List<TopicViewModel> topics = _topics.GetAll().Select(_topicMapper.GetEntityOne).ToList();
            return View(topics); 
        }

    }
}
