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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename='./DiplomnaDataBase.db'");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Customers_Model>().HasMany(c => c.Orders).WithOne(p => p.Customer).OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PurchaseItem>().HasOne(p => p.Item);
            mb.Entity<PurchaseItem>().HasOne(p => p.Purchase).WithMany(p => p.PurchaseItems);

        }
    }
}
