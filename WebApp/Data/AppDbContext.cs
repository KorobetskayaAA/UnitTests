using Microsoft.EntityFrameworkCore;
using WebApp.Data.Model;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Citation> Citations { get; set; }

    }
}
