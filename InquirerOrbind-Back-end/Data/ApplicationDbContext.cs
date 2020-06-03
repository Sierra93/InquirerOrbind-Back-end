using InquirerOrbind_Back_end.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquirerOrbind_Back_end.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<User> Users { get; set; }

        public DbSet<UserDetail> UserDetailы { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MultepleContextTable>()
            .HasKey(t => new { t.UserId });

            modelBuilder.Entity<MultepleContextTable>()
               .HasOne(sc => sc.User)
               .WithMany(s => s.MultepleContextTables);

            modelBuilder.Entity<MultepleContextTable>()
               .HasOne(sc => sc.DetailUser)
               .WithMany(s => s.MultepleContextTables);
        }
    }
}
