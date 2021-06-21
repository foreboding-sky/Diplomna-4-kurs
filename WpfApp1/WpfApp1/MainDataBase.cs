﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MainDataBase
    {
        #region Singleton
        private static MainDataBase instance;
        public static MainDataBase GetInstance()
        {
            if (instance == null)
                instance = new MainDataBase();
            return instance;
        }
        #endregion
        public List<PriceList_Model> PriceList_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.PriceList_Items.Include(pl => pl.SupplyItems).Include(pl => pl.PurchaseItems).ToList();
                }
            }
        }
        public List<Customers_Model> Customers_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Customers_Items.Include(c => c.Orders).ToList();
                }
            }
        }
        public List<Supplier_Model> Suppliers_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Suppliers_Items.Include(c => c.Supplies).ToList();
                }
            }
        }
        public List<Supply_Model> Supply_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Supply_Items.Include(p => p.Supplier).Include(p => p.SupplyItems).ThenInclude(p => p.Item).ToList();
                }
            }
        }
        public List<Purchase_Model> Purchase_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Purchase_Items.Include(p => p.Customer).Include(p => p.PurchaseItems).ThenInclude(p => p.Item).ToList();
                }
            }
        }
        public MainDataBase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (!db.PriceList_Items.Any())
                {
                    db.PriceList_Items.AddRange(GetPriceListItems());
                    db.SaveChanges();
                }
                if (!db.Customers_Items.Any())
                {
                    db.Customers_Items.AddRange(GetCustomersItems());
                    db.SaveChanges();
                }
                if (!db.Suppliers_Items.Any())
                {
                    db.Suppliers_Items.AddRange(GetSuppliersItems());
                    db.SaveChanges();
                }
            }
        }
        private List<PriceList_Model> GetPriceListItems()
        {
            return new List<PriceList_Model>()
            {
                new PriceList_Model{Name = "Test1", Price = 10},
                new PriceList_Model{Name = "Test2", Price = 20},
                new PriceList_Model{Name = "Test3", Price = 15},
                new PriceList_Model{Name = "Test4", Price = 5}
            };
        }
        private List<Customers_Model> GetCustomersItems()
        {
            return new List<Customers_Model>()
            {
                new Customers_Model("Test cutomer", "380692281337", "test@gmail.com", "Test address"),
                new Customers_Model("Test cutomer2", "380692281337", "test@gmail.com", "Test address"),
                new Customers_Model("Test cutomer3", "380692281337", "test@gmail.com", "Test address"),
                new Customers_Model("Test cutomer4", "380692281337", "test@gmail.com", "Test address")
            };
        }
        private List<Supplier_Model> GetSuppliersItems()
        {
            return new List<Supplier_Model>()
            {
                new Supplier_Model("Test supplier", "380692281337", "test@gmail.com", "Test address"),
                new Supplier_Model("Test supplier2", "380692281337", "test@gmail.com", "Test address"),
                new Supplier_Model("Test supplier3", "380692281337", "test@gmail.com", "Test address"),
                new Supplier_Model("Test supplier4", "380692281337", "test@gmail.com", "Test address")
            };
        }
        #region PriceList
        public void AddPriceList()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.PriceList_Items.Add(new PriceList_Model());
                db.SaveChanges();
            }
        }
        public void DeletePriceListItem(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.PriceList_Items.Remove(db.PriceList_Items.First(t => t.ID == id));
                db.SaveChanges();
            }
        }
        public void SavePriceList(List<PriceList_Model> priceListModels)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach(var model in priceListModels)
                {
                    var dbModel = db.PriceList_Items.Where(p => p.ID == model.ID).SingleOrDefault();
                    dbModel.Name = model.Name;
                    dbModel.Price = model.Price;
                }
                db.SaveChanges();
            }
        }
        #endregion
        #region Custommers
        public void AddCustomers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Customers_Items.Add(new Customers_Model());
                db.SaveChanges();
            }
        }
        public void DeleteCustomers(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Customers_Items.Remove(db.Customers_Items.First(t => t.ID == id));
                db.SaveChanges();
            }
        }
        public void SaveCustomers(List<Customers_Model> customers)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var model in customers)
                {
                    var item = db.Customers_Items.Find(model.ID);
                    item.Name = model.Name;
                    item.Tel = model.Tel;
                    item.Email = model.Email;
                    item.Address = model.Address;
                }
                db.SaveChanges();
            }
        }
        #endregion
        #region Suppliers
        public void AddSuppliers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Suppliers_Items.Add(new Supplier_Model());
                db.SaveChanges();
            }
        }
        public void DeleteSuppliers(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Suppliers_Items.Remove(db.Suppliers_Items.First(t => t.ID == id));
                db.SaveChanges();
            }
        }
        public void SaveSuppliers(List<Supplier_Model> suppliers)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var model in suppliers)
                {
                    var item = db.Suppliers_Items.Find(model.ID);
                    item.Name = model.Name;
                    item.Tel = model.Tel;
                    item.Email = model.Email;
                    item.Address = model.Address;
                }
                db.SaveChanges();
            }
        }
        #endregion
        #region Purchase
        public void AddPurchase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Purchase_Items.Add(new Purchase_Model() { Customer = db.Customers_Items.FirstOrDefault()});
                db.SaveChanges();
            }
        }
        public void DeletePurchase(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //var purchase = db.Purchase_Items.FirstOrDefault(p => p.ID == id);
                //db.PurchaseItem_Items.RemoveRange(purchase.PurchaseItems);
                db.Purchase_Items.Remove(db.Purchase_Items.First(t => t.ID == id));
                db.SaveChanges();
            }
        }
        public void SavePurchase(List<Purchase_Model> purchaseModels)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.PurchaseItem_Items.RemoveRange(db.PurchaseItem_Items);
                db.SaveChanges();
                foreach (var model in purchaseModels)
                {
                    foreach (var pi in model.PurchaseItems)
                    {
                        var itemToAdd = db.PriceList_Items.Find(pi.Item.ID);
                        var purchaseToAdd = db.Purchase_Items.Find(model.ID);
                        db.PurchaseItem_Items.Add(new PurchaseItem() { Item = itemToAdd, Purchase = purchaseToAdd, Count = pi.Count });

                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception e)
                        {
                            var sas = e.Message;
                        }
                    }

                    if (model.Customer != null)
                    {
                        var customer = db.Customers_Items.Include(p => p.Orders).First(p => p.ID == model.Customer.ID);
                        if (customer.Orders == null)
                        {
                            customer.Orders = new List<Purchase_Model>();
                        }
                        if (!customer.Orders.Any(o => o.ID == model.ID))
                        {
                            var customersWithThisOrder = this.Customers_List.Where(c => c.Orders.Any(o => o.ID == model.ID)).ToList();
                            if (customersWithThisOrder.Count > 0)
                            {
                                var test = db.Purchase_Items.Include(o => o.Customer).First(o => o.ID == model.ID);
                                customersWithThisOrder.First().Orders.Remove(test);
                                customer.Orders.Add(test);
                                db.SaveChanges();
                            }
                            else
                            {
                                customer.Orders.Add(model);
                            }
                        }   
                    }
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    var sas = e.Message;
                }
                GC.Collect();
            }
        }

        public void RemovePurchaseItem(PurchaseItem item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.PurchaseItem_Items.Remove(db.PurchaseItem_Items.Find(item.ID));
                db.SaveChanges();
            }
        }

        public void SavePurchaseItem(Purchase_Model model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var dbModel = db.Purchase_Items.Where(p => p.ID == model.ID).Include(p =>p.Customer).Include(p=>p.PurchaseItems).SingleOrDefault();
                dbModel.PurchaseItems.Add(new PurchaseItem { Purchase = model, Item = db.PriceList_Items.FirstOrDefault()});
                db.SaveChanges();
            }
        }
        #endregion
        #region Supply
        public void AddSupply()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Supply_Items.Add(new Supply_Model() { Supplier = db.Suppliers_Items.FirstOrDefault()});
                db.SaveChanges();
            }
        }
        public void DeleteSupply(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Supply_Items.Remove(db.Supply_Items.First(t => t.ID == id));
                db.SaveChanges();
            }
        }
        public void SaveSupply(List<Supply_Model> supplyModels)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SupplyItem_Items.RemoveRange(db.SupplyItem_Items);
                db.SaveChanges();
                foreach (var model in supplyModels)
                {
                    foreach (var pi in model.SupplyItems)
                    {
                        var itemToAdd = db.PriceList_Items.Find(pi.Item.ID);
                        var supplyToAdd = db.Supply_Items.Find(model.ID);
                        db.SupplyItem_Items.Add(new SupplyItem() { Item = itemToAdd, Supply = supplyToAdd, Count = pi.Count, Price = pi.Price});
                    }

                    if (model.Supplier != null)
                    {
                        var supplier = db.Suppliers_Items.Include(p => p.Supplies).First(p => p.ID == model.Supplier.ID);
                        if (supplier.Supplies == null)
                        {
                            supplier.Supplies = new List<Supply_Model>();
                        }
                        if (!supplier.Supplies.Any(o => o.ID == model.ID))
                        {
                            var suppliersWithThisOrder = this.Suppliers_List.Where(c => c.Supplies.Any(o => o.ID == model.ID)).ToList();
                            if (suppliersWithThisOrder.Count > 0)
                            {
                                var test = db.Supply_Items.Include(o => o.Supplier).First(o => o.ID == model.ID);
                                suppliersWithThisOrder.First().Supplies.Remove(test);
                                supplier.Supplies.Add(test);
                                db.SaveChanges();
                            }
                            else
                            {
                                supplier.Supplies.Add(model);
                            }
                        }
                    }
                }
                db.SaveChanges();
                GC.Collect();
            }
        }

        public void RemoveSupplyItem(SupplyItem item)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SupplyItem_Items.Remove(db.SupplyItem_Items.Find(item.ID));
                db.SaveChanges();
            }
        }

        public void SaveSupplyItem(Supply_Model model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var dbModel = db.Supply_Items.Where(p => p.ID == model.ID).Include(p => p.Supplier).Include(p => p.SupplyItems).SingleOrDefault();
                dbModel.SupplyItems.Add(new SupplyItem { Supply = model, Item = db.PriceList_Items.FirstOrDefault() });
                db.SaveChanges();
            }
        }
        #endregion
    }
}