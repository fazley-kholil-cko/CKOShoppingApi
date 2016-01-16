using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHCKOShoppingApi.Models
{
    class Drink : Item
    {
        public int CapacityInLitre { get; set; }
        public string bottleType { get; set; }
    }
}