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
        private CustomRoleProvider _rp;

        public AdminController(IUserService users, IRoleService roles)
        {
            _users = users;
            _roles = roles;
            _userMapper = new ViewUserMapper(_roles);
            _rp = new CustomRoleProvider();
        }

        public ActionResult Index()
        {
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            IEnumerable<UserViewModel> users = _users.GetAll().Select(_userMapper.GetEntityOne);
            return View(users);
        }

        public ActionResult SetUser(long id)
        {
            User user = _users.GetById(id);
            Role role = _roles.GetByName("user");
            List<Role> roles = new List<Role>();
            roles.Add(role);
            user.Roles = roles;
            _users.SetUserRoles(user);
            return RedirectToAction("Index");
        }

        public ActionResult SetModer(long id)
        {
            User user = _users.GetById(id);
            Role role = _roles.GetByName("moderator");
            user.Roles.Add(role);
            _users.SetUserRoles(user);
            return RedirectToAction("Index");
        }

    }
}
