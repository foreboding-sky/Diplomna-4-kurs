using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    public class MainWindow_ViewModel : Base_ViewModel
    {
        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (_currentPage == value)
                    return;

                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        public  MainWindow_ViewModel()
        {
            CurrentPage = new Pages.PriceList();
        }

        private RelayCommand showPriceList;
        public RelayCommand ShowPriceList
        {
            get
            {
                return showPriceList ??
                  (showPriceList = new RelayCommand(obj =>
                  {
                      CurrentPage = new Pages.PriceList();
                  }));
            }
        }
        private RelayCommand showCustomers;
        public RelayCommand ShowCustomers
        {
            get
            {
                return showCustomers ??
                  (showCustomers = new RelayCommand(obj =>
                  {
                      CurrentPage = new Pages.Customers();
                  }));
            }
        }
        private RelayCommand showPurchases;
        public RelayCommand ShowPurchases
        {
            get
            {
                return showPurchases ??
                  (showPurchases = new RelayCommand(obj =>
                  {
                      CurrentPage = new Pages.Purchase();
                  }));
            }
        }
        private RelayCommand showSuppliers;
        public RelayCommand ShowSuppliers
        {
            get
            {
                return showSuppliers ??
                  (showSuppliers = new RelayCommand(obj =>
                  {
                      CurrentPage = new Pages.Suppliers();
                  }));
            }
        }
        private RelayCommand showSupplies;
        public RelayCommand ShowSupplies
        {
            get
            {
                return showSupplies ??
                  (showSupplies = new RelayCommand(obj =>
                  {
                      CurrentPage = new Pages.Supply();
                  }));
            }
        }
    }
}
