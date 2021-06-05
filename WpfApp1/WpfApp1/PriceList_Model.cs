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
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public PriceList_Model()
        {
            Name = "";
            Count = 0;
            Price = 0;
        }
        public PriceList_Model(string _name, int _count, double _price)
        {
            Name = _name;
            Count = _count;
            Price = _price;
        }
    }
}
