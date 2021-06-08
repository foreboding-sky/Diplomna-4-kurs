﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Purchase_Model
    {
        public int ID { get; set; }
        public Customers_Model Customer { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        [NotMapped]
        public double TotalPrice { get; set; }
    }

    public class PurchaseItem
    {
        public int ID { get; set; }
        public Purchase_Model Purchase { get; set; }
        public Item_Model Item { get; set; }
        public int Count { get; set; }
    }
}