using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Interface.Entities;

namespace Dal.Implementation
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("name=Forum") { }

        public DbSet<DalUser> Users { get; set; }
        public DbSet<DalRole> Roles { get; set; }
        public DbSet<DalTopic> Topics { get; set; }
        public DbSet<DalPost> Posts { get; set; }
        public DbSet<DalUserRole> UserRoles { get; set; }

    }   
}
