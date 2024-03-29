﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Dal.Interface.Entities
{
    public class DalTopic : IEntity
    {
        public long Id { get; set; }
        public virtual DalUser User { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
