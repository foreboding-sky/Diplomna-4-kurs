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
        private Purchase_ViewModel selectedPurchaseItem;
        public Purchase_ViewModel SelectedPurchaseItem
        {
            get
            {
                return selectedPurchaseItem;
            }
            set
            {
                selectedPurchaseItem = value;
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
                    OnPropertyChanged("PriceList");
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command(obj =>
                {
                    PriceList_Model selectedItem = obj as PriceList_Model;
                    if (selectedItem != null)
                    {
                        MainDataBase.GetInstance().DeletePurchase(selectedItem.ID);
                    }
                    OnPropertyChanged("PriceList");
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
                    OnPropertyChanged("PriceList");
                });
            }
        }
    }
}
