using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PriceList_Model
    {
        public int ID { get; set; }
       
        public List<ItemStock> Items { get; set; }
    }



    public class ItemStock
    {
        public Item_model Item { get; set; }
        public int Count { get; set; }

        //public int MyProperty { get; set; }
    }

    public class Item_model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
