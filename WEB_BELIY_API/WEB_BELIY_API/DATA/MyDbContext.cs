using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.DATA
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions option) : base (option) { }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ImportBill> ImportBills { get; set; }
        public DbSet<ImportDetail> ImportDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.IDOrder, o.IDProStock });

            modelBuilder.Entity<OrderDetail>()
                     .HasOne<Order>(sc => sc.Order)
                     .WithMany(s => s.OrderDetails)
                     .HasForeignKey(sc => sc.IDOrder)
                     .OnDelete((DeleteBehavior)ReferentialAction.NoAction);

            modelBuilder.Entity<OrderDetail>()
                        .HasOne<ProductStock>(sc => sc.ProductStock)
                        .WithMany(s => s.OrderDetails)
                        .HasForeignKey(sc => sc.IDProStock)
                        .OnDelete((DeleteBehavior)ReferentialAction.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
