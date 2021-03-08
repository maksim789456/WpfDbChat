using Microsoft.EntityFrameworkCore;

namespace WpfDbChat
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<ChatItemDb> ChatItems { get; set; }

        public AppDbContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=WpfDbChat.db");
        }
    }
}