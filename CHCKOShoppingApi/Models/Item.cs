using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHCKOShoppingApi.Models
{
    public class Item
    {
        private static int idCounter = 0;
        private DateTime datePurchased;

        public int Id
        {
            get { idCounter++; return idCounter; }
        }

        public DateTime DatePurchased
        {
            get { return datePurchased; }
            set { datePurchased = new DateTime(); }
        }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
       
    }
}