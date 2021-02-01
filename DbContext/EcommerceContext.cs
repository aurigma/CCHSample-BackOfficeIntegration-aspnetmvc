using CustomersCanvasSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Db
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options): base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=ecommerce-data.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product("7013145727", "Test Product 1", 14.99f, "https://picsum.photos/id/1025/600/800"),
                new Product("4245545145", "Test Product 2", 9.99f, "https://picsum.photos/id/1026/600/800"),
                new Product("1253932525", "Test Product 3", 19.99f, "https://picsum.photos/id/1027/600/800"),
                new Product("9559586705", "Test Product 4", 4.99f, "https://picsum.photos/id/1028/600/800"),
                new Product("1540922619", "Test Product 5", 1.99f, "https://picsum.photos/id/1029/600/800")
            );
        }
    }

}
