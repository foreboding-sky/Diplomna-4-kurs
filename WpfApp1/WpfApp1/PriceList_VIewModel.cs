using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PriceList_VIewModel : Base_ViewModel
    {
        private string propName = "ID";
        private bool isDescending = false;

        private PriceList_Model selectedPriceListItem;
        public PriceList_Model SelectedPriceListItem
        {
            get
            {
                return selectedPriceListItem;
            }
            set
            {
                selectedPriceListItem = value;
                OnPropertyChanged("SelectedPriceListItem");
            }
        }

        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(PriceList_Model));
        ObservableCollection<PriceList_Model> priceList = new ObservableCollection<PriceList_Model>();
        public ObservableCollection<PriceList_Model> PriceList
        {
            get
            {
                PropertyDescriptor prop = props.Find(propName, true);
                if (isDescending)
                {
                    priceList = new ObservableCollection<PriceList_Model>(MainDataBase.GetInstance().PriceList_List.OrderByDescending(x => prop.GetValue(x)));
                }
                else
                {
                    priceList = new ObservableCollection<PriceList_Model>(MainDataBase.GetInstance().PriceList_List.OrderBy(x => prop.GetValue(x)));
                }
                return priceList;
            }
        }
        public PriceList_VIewModel()
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
                    OnPropertyChanged("PriceList");
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
                    OnPropertyChanged("PriceList");
                });
            }
        }
        public Command AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    MainDataBase.GetInstance().AddPriceList();
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
                    if(selectedItem != null)
                    {
                        MainDataBase.GetInstance().DeletePriceListItem(selectedItem.ID);
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
                    MainDataBase.GetInstance().SavePriceList(priceList.ToList());
                    OnPropertyChanged("PriceList");
                });
            }
        }
    }
}
