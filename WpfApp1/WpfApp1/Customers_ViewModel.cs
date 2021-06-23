using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Customers_ViewModel : Base_ViewModel
    {
        private string propName = "ID";
        private bool isDescending = false;

        private Customers_Model selectedCustomersItem;
        public Customers_Model SelectedCustomersItem
        {
            get
            {
                return selectedCustomersItem;
            }
            set
            {
                selectedCustomersItem = value;
                OnPropertyChanged("SelectedCustomersItem");
            }
        }

        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Customers_Model));
        ObservableCollection<Customers_Model> customers = new ObservableCollection<Customers_Model>();
        public ObservableCollection<Customers_Model> Customers
        {
            get
            {
                PropertyDescriptor prop = props.Find(propName, true);
                if (isDescending)
                {
                    customers = new ObservableCollection<Customers_Model>(MainDataBase.GetInstance().Customers_List.OrderByDescending(x => prop.GetValue(x)));
                }
                else
                {
                    customers = new ObservableCollection<Customers_Model>(MainDataBase.GetInstance().Customers_List.OrderBy(x => prop.GetValue(x)));
                }
                return customers;
            }
        }
        public Customers_ViewModel()
        {
        }
        public Command Sort
        {
            get
            {
                return new Command(obj =>
                {
                    propName = obj.ToString();
                    isDescending = false;
                    OnPropertyChanged("Customers");
                });
            }
        }
        public Command SortDescending
        {
            get
            {
                return new Command(obj =>
                {
                    propName = obj.ToString();
                    isDescending = true;
                    OnPropertyChanged("Customers");
                });
            }
        }
        public Command AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().AddCustomers();
                    OnPropertyChanged("Customers");
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(obj =>
                {
                    Customers_Model selectedItem = obj as Customers_Model;
                    if (selectedItem != null)
                    {
                        MainDataBase.GetInstance().DeleteCustomers(selectedItem.ID);
                    }
                    OnPropertyChanged("Customers");
                });
            }
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().SaveCustomers(customers.ToList());
                    OnPropertyChanged("Customers");
                });
            }
        }
    }
}
