using EcommerceBackEnd.Entity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Api Fluent used to configure de composed primary key for the ShoesOrdersDBTable

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoesOrdersEntity>()
                .HasKey(x => new { x.ShoesId, x.OrderId }); // columns that represent primary keys

            base.OnModelCreating(modelBuilder);
        }

        // DB sets to handle data from database
        public DbSet<ShoesEntity> ShoesDBTable { get; set; }
        public DbSet<OrdersEntity> OrdersDBTable { get; set; }
        public DbSet<ShoesOrdersEntity> ShoesOrdersDBTable { get; set; }
    }
}
