using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Purchase_Model> purchase = new ObservableCollection<Purchase_Model>();
        public ObservableCollection<Purchase_Model> Purchase
        {
            get
            {
                purchase = new ObservableCollection<Purchase_Model>(MainDataBase.GetInstance().Purchase_List);
                return purchase;
            }
        }
        ObservableCollection<PriceList_Model> purchaseItems = new ObservableCollection<PriceList_Model>();
        public ObservableCollection<PriceList_Model> PurchaseItems
        {
            get
            {
                purchaseItems = new ObservableCollection<PriceList_Model>(MainDataBase.GetInstance().PriceList_List);
                return purchaseItems;
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
                    OnPropertyChanged("Purchase");
                });
            }
        }

    }
}
