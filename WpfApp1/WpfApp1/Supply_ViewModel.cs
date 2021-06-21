using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Supply_ViewModel : Base_ViewModel
    {
        public int ItemsCount
        {
            get
            {
                var item = selectedSupplyItem;
                if (item != null)
                    return item.SupplyItems.Count();
                else
                {
                    return 0;
                }
            }
        }
        private Supply_Model selectedSupplyItem;
        public Supply_Model SelectedSupplyItem
        {
            get
            {
                return selectedSupplyItem;
            }
            set
            {
                selectedSupplyItem = value;
                OnPropertyChanged("Suppliers");
                OnPropertyChanged("SelectedSupplyItem");
                OnPropertyChanged("ItemsCount");
            }
        }

        public ObservableCollection<Supplier_Model> Suppliers
        {
            get
            {
                return new ObservableCollection<Supplier_Model>(MainDataBase.GetInstance().Suppliers_List);
            }
        }

        private string propName = "ID";
        private bool isDescending = false;

        public Command Sort
        {
            get
            {
                return new Command(obj =>
                {
                    propName = obj.ToString();
                    isDescending = false;
                    OnPropertyChanged("Supply");
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
                    OnPropertyChanged("Supply");
                });
            }
        }

        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Supply_Model));
        ObservableCollection<Supply_Model> supply = new ObservableCollection<Supply_Model>();
        public ObservableCollection<Supply_Model> Supply
        {
            get
            {
                PropertyDescriptor prop = props.Find(propName, true);
                if (isDescending)
                {
                    supply = new ObservableCollection<Supply_Model>(MainDataBase.GetInstance().Supply_List.OrderByDescending(x => prop.GetValue(x)));
                }
                else
                {
                    supply = new ObservableCollection<Supply_Model>(MainDataBase.GetInstance().Supply_List.OrderBy(x => prop.GetValue(x)));
                }
                return supply;
            }
        }
        ObservableCollection<PriceList_Model> products = new ObservableCollection<PriceList_Model>();
        public ObservableCollection<PriceList_Model> Products
        {
            get
            {
                products = new ObservableCollection<PriceList_Model>(MainDataBase.GetInstance().PriceList_List);
                return products;
            }
        }
        public Supply_ViewModel()
        {
        }
        public Command AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().AddSupply();
                    OnPropertyChanged("Supply");
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(obj =>
                {
                    Supply_Model selectedItem = obj as Supply_Model;
                    if (selectedItem != null)
                    {
                        MainDataBase.GetInstance().DeleteSupply(selectedItem.ID);
                    }
                    OnPropertyChanged("Supply");
                });
            }
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().SaveSupply(supply.ToList());
                    OnPropertyChanged("Supply");
                });
            }
        }
        public Command AddItem
        {
            get
            {
                return new Command(obj =>
                {
                    if (selectedSupplyItem != null)
                    {
                        selectedSupplyItem.SupplyItems.Add(new SupplyItem());
                        MainDataBase.GetInstance().SaveSupplyItem(selectedSupplyItem);
                    }
                    var sas = supply.IndexOf(selectedSupplyItem);
                    OnPropertyChanged("Supply");
                    selectedSupplyItem = supply[sas];
                    OnPropertyChanged("SelectedSupplyItem");
                });
            }
        }

        public Command RemoveSupplyItem
        {
            get
            {
                return new Command(obj =>
                {
                    int id = 0;
                    if (int.TryParse(obj.ToString(), out id))
                    {
                        var pi = selectedSupplyItem.SupplyItems.First(pi => pi.ID == id);
                        selectedSupplyItem.SupplyItems.Remove(pi);

                        MainDataBase.GetInstance().RemoveSupplyItem(pi);

                        var sas = supply.IndexOf(selectedSupplyItem);
                        OnPropertyChanged("Supply");
                        selectedSupplyItem = supply[sas];
                        OnPropertyChanged("SelectedSupplyItem");
                    }
                });
            }
        }
    }
}
