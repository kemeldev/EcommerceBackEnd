using EcommerceBackEnd.Entity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // DB sets to handle data from database
        public DbSet<ShoesEntity> ShoesDBTable { get; set; }
    }
}
