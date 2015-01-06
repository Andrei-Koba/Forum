using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Dal.Interface.Entities
{
    public class DalUserRole: IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }

    }
}
