using Microsoft.EntityFrameworkCore;

namespace YourNamespace
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets here. For example:
        // public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your entities here. For example:
            // modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
