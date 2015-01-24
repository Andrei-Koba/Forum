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

        public HomeController(ITopicService topics, ViewTopicMapper topicMapper, IUserService users, IPostService posts)
        {
            _posts = posts;
            _topics = topics;
            _topicMapper = topicMapper;
            _users = users;
            _postMapper = new ViewPostMapper(_posts);
        }

        public ActionResult Index()
        {
            IEnumerable<Topic> topics = _topics.GetAll().ToList();
            List<Topic> latest = topics.Where(x => (DateTime.Now - x.CreationDate).Days <= 0).ToList();
            IEnumerable<TopicViewModel> viewLatest = latest.Select(_topicMapper.GetBll).ToList();
            ViewBag.Latest = viewLatest;
            if (Request.IsAuthenticated)
            {
                User user = _users.GetByLogin(User.Identity.Name);
                IEnumerable<Topic> userTopics = _topics.GetByCreator(user.Id);
                List<TopicViewModel> viewUserTopics = new List<TopicViewModel>();
                foreach (var item in userTopics)
                {
                    TopicViewModel m = _topicMapper.GetBll(item);
                    viewUserTopics.Add(m);
                }
                ViewBag.UserTopics = viewUserTopics;
            }
            return View();
        }

        public ActionResult Topics()
        {
            List<TopicViewModel> topics = _topics.GetAll().Select(_topicMapper.GetBll).ToList();
            return View(topics); 
        }

        [Authorize]
        public ActionResult UserTopics()
        {
            string login = User.Identity.Name;
            User user = _users.GetByLogin(login);
            IEnumerable<Topic> userTopics = _topics.GetByCreator(user.Id);
            IEnumerable<TopicViewModel> viewUserTopics = userTopics.Select(_topicMapper.GetBll);
            return View(viewUserTopics);
        }

        [Authorize]
        public ActionResult CreateTopicForm()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTopic()
        {
            TopicViewModel topic = new TopicViewModel();
            topic.Name = Request.Params["TopicName"];
            topic.CreationDate = DateTime.Now;
            string login = User.Identity.Name;
            User user = _users.GetByLogin(login);
            topic.Creator = user;
            topic.PostsCount = 0;
            Topic bll = _topicMapper.GetDal(topic);
            _topics.Add(bll);
            Topic res = _topics.GetByName(bll.Name);
            return RedirectToAction("TopicPosts",res);
        }


        public ActionResult TopicPosts(long Id, int page = 1)
        {
            Topic topic = _topics.GetById(Id);
            IEnumerable<ViewPost> posts = _posts.GetByTopic(topic.Id).Select(_postMapper.GetBll).ToList();
            TopicPostsModel res = new TopicPostsModel(posts, page, 4);
            res.Id = topic.Id;
            res.CreationDate = topic.CreationDate;
            res.TopicCreator = topic.Creator;
            res.TopicName = topic.Name;
            return View(res);
        }



    }
}
