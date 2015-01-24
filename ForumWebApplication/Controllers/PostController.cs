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
using System.Text;

namespace ForumWebApplication.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/
        private ITopicService _topics;
        private IPostService _posts;
        private IUserService _users;

        public PostController(ITopicService topics, IPostService posts, IUserService users)
        {
            _topics = topics;
            _posts = posts;
            _users = users;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create()
        {
            Post post = new Post();
            post.Dislikes = 0;
            post.Likes = 0;
            string login = User.Identity.Name;
            User user = _users.GetByLogin(login);
            post.Author = user;
            post.LastEdit = DateTime.Now;
            post.IsBlocked = false;
            long topicId = long.Parse(Request.Params["Id"]);
            Topic topic = _topics.GetById(topicId);
            post.Topic = topic;
            post.Message = Request.Params["message"];
            post.ResponseTo = 0;
            _posts.Add(post);
            return RedirectToAction("TopicPosts", "Home", topic);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void Block(string strPostId)
        {
            long postId = long.Parse(strPostId);
            Post post = _posts.GetById(postId);
            post.IsBlocked = true;
            _posts.Edit(post);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateResponse(string topicId, string postId)
        {
            long tId = long.Parse(topicId);
            long pId = long.Parse(postId);
            if (Request.IsAjaxRequest())
            {
                ViewBag.Topic = tId;
                ViewBag.Post = pId;
                return PartialView();
            }
            Topic topic = _topics.GetById(tId);
            return RedirectToAction("TopicPosts", "Home", topic);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CommitResponse(long topicId, long postId)
        {
            Post post = new Post();
            string login = User.Identity.Name;
            User user = _users.GetByLogin(login);
            post.Author = user;
            post.Dislikes = 0;
            post.IsBlocked = false;
            post.LastEdit = DateTime.Now;
            post.Likes = 0;
            post.Message = Request.Params["message"];
            post.ResponseTo = postId;
            Topic topic = _topics.GetById(topicId);
            post.Topic = topic;
            _posts.Add(post);
            return RedirectToAction("TopicPosts", "Home", topic);
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Like(string strId)
        {
            int count = strId.Count();
            int postId = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 2; i < count; i++)
            {
                sb.Append(strId[i]);
            }


            postId = int.Parse(sb.ToString());
            Post post = _posts.GetById(postId);
            post.Likes++;
            _posts.Edit(post);
            if (Request.IsAjaxRequest())
            {
                return new JsonResult()
                {
                    Data = post.Likes,
                };
            }
            return View();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Dislike(string strId)
        {
            int count = strId.Count();
            int postId = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 2; i < count; i++)
            {
                sb.Append(strId[i]);
            }
            postId = int.Parse(sb.ToString());
            Post post = _posts.GetById(postId);
            post.Dislikes++;
            _posts.Edit(post);
            if (Request.IsAjaxRequest())
            {
                return new JsonResult()
                {
                    Data = post.Dislikes,
                };
            }
            return View();
        }

    }
}
