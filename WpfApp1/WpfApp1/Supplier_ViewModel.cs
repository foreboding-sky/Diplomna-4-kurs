using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Supplier_ViewModel : Base_ViewModel
    {
        private Supplier_Model selectedCustomersItem;
        public Supplier_Model SelectedCustomersItem
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

        ObservableCollection<Supplier_Model> suppliers = new ObservableCollection<Supplier_Model>();
        public ObservableCollection<Supplier_Model> Suppliers
        {
            get
            {
                suppliers = new ObservableCollection<Supplier_Model>(MainDataBase.GetInstance().Suppliers_List);
                return suppliers;
            }
        }
        public Supplier_ViewModel()
        {
        }
        public Command AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().AddSuppliers();
                    OnPropertyChanged("Suppliers");
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(obj =>
                {
                    Supplier_Model selectedItem = obj as Supplier_Model;
                    if (selectedItem != null)
                    {
                        MainDataBase.GetInstance().DeleteSuppliers(selectedItem.ID);
                    }
                    OnPropertyChanged("Suppliers");
                });
            }
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().SaveSuppliers(suppliers.ToList());
                    OnPropertyChanged("Suppliers");
                });
            }
        }
    }
}
