using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.DataAccess;
using Dal.Implementation.Concrete;
using Dal.Interface.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Dal.Interface;

namespace Dal.Implementation.Concrete
{
    public class RoleRepository : EntityRepository<DalRole>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context) { }
    }
}
