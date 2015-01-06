using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Dal.Interface.Entities
{
    public class DalRole: IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
