using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Implementation.EntityMappers;
using Bll.Interface.Entities;
using Bll.Interface.DataServices;
using Interfaces;
using ForumWebApplication.Models;

namespace ForumWebApplication.Infrastructura.ModelMappers
{
    public class ViewUserMapper: IEntityMapper<UserViewModel,User>
    {
        private IRoleService _roles;

        public ViewUserMapper(IRoleService roles)
        {
            _roles = roles;
        }

        public UserViewModel GetEntityOne(User dalEntity)
        {
            List<string> roles = new List<string>();
            foreach (var item in dalEntity.Roles)
	        {
		        roles.Add(item.Name);
	        }

            return new UserViewModel()
            {
                Id = dalEntity.Id,
                Name = dalEntity.Name,
                Avatar = dalEntity.Avatar,
                RegistrationDate = dalEntity.RegistrationDate,
                Login = dalEntity.Login,
                Mail = dalEntity.Mail,
                Roles = roles,
                Pass = dalEntity.Pass
            };
        }

        public User GetEntityTwo(UserViewModel bllEntity)
        {
            List<Role> roles = new List<Role>();
            foreach (var item in bllEntity.Roles)
	        {
		        roles.Add(_roles.GetByName(item));
	        }
            User result = new User()
            {
                Id = bllEntity.Id,
                Login = bllEntity.Login,
                Name = bllEntity.Name,
                Avatar = bllEntity.Avatar,
                RegistrationDate = bllEntity.RegistrationDate,
                Mail = bllEntity.Mail,
                Roles = roles,
                Pass = bllEntity.Pass
            };
            return result;
        }

        public void Dispose()
        {
            _roles.Dispose();
        }
    }
}