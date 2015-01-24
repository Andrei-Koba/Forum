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
    public class UserMapper : IEntityMapper<User, DalUser>
    {

        private IEntityMapper<Role, DalRole> _roleMapper;

        public UserMapper()
        {
            _roleMapper = new RoleMapper();
        }

        public User GetBll(DalUser dalEntity)
        {
            User bll = new User();
            bll.Id = dalEntity.Id;
            bll.Login = dalEntity.Login;
            bll.Mail = dalEntity.Mail;
            bll.Pass = dalEntity.Pass;
            bll.RegistrationDate = dalEntity.RegistrationDate;
            bll.Name = dalEntity.Name;
            bll.Avatar = dalEntity.Avatar;
            if (dalEntity.Roles != null)
            {
                bll.Roles = dalEntity.Roles.Select(_roleMapper.GetBll);
            }
            return bll;

        }

        public DalUser GetDal(User bllEntity)
        {
            
            DalUser dal = new DalUser();
            dal.Avatar = bllEntity.Avatar;
            dal.Id = bllEntity.Id;
            dal.Login = bllEntity.Login;
            dal.Mail = bllEntity.Mail;
            dal.Name = bllEntity.Name;
            dal.Pass = bllEntity.Pass;
            dal.RegistrationDate = bllEntity.RegistrationDate;
            return dal;
        }
    }
}
