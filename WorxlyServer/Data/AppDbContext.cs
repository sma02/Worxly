using Microsoft.EntityFrameworkCore;
using WorxlyServer.Models;

namespace WorxlyServer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerCategory> Categories{ get; set; }
        public DbSet<Chat> Chats{ get; set; }
        public DbSet<ChatMessage> ChatMessages{ get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().UseTptMappingStrategy();
        }
        public DbSet<Service> Service { get; set; } = default!;
    }
}
