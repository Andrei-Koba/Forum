using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Implementation;
using Bll.Interface.Entities;
using Dal.Interface.Entities;
using Dal.Implementation.Concrete;
using Bll.Implementation.EntityMappers;
using Dal.Interface.DataAccess;
using System.Linq.Expressions;
using Bll.Interface;
using Bll.Interface.DataServices;
using Dal.Interface;

namespace Bll.Implementation.Services
{
    public class RoleService: IRoleService 
    {

        protected readonly IRoleRepository _repository;
        protected readonly IEntityMapper<Role, DalRole> _mapper;
        protected readonly IUserRoleRepository _userRoles;

        public RoleService(IRoleRepository repository, IEntityMapper<Role, DalRole> mapper, IUserRoleRepository userRoles)
        {
            _repository = repository;
            _mapper = mapper;
            _userRoles = userRoles;
        }

        public virtual IEnumerable<Role> GetAll()
        {
            IEnumerable<DalRole> dal = _repository.GetAll().ToList();
            IEnumerable<Role> bll = dal.Select(_mapper.GetEntityOne).ToList();
            return bll;
        }

        public virtual Role GetById(long id)
        {
            DalRole dal = _repository.FindById(id);
            return _mapper.GetEntityOne(dal);
        }

        public virtual Role GetByName(string name)
        {
            DalRole dal = _repository.Find(x => x.Name == name).Single();
            return _mapper.GetEntityOne(dal);
        }

        public virtual void Add(Role entity)
        {
            _repository.Add(_mapper.GetEntityTwo(entity));
            _repository.Save();
        }

        public virtual void Edit(Role entity)
        {
            _repository.Edit(_mapper.GetEntityTwo(entity));
            _repository.Save();
        }

        public virtual void Delete(Role entity)
        {
            _repository.Delete(_mapper.GetEntityTwo(entity));
            IEnumerable<DalUserRole> userRoles = _userRoles.Find(x => x.RoleId == entity.Id);
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


        public IEnumerable<Role> Find(Expression<Func<Role, bool>> predicates)
        {
            IEnumerable<DalRole> dals = _repository.Find(x => predicates.Compile().Invoke(_mapper.GetEntityOne(x)));
            IEnumerable<Role> blls = dals.Select(_mapper.GetEntityOne);
            return blls;
        }
    }
}
