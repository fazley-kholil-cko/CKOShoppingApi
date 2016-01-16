using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CHCKOShoppingApi.Models
{
    public class Item
    {
        private DateTime datePurchased;
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public DateTime DatePurchased
        {
            get { return datePurchased; }
            set { datePurchased = new DateTime(); }
        }
    }
}