using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bll.Implementation;
using Bll.Interface.Entities;
using Dal.Interface.Entities;
using Dal.Implementation.Concrete;
using Bll.Implementation.EntityMappers;
using Dal.Interface.DataAccess;
using Dal.Interface;
using Bll.Interface;
using Bll.Interface.DataServices;

namespace Bll.Implementation.Services
{
    public class UserService : BaseService<DalUser, User, IEntityRepository<DalUser>, UserMapper>, IUserService
    {
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository repository, IUnitOfWork uow, IRoleRepository roles)
            : base(repository, uow)
        {
            _roleRepository = roles;
        }

        public User GetByLogin(string login)
        {
            DalUser dal = _repository.Find(x => x.Login == login).FirstOrDefault();
            if (dal == null) return null;
            return _mapper.GetBll(dal);
        }

        public void SetUserRoles(long userId, string[] roles)
        {
            if (roles == null) throw new ArgumentNullException("Roles is null");
            DalUser user = _repository.FindById(userId);
            if (user == null) return;
            List<DalRole> userRoles = _roleRepository.Find(r => roles.Contains(r.Name)).ToList();
            if (user.Roles == null)
            {
                user.Roles = new List<DalRole>();
            }
            user.Roles.Clear();
            foreach (var role in userRoles)
            {
                user.Roles.Add(role);
            }
            _repository.Edit(user);
            _unitOfWork.Commit();
        }
    }
}
