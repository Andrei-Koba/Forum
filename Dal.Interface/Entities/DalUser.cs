using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Dal.Interface.Entities
{
    public class DalUser : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Mail { get; set; }
        public string Avatar { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Pass { get; set; }
    }
}
