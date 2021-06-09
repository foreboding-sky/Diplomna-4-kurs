using Microsoft.EntityFrameworkCore;
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
                    return db.PriceList_Items.ToList();
                }
            }
        }
        public List<Customers_Model> Customers_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Customers_Items.ToList();
                }
            }
        }
        public List<Purchase_Model> Purchase_List
        {
            get
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    return db.Purchase_Items.ToList();
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
                if(!db.Purchase_Items.Any())
                {
                    db.Purchase_Items.AddRange(GetPurchaseItems());
                    db.SaveChanges();
                }
            }
        }
        private List<PriceList_Model> GetPriceListItems()
        {
            return new List<PriceList_Model>()
            {
                new PriceList_Model()
            };
        }
        private List<Customers_Model> GetCustomersItems()
        {
            return new List<Customers_Model>()
            {
                new Customers_Model("Test cutomer", "380692281337", "test@gmail.com", "Test address")
            };
        }
        private List<Purchase_Model> GetPurchaseItems()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Purchase_Model> purchases = new List<Purchase_Model>();
                purchases.Add(new Purchase_Model { Customer = db.Customers_Items.FirstOrDefault() });
            }
            return new List<Purchase_Model>()
            {
                
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
                    dbModel.Count = model.Count;
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
        #region Purchase
        public void AddPurchase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Purchase_Items.Add(new Purchase_Model());
                db.SaveChanges();
            }
        }
        public void DeletePurchase(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Purchase_Items.Remove(db.Purchase_Items.First(t => t.ID == id));
                db.SaveChanges();
            }
        }
        public void SavePurchase(List<Purchase_Model> purchaseModels)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var model in purchaseModels)
                {
                    var dbModel = db.Purchase_Items.Where(p => p.ID == model.ID).Include(p => p.Customer).SingleOrDefault();
                    dbModel.Customer = model.Customer;
                    foreach(var purchaseItem in model.PurchaseItems)
                    {
                        var dbPurchaseItem = db.PurchaseItem_Items.Where(p => p.ID == purchaseItem.ID).Include(p => p.Item).SingleOrDefault();
                        if (dbPurchaseItem != null)
                        {
                            dbPurchaseItem = purchaseItem;
                        }
                        else
                        {
                            dbPurchaseItem = new PurchaseItem();
                            dbPurchaseItem = purchaseItem;
                        }
                        //var dbItem = db.Items.Where(p => p.ID == purchaseItem.Item.ID).FirstOrDefault();
                        //if (dbItem != null)
                        //{
                        //    dbItem.Name = purchaseItem.Item.Name;
                        //    dbItem.Price = purchaseItem.Item.Price;
                        //}
                        //else
                        //{
                        //    dbItem = new Item_Model();
                        //    dbItem.Name = purchaseItem.Item.Name;
                        //    dbItem.Price = purchaseItem.Item.Price;
                        //    db.Items.Add(dbItem);
                        //}
                    }
                }
                db.SaveChanges();
            }
        }
        #endregion
    }
}