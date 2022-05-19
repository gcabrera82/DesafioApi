using DesafioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }


        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Store> Store { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<Voucher> Voucher { get; set; }

        public DbSet<StoreProducts> StoreProducts { get; set; }

        public DbSet<VoucherProducts> VoucherProducts   { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
    }
}
