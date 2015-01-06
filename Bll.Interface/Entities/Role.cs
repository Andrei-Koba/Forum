using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Bll.Interface.Entities
{
    public class Role:IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
