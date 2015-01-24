using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.Entities;
using Bll.Interface.Entities;

namespace Bll.Implementation.EntityMappers
{
    public class RoleMapper: IEntityMapper<Role,DalRole>
    {
        public Role GetBll(DalRole dalEntity)
        {
            return new Role() { Id = dalEntity.Id, Name = dalEntity.Name };
        }

        public DalRole GetDal(Role bllEntity)
        {
            return new DalRole() { Id = bllEntity.Id, Name = bllEntity.Name };
        }

    }
}
