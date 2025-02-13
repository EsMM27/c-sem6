using Microsoft.EntityFrameworkCore;
using RP1.Models;

namespace RP1.DataAccess
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Item> Items { get; set; }


    }

}