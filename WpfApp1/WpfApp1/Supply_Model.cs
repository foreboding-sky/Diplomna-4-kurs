using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Supply_Model
    {
        public int ID { get; set; }
        public Supplier_Model Supplier { get; set; }
        public List<SupplyItem> SupplyItems { get; set; } = new List<SupplyItem>();
        [NotMapped]
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                if (SupplyItems != null)
                {
                    foreach (var item in SupplyItems)
                    {
                        if (item.Item != null)
                        {
                            totalPrice += item.Item.Price * item.Count;
                        }
                    }
                }
                return totalPrice;
            }
        }
        public Supply_Model()
        {
        }
    }
    public class SupplyItem
    {
        public int ID { get; set; }
        //public int PurchaseID { get; set; }
        public Supply_Model Supply { get; set; }
        public PriceList_Model Item { get; set; }
        public int Count { get; set; }
        public SupplyItem()
        {
            //Item = new PriceList_Model();
            Count = 0;
        }
    }
}
