using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.DataServices;
using Bll.Interface.Entities;
using ForumWebApplication.Models;
using ForumWebApplication.Infrastructura.ModelMappers;
using ForumWebApplication.Providers;

namespace ForumWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private ITopicService _topics;
        private ViewTopicMapper _topicMapper;
        private IUserService _users;
        private ViewPostMapper _postMapper;
        private IPostService _posts;
        private CustomRoleProvider _rp;

        public HomeController(ITopicService topics, ViewTopicMapper topicMapper, IUserService users, IPostService posts)
        {
            _posts = posts;
            _topics = topics;
            _topicMapper = topicMapper;
            _users = users;
            _postMapper = new ViewPostMapper(_posts);
            _rp = new CustomRoleProvider();
        }

        public ActionResult Index()
        {
            IEnumerable<Topic> topics = _topics.GetAll();
            List<Topic> latest = topics.Where(x => (DateTime.Now - x.CreationDate).Days <= 0).ToList();
            IEnumerable<Topic> popular = _topics.GetMostPopular(3);
            IEnumerable<TopicViewModel> viewLatest = latest.Select(_topicMapper.GetEntityOne);
            IEnumerable<TopicViewModel> viewPopular = popular.Select(_topicMapper.GetEntityOne);
            ViewBag.Latest = viewLatest;
            ViewBag.Popular = viewPopular;
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name,"admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View();
        }

        public ActionResult Topics()
        {
            List<TopicViewModel> topics = _topics.GetAll().Select(_topicMapper.GetEntityOne).ToList();
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View(topics); 
        }

        [Authorize]
        public ActionResult UserTopics()
        {
            string login = User.Identity.Name;
            User user = _users.GetByLogin(login);
            IEnumerable<Topic> userTopics = _topics.GetByCreator(user);
            IEnumerable<TopicViewModel> viewUserTopics = userTopics.Select(_topicMapper.GetEntityOne);
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View(viewUserTopics);
        }

        [Authorize]
        public ActionResult CreateTopic()
        {
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            TopicViewModel topic = new TopicViewModel();
            return View(topic);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTopic(TopicViewModel topic)
        {
            topic.CreationDate = DateTime.Now;
            string login = User.Identity.Name;
            User user = _users.GetByLogin(login);
            topic.Creator = user;
            topic.PostsCount = 0;
            Topic bll = _topicMapper.GetEntityTwo(topic);
            _topics.Add(bll);
            Topic res = _topics.GetByName(bll.Name);
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return RedirectToAction("TopicPosts",res);
        }


        public ActionResult TopicPosts(long Id, int page = 1)
        {
            Topic topic = _topics.GetById(Id);
            IEnumerable<ViewPost> posts = _posts.GetByTopic(topic).Select(_postMapper.GetEntityOne);
            TopicPostsModel res = new TopicPostsModel(posts, page, 4);
            res.Id = topic.Id;
            res.CreationDate = topic.CreationDate;
            res.TopicCreator = topic.Creator;
            res.TopicName = topic.Name;
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View(res);
        }



    }
}
