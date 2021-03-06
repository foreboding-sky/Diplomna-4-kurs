using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Customers_Model : IComparable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Purchase_Model> Orders { get; set; }
        public Customers_Model()
        {
            Name = "";
            Tel = "";
            Email = "";
            Address = "";
        }
        public Customers_Model(string _name, string _tel, string _email, string _address)
        {
            Name = _name;
            Tel = _tel;
            Email = _email;
            Address = _address;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Customers_Model other = obj as Customers_Model;
            if (other != null)
                return this.Name.CompareTo(other.Name);
            else
                throw new ArgumentException("Object is not a Customers_Model");
        }
    }
}
