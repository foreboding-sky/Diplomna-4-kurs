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
        public int Count { get; set; }
        //public int PurchaseItemID { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        public PriceList_Model()
        {
            Name = "";
            Price = 0;
            Count = 0;
        }
        public PriceList_Model(string _name, double _price)
        {
            Name = _name;
            Price = _price;
            Count = 0;
        }
    }
}
