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
    public class RoleService : BaseService<DalRole, Role, IEntityRepository<DalRole>, RoleMapper>, IRoleService
    {
        public RoleService(IRoleRepository repository, IUnitOfWork uow)
            : base(repository,uow) { }

        public Role GetByName(string name)
        {
            DalRole dal = _repository.Find(x => x.Name == name).FirstOrDefault();
            if (dal == null) return null;
            return _mapper.GetBll(dal);
        }
    }
}
