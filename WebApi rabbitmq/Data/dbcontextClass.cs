using Microsoft.EntityFrameworkCore;
using WebApi_rabbitmq.Models;
namespace WebApi_rabbitmq.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductClass>()
                .HasKey(p => p.ProductID);
        }
        public DbSet<ProductClass> Product_
        {
            get;
            set;
        }
    }
}