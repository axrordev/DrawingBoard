using DrawingBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrawingBoard.Data.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Drawing> Drawings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drawing>().HasQueryFilter(entity => !entity.IsDeleted);
        }
    }
}
