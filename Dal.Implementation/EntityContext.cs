using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Interface.Entities;
using System.Diagnostics;

namespace Dal.Implementation
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("name=Forum") 
        {
            //this.Configuration.LazyLoadingEnabled = false;
            Debug.WriteLine("---------------------context creating---------------------");
        }

        public DbSet<DalUser> Users { get; set; }
        public DbSet<DalRole> Roles { get; set; }
        public DbSet<DalTopic> Topics { get; set; }
        public DbSet<DalPost> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DalRole>().HasMany(c => c.Users)
                .WithMany(s => s.Roles)
                .Map(t => t.MapLeftKey("RoleId")
                .MapRightKey("UserId")
                .ToTable("UserRoles"));

            modelBuilder.Entity<DalTopic>()
                .HasRequired(c => c.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DalPost>()
                .HasRequired(c => c.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DalPost>()
                .HasRequired(c => c.Topic)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        protected override void Dispose(bool disposing)
        {
            Debug.WriteLine("-----------context disposing------------------");
            base.Dispose(disposing);
        }

    }   
}
