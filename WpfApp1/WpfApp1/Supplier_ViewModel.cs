using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Supplier_ViewModel : Base_ViewModel
    {
        private string propName = "ID";
        private bool isDescending = false;

        private Supplier_Model selectedSuppliersItem;
        public Supplier_Model SelectedSuppliersItem
        {
            get
            {
                return selectedSuppliersItem;
            }
            set
            {
                selectedSuppliersItem = value;
                OnPropertyChanged("SelectedSuppliersItem");
            }
        }

        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Supplier_Model));
        ObservableCollection<Supplier_Model> suppliers = new ObservableCollection<Supplier_Model>();
        public ObservableCollection<Supplier_Model> Suppliers
        {
            get
            {
                PropertyDescriptor prop = props.Find(propName, true);
                if (isDescending)
                {
                    suppliers = new ObservableCollection<Supplier_Model>(MainDataBase.GetInstance().Suppliers_List.OrderByDescending(x => prop.GetValue(x)));
                }
                else
                {
                    suppliers = new ObservableCollection<Supplier_Model>(MainDataBase.GetInstance().Suppliers_List.OrderBy(x => prop.GetValue(x)));
                }
                return suppliers;
            }
        }
        public Supplier_ViewModel()
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
                    OnPropertyChanged("Suppliers");
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
                    OnPropertyChanged("Suppliers");
                });
            }
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
