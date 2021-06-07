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
                    return db.PriceList_Items.Include(s => s.Item).ToList();
                    //return db.PriceList_Items.ToList();
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
                    var dbModel = db.PriceList_Items.Where(p => p.ID == model.ID).Include(p => p.Item).SingleOrDefault();
                    dbModel.Item.Name = model.Item.Name;
                    dbModel.Item.Price = model.Item.Price;
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
    }
}
