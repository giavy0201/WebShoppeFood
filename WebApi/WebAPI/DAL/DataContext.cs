using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataContext : IdentityDbContext<Customer>
    {
        public DataContext() : base()
        {

        }
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetRoles(modelBuilder);
        }
        private void SetRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name ="Admin",ConcurrencyStamp="1",NormalizedName="Admin"},
                new IdentityRole() { Name = "Customer", ConcurrencyStamp = "2", NormalizedName = "Customer" }
                );
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<ContentProduct> ContentProducts { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ListMenu> ListMenus { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionStore> CollectionStores { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<DetailCart> DetailCarts { get; set; }
        public DbSet<AccountStore> AccountStores { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
