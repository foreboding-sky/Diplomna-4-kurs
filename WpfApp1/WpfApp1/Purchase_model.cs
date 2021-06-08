using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Purchase_Model
    {
        public int ID { get; set; }
        public Customers_Model Customer { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
        [NotMapped]
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (var item in PurchaseItems)
                {
                    totalPrice += item.Item.Price * item.Count;
                }
                return totalPrice;
            }
        }
        public Purchase_Model()
        {
            Customer = new Customers_Model();
            PurchaseItems.Add(new PurchaseItem());
        }
        public Purchase_Model(string _name, double _price, int _count)
        {
            Customer = new Customers_Model();
            PurchaseItems.Add(new PurchaseItem(_name, _price, _count));
        }
    }

    public class PurchaseItem
    {
        public int ID { get; set; }
        public int PuchaseID { get; set; }
        public Purchase_Model Purchase { get; set; }
        public Item_Model Item { get; set; }
        public int Count { get; set; }
        public PurchaseItem()
        {
            Item = new Item_Model();
            Count = 0;
        }
        public PurchaseItem(string _name, double _price, int _count)
        {
            Item = new Item_Model(_name, _price);
            Count = _count;
        }
    }
}
