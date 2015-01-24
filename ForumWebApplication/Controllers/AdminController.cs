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

    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private IUserService _users;
        private ViewUserMapper _userMapper;
        private IRoleService _roles;

        public AdminController(IUserService users, IRoleService roles)
        {
            _users = users;
            _roles = roles;
            _userMapper = new ViewUserMapper(_roles);
        }

        public ActionResult Index()
        {
            IEnumerable<UserViewModel> users = _users.GetAll().Select(_userMapper.GetBll);
            return View(users);
        }

        public ActionResult SetUser(long id)
        {
            User user = _users.GetById(id);
            string[] roles = new string[1];
            roles[0] = "user";
            _users.SetUserRoles(user.Id, roles);
            return RedirectToAction("Index");
        }

        public ActionResult SetModer(long id)
        {
            User user = _users.GetById(id);
            string[] roles = new string[2];
            roles[0] = "user";
            roles[1] = "moderator";
            _users.SetUserRoles(user.Id, roles);
            return RedirectToAction("Index");
        }

    }
}
