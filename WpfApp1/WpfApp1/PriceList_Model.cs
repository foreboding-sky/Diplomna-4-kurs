using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PriceList_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        [NotMapped]
        public int Count 
        {
            get
            {
                if (PurchaseItems != null && SupplyItems != null)
                {
                    return SupplyItems.Sum(si => si.Count) - PurchaseItems.Sum(pi => pi.Count);
                }
                else
                {
                    return 0;
                }
            }
        }

        //public int PurchaseItemID { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        public List<SupplyItem> SupplyItems { get; set; }
        public PriceList_Model()
        {
            Name = "";
            Price = 0;
            PurchaseItems = new List<PurchaseItem>();
            SupplyItems = new List<SupplyItem>();
        }
        public PriceList_Model(string _name, double _price)
        {
            Name = _name;
            Price = _price;
        }
    }
}
