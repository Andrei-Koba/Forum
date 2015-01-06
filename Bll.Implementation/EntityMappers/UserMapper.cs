using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.Entities;
using Bll.Interface.Entities;
using Dal.Interface.DataAccess;

namespace Bll.Implementation.EntityMappers
{
    public class UserMapper: IEntityMapper<User,DalUser>
    {

        protected IRoleRepository _roles;
        protected IUserRoleRepository _userRoles;
        protected IEntityMapper<Role, DalRole> _roleMapper;

        public UserMapper(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IEntityMapper<Role,DalRole> roleMapper)
        {
            _roles = roleRepository;
            _userRoles = userRoleRepository;
            _roleMapper = roleMapper;

        }

        public User GetEntityOne(DalUser dalEntity)
        {
            List<DalUserRole> userRoles = _userRoles.Find(x => x.UserId == dalEntity.Id).ToList();
            List<DalRole> dalRoles = new List<DalRole>();
            foreach (var item in userRoles)
	        {
		        dalRoles.Add(_roles.FindById(item.RoleId));
	        }
            List<Role> roles = new List<Role>();
            foreach (var item in dalRoles)
            {
                roles.Add(_roleMapper.GetEntityOne(item));
            }

            return new User()
            {
                Id = dalEntity.Id,
                Avatar = dalEntity.Avatar,
                Login = dalEntity.Login,
                Name = dalEntity.Name,
                Mail = dalEntity.Mail,
                Roles = roles,
                RegistrationDate = dalEntity.RegistrationDate,
                Pass = dalEntity.Pass
            };
        }

        public DalUser GetEntityTwo(User bllEntity)
        {
            return new DalUser()
            {
                Id = bllEntity.Id,
                Name = bllEntity.Name,
                Avatar = bllEntity.Avatar,
                Pass = bllEntity.Pass,
                Mail = bllEntity.Mail,
                RegistrationDate = bllEntity.RegistrationDate,
                Login = bllEntity.Login
            };
        }

        public void Dispose()
        {
            _roleMapper.Dispose();
            _userRoles.Dispose();
            _roles.Dispose();
        }
    }
}
