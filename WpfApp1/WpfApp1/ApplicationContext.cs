using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<PriceList_Model> PriceList_Items { get; set; }
        public DbSet<Customers_Model> Customers_Items { get; set; }
        public DbSet<Purchase_Model> Purchase_Items { get; set; }
        public DbSet<PurchaseItem> PurchaseItem_Items { get; set; }
        public DbSet<Supplier_Model> Suppliers_Items { get; set; }
        public DbSet<Supply_Model> Supply_Items { get; set; }
        public DbSet<SupplyItem> SupplyItem_Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename='./DiplomnaDataBase.db'");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Customers_Model>().HasMany(c => c.Orders).WithOne(p => p.Customer).OnDelete(DeleteBehavior.Cascade);
            mb.Entity<PriceList_Model>().HasMany(p => p.PurchaseItems).WithOne(p => p.Item);
            mb.Entity<PurchaseItem>().HasOne(p => p.Purchase).WithMany(p => p.PurchaseItems);

            mb.Entity<Supplier_Model>().HasMany(c => c.Supplies).WithOne(p => p.Supplier).OnDelete(DeleteBehavior.Cascade);
            mb.Entity<PriceList_Model>().HasMany(p => p.SupplyItems).WithOne(p => p.Item);
            mb.Entity<SupplyItem>().HasOne(p => p.Supply).WithMany(p => p.SupplyItems);

        }
    }
}
