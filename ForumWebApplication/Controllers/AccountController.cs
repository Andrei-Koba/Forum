using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
using ForumWebApplication.Models;
using Bll.Interface.DataServices;
using Bll.Interface.Entities;
using ForumWebApplication.Infrastructura.ModelMappers;
using ForumWebApplication.Providers;
using System.Web.Helpers;

namespace ForumWebApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private ViewUserMapper _mapper;
        private IRoleService _roles;
        private CustomRoleProvider _rp;

        public AccountController(IUserService userService, IRoleService roles)
        {
            _userService = userService;
            _roles = roles;
            _mapper = new ViewUserMapper(_roles);
            _rp = new CustomRoleProvider();
        }

        public ActionResult Login()
        {
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View();

        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index","Home");
                        //return RedirectToAction("Index", "Request");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            RegisterViewModel reg = new RegisterViewModel();
            reg.AddedDate = DateTime.Now;
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View(reg);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel regModel)
        {
            //if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            //{
            //    ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            //    return View(viewModel);
            //}
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            var anyUser = _userService.GetAll().Any(u => u.Mail.Contains(regModel.Email));
            if (anyUser)
            {
                ModelState.AddModelError("Email", "Пользователь с таким адресом уже зарегистрирован");
                return View(regModel);
            }

            var anyUser1 = _userService.GetAll().Any( u => u.Login==regModel.Login);
            if (anyUser1)
            {
                ModelState.AddModelError("Login", "Пользователь с таким логином уже зарегистрирован");
                return View(regModel);
            }


            if (ModelState.IsValid)
            {
                
                UserViewModel viewModel = new UserViewModel()
                {
                    Name = regModel.Name,
                    Login = regModel.Login,
                    Pass = regModel.Password,
                    RegistrationDate = DateTime.Now,
                    Avatar = "image.png",
                    Mail = regModel.Email
                };
                CustomMembershipProvider provider = new CustomMembershipProvider();
                MembershipUser mUser = provider.CreateUser(viewModel);
                if (mUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
                List<Role> roles = new List<Role>();
                roles.Add(_roles.GetByName("user"));
                Bll.Interface.Entities.User user = new User()
                {
                    Login = viewModel.Login,
                    Name = viewModel.Name,
                    Pass = viewModel.Pass,
                    Mail = viewModel.Mail,
                    RegistrationDate = DateTime.Now,
                    Avatar = viewModel.Avatar,
                    Roles = roles
                };
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(regModel); 
            }
        }

        [Authorize]
        public ActionResult Info()
        {
            string name = User.Identity.Name;
            Bll.Interface.Entities.User user = _userService.GetByLogin(name);
            UserViewModel viewUser = _mapper.GetEntityOne(user);
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            return View(viewUser);
        }

        [HttpPost]
        public ActionResult Info(HttpPostedFileBase imgFile)
        {
            string strId = Request.Params["Id"];
            long userId = long.Parse(strId);
            User user = _userService.GetById(userId);
            user.Name = Request.Params["Name"];
            user.Mail = Request.Params["Mail"];
            if (imgFile != null)
            {
                string fileName = user.Id.ToString() + imgFile.FileName.Substring(imgFile.FileName.LastIndexOf('.'));
                imgFile.SaveAs(Server.MapPath(WebConfigurationManager.AppSettings["Avatars"]) + fileName);
                user.Avatar = fileName;
            }
            _userService.Edit(user);
            UserViewModel viewUser = _mapper.GetEntityOne(user);
            ViewBag.Admin = _rp.IsUserInRole(User.Identity.Name, "admin");
            ViewBag.Moder = _rp.IsUserInRole(User.Identity.Name, "moderator");
            System.Threading.Thread.Sleep(500); // Чтобы сетвер успел загрузить  картинку
            return View(viewUser);
        }


        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
            try
            {
                User user = _userService.GetByLogin(login);
                if (user != null)
                {
                    if(Crypto.VerifyHashedPassword(user.Pass,password)) isValid = true;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
