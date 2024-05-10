using Microsoft.EntityFrameworkCore;
using CrystalSharpReadModelStoreSqlServerExample.Application.ReadModels;

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Infrastructure
{
    public class AppReadModelStoreDbContext : DbContext
    {
        public DbSet<CustomerOrderReadModel> CustomerOrder { get; set; }

        public AppReadModelStoreDbContext(DbContextOptions<AppReadModelStoreDbContext> options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
