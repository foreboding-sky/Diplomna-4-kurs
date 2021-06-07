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
        public Item_Model Item { get; set; }
        public int Count { get; set; }
        public PriceList_Model()
        {
            Item = new Item_Model();
            Count = 0;
        }
        public PriceList_Model(string _name, double _price, int _count)
        {
            Item = new Item_Model(_name, _price);
            Count = _count;
        }
    }

    public class Item_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Item_Model()
        {
            Name = "";
            Price = 0;
        }
        public Item_Model(string _name, double _price)
        {
            Name = _name;
            Price = _price;
        }
    }
}
