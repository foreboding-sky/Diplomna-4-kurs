using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Purchase_ViewModel : Base_ViewModel
    {
        public int ItemsCount
        {
            get
            {
                var item = selectedPurchaseItem;
                if (item != null)
                    return item.PurchaseItems.Count();
                else
                {
                    return 0;
                }
            }
        }

        private Purchase_Model selectedPurchaseItem;
        public Purchase_Model SelectedPurchaseItem
        {
            get
            {
                return selectedPurchaseItem;
            }
            set
            {
                selectedPurchaseItem = value;
                OnPropertyChanged("Customers");
                OnPropertyChanged("SelectedPurchaseItem");
                OnPropertyChanged("ItemsCount");
            }
        }
        private PurchaseItem selectedItem;
        public PurchaseItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedPurchaseItem");
            }
        }
        public ObservableCollection<Customers_Model> Customers
        {
            get
            {
                return new ObservableCollection<Customers_Model>(MainDataBase.GetInstance().Customers_List);
            }
        }

        //private Func<Purchase_Model, object> selector = x => x.ID;
        private string propName = "ID";
        private bool isDescending = false;

        public Command Sort
        {
            get
            {
                return new Command(obj =>
                {
                    propName = obj.ToString();
                    OnPropertyChanged("Purchase");
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
                    OnPropertyChanged("Purchase");
                });
            }
        }

        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Purchase_Model));
        ObservableCollection<Purchase_Model> purchase = new ObservableCollection<Purchase_Model>();
        public ObservableCollection<Purchase_Model> Purchase
        {
            get
            {
                PropertyDescriptor prop = props.Find(propName, true);
                if (isDescending)
                {
                    purchase = new ObservableCollection<Purchase_Model>(MainDataBase.GetInstance().Purchase_List.OrderByDescending(x => prop.GetValue(x)));
                }
                else
                {
                    purchase = new ObservableCollection<Purchase_Model>(MainDataBase.GetInstance().Purchase_List.OrderBy(x => prop.GetValue(x)));
                }
                return purchase;
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
        public Purchase_ViewModel()
        {
        }
        public Command AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().AddPurchase();
                    OnPropertyChanged("Purchase");
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(obj =>
                {
                    Purchase_Model selectedItem = obj as Purchase_Model;
                    if (selectedItem != null)
                    {
                        MainDataBase.GetInstance().DeletePurchase(selectedItem.ID);
                    }
                    OnPropertyChanged("Purchase");
                });
            }
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().SavePurchase(purchase.ToList());
                    OnPropertyChanged("Purchase");
                });
            }
        }
        public Command AddItem
        {
            get
            {
                return new Command(obj =>
                {
                    if(selectedPurchaseItem!=null)
                    {
                        selectedPurchaseItem.PurchaseItems.Add(new PurchaseItem());
                        MainDataBase.GetInstance().SavePurchaseItem(selectedPurchaseItem);
                    }
                    var sas = purchase.IndexOf(selectedPurchaseItem);
                    OnPropertyChanged("Purchase");
                    selectedPurchaseItem = purchase[sas];
                    OnPropertyChanged("SelectedPurchaseItem");
                });
            }
        }

        public Command RemovePurchaseItem
        {
            get
            {
                return new Command(obj =>
                {
                    int id = 0;
                    if (int.TryParse(obj.ToString(), out id))
                    {
                        var pi = selectedPurchaseItem.PurchaseItems.First(pi => pi.ID == id);
                        selectedPurchaseItem.PurchaseItems.Remove(pi);

                        MainDataBase.GetInstance().RemovePurchaseItem(pi);

                        var sas = purchase.IndexOf(selectedPurchaseItem);
                        OnPropertyChanged("Purchase");
                        selectedPurchaseItem = purchase[sas];
                        OnPropertyChanged("SelectedPurchaseItem");
                    }
                });
            }
        }
        

    }
}
