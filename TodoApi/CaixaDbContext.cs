using Dominio;
using Microsoft.EntityFrameworkCore;

namespace TodoApi
{
    public class CaixaDbContext : DbContext
    {
        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbFile = Path.Combine(appdata, "CaixaApi.sqlite");

            optionsBuilder.UseSqlite($"Data Source={dbFile}");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .Property(t => t.Priority)
                .HasConversion<string>()
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }
    }
}
