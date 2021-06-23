using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Supplier_Model : IComparable
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<Supply_Model> Supplies { get; set; }
        public Supplier_Model()
        {
            Name = "";
            Tel = "";
            Email = "";
            Address = "";
        }
        public Supplier_Model(string _name, string _tel, string _email, string _address)
        {
            Name = _name;
            Tel = _tel;
            Email = _email;
            Address = _address;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Supplier_Model other = obj as Supplier_Model;
            if (other != null)
                return this.Name.CompareTo(other.Name);
            else
                throw new ArgumentException("Object is not a Supplier_Model");
        }
    }
}
