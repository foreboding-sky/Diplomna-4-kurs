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
        }
    }

    public class PurchaseItem
    {
        public int ID { get; set; }
        //public int PurchaseID { get; set; }
        public Purchase_Model Purchase { get; set; }
        public PriceList_Model Item { get; set; }
        public int Count { get; set; }
        public PurchaseItem()
        {
            //Item = new PriceList_Model();
            Count = 0;
        }
    }
}
