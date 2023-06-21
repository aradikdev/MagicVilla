using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa() {
                    Id = 1, 
                    Name = "Pool view", 
                }, 
                new Villa() {
                    Id = 2, 
                    Name = "Sea view", 
                }
                );
        }
    }
}
