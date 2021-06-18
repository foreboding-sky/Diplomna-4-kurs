﻿using Microsoft.EntityFrameworkCore;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename='./DiplomnaDataBase.db'");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Customers_Model>().HasMany(c => c.Orders).WithOne(p => p.Customer).OnDelete(DeleteBehavior.Cascade);
            //mb.Entity<PurchaseItem>().HasOne(p => p.Item).WithMany(p => p.PurchaseItems); //HasForeignKey<PurchaseItem>(p => p.PurchaseID);
            mb.Entity<PriceList_Model>().HasMany(p => p.PurchaseItems).WithOne(p => p.Item);
            mb.Entity<PurchaseItem>().HasOne(p => p.Purchase).WithMany(p => p.PurchaseItems);
        }
    }
}
