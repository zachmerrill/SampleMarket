using Microsoft.EntityFrameworkCore;

namespace SampleMarket.Data
{
    public class SampleMarketDbContext : DbContext
    {
        public SampleMarketDbContext (DbContextOptions<SampleMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<SampleMarket.Models.Product> Products { get; set; }

        public DbSet<SampleMarket.Models.CartItem> CartItems { get; set; }
    }
}
