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
    public class UserService: IUserService
    {
        protected readonly IUserRepository _repository;
        protected readonly IEntityMapper<User, DalUser> _mapper;
        protected readonly IUserRoleRepository _userRoles;
        protected readonly IRoleRepository _roles;

        public UserService(IUserRepository repository, IEntityMapper<User, DalUser> mapper, IUserRoleRepository userRoles, IRoleRepository roles)
        {
            _repository = repository;
            _mapper = mapper;
            _userRoles = userRoles;
            _roles = roles;
        }

        public virtual IEnumerable<User> GetAll()
        {
            IEnumerable<DalUser> dal = _repository.GetAll().ToList();
            IEnumerable<User> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual User GetById(long id)
        {
            DalUser dal = _repository.FindById(id);
            return _mapper.GetEntityOne(dal);
        }

        public virtual IEnumerable<User> GetByName(string name)
        {
            IEnumerable<DalUser> dal = _repository.Find(x => x.Name == name).ToList();
            IEnumerable<User> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual User GetByLogin(string login)
        {
            try
            {
                DalUser dal = _repository.Find(x => x.Login == login).Single();
                User bll = _mapper.GetEntityOne(dal);
                return bll;
            }
            catch
            {
                return null;
            }
        }

        //public virtual IEnumerable<User> GetByRole(Role role)
        //{
        //    IEnumerable<DalUser> dal = _repository.Find(x => x.RoleId == role.Id).ToList();
        //    IEnumerable<User> bll = dal.Select(_mapper.GetBllEntity).ToList();
        //    return bll;
        //}

        public virtual void Add(User entity)
        {
            _repository.Add(_mapper.GetEntityTwo(entity));
            _repository.Save();
            DalUser user = _repository.Find(x => x.Login == entity.Login).FirstOrDefault();
            DalUserRole userRole = new DalUserRole()
            {
                RoleId = _roles.Find(x => x.Name == "user").FirstOrDefault().Id,
                UserId = user.Id
            };
            _userRoles.Add(userRole);
            _repository.Save();
        }

        public virtual void Edit(User entity)
        {
            DalUser dal = _mapper.GetEntityTwo(entity);
            _repository.Edit(dal);
            _repository.Save();
        }

        public virtual void Delete(User entity)
        {
            _repository.Delete(_mapper.GetEntityTwo(entity));
            IEnumerable<DalUserRole> userRoles = _userRoles.Find(x => x.UserId == entity.Id);
            foreach (var item in userRoles)
            {
                _userRoles.Delete(item); 
            }
            _repository.Save();
        }

        public virtual void Save()
        {
            _repository.Save();
        }

        public void Dispose()
        {
            _repository.Dispose();
            _mapper.Dispose();

        }

        public void SetUserRoles(User user)
        {
            List<DalUserRole> userRoles = _userRoles.Find(x => x.UserId == user.Id).ToList();
            foreach (var item in userRoles)
            {
                _userRoles.Delete(item);
            }
            _userRoles.Save();
            foreach (var item in user.Roles)
            {
                DalUserRole userRole = new DalUserRole()
                {
                    RoleId = item.Id,
                    UserId = user.Id
                };
                _userRoles.Add(userRole);
            }
            _userRoles.Save();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicates)
        {
            IEnumerable<DalUser> dals = _repository.Find(x => predicates.Compile().Invoke(_mapper.GetEntityOne(x)));
            IEnumerable<User> blls = dals.Select(_mapper.GetEntityOne);
            return blls;
        }
    }
}
