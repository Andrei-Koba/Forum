using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.DataServices;
using Bll.Interface.Entities;
using ForumWebApplication.Infrastructura.ModelMappers;
using ForumWebApplication.Models;

namespace ForumWebApplication.Controllers
{
    [Authorize(Roles = "Администратор, Модератор, Исполнитель")]
    public class UserController : Controller
    {
        private IUserService _users;
        private IRoleService _roles;
        private ViewUserMapper _mapper;

        private UserController(IUserService users, IRoleService roles)
        {
            _users = users;
            _roles = roles;
            _mapper = new ViewUserMapper(roles);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var users = _users.GetAll().Select(_mapper.GetEntityOne);
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            //SelectList roles = new SelectList(db.Roles, "Id", "Name");
            List<Role> roles = _roles.GetAll().ToList();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _users.Add(_mapper.GetEntityTwo(user));
                return RedirectToAction("Index");
            }
            List<Role> roles = _roles.GetAll().ToList();
            ViewBag.Roles = roles;

            return View(user);
        }

    }
}
