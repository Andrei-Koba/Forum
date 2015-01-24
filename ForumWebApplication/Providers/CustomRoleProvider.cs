using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ninject;
using Ninject.Web.Common;
using Bll.Interface.Entities;
using Bll.Interface.DataServices;
using ForumWebApplication.App_Start;

namespace ForumWebApplication.Providers
{
    public class CustomRoleProvider: RoleProvider
    {

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public CustomRoleProvider()
        {
            _userService = NinjectWebCommon.GetKernel().Get<IUserService>();
            _roleService = NinjectWebCommon.GetKernel().Get<IRoleService>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            try
            {
                User user = _userService.GetByLogin(username);
                foreach (var item in user.Roles)
                {
                    if (item.Name == roleName) return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public override string[] GetRolesForUser(string login)
        {
                User user = _userService.GetByLogin(login);
                List<string> roles = new List<string>();
                foreach (var item in user.Roles)
            	{
                    roles.Add(item.Name);
	            }
                return roles.ToArray();
        }

        public override void CreateRole(string roleName)
        {
            Role existRole = _roleService.GetByName(roleName);
            if (existRole != null)
            {
                throw new ArgumentException("Role with that name already exist.");
            }
            Role newRole = new Role() {Name = roleName };
            _roleService.Add(newRole);
        }


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}