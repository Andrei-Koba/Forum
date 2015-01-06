using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface.Entities;

namespace Dal.Interface.DataAccess
{
    public interface IUserRoleRepository: IEntityRepository<DalUserRole>
    {
    }
}
