using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.DB
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>().HasKey(p => new { p.AppUserId, p.StockId });
            builder.Entity<Portfolio>().HasOne(p => p.AppUser).WithMany(p => p.Portfolios).HasForeignKey(p => p.AppUserId);
            builder.Entity<Portfolio>().HasOne(p => p.Stock).WithMany(p => p.Portfolios).HasForeignKey(p => p.StockId);

            List<IdentityRole> roles =
            [
                new() {Name = "Admin", NormalizedName = "ADMIN"},
                new() {Name = "User", NormalizedName = "USER"}
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}