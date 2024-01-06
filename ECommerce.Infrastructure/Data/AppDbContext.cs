using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce.Infrastructure.Date
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Address> Addresses{ get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
