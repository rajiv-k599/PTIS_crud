using Microsoft.EntityFrameworkCore;
using SimpleCrud.Models;

namespace SimpleCrud.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Student> Students { get; set; }
          }
}
