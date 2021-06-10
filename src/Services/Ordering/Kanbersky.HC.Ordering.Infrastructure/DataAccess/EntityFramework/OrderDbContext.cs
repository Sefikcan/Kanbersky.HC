using Microsoft.EntityFrameworkCore;

namespace Kanbersky.HC.Ordering.Infrastructure.DataAccess.EntityFramework
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Entities.Order> Orders { get; set; }
    }
}
