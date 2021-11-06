using Microsoft.EntityFrameworkCore;
using NotificationService.Core.Entities;

namespace NotificationService.Infra.DataContexts
{
    public class DataContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Notification> notifications { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Notification>().ToTable("notifications");
        }
    }
}
