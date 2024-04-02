using EcommerceBackEnd.Entity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackEnd
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ShoesEntity> ShoesDBTable { get; set; }
    }
}
