using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHCKOShoppingApi.Models
{
    class ShoppingRepository : IShoppingRepository
    {
        public static ShoppingRepository shoppingBasket= null;
        public static readonly Dictionary<string, List<Item>> Items = new Dictionary<string, List<Item>>();

        static List<Item> items = new List<Item>
             {
                 new Item {Id=1, name = "pepsi", price = 15, quantity = 1 },
                 new Item {Id=2, name = "fanta", price = 15, quantity = 1 },
                 new Item {Id=3, name = "sprite", price = 15, quantity = 1 },
             };

        private ShoppingRepository() {}

        public void initRepository()
        {
            addItems("drinks", items);
        }

        public static ShoppingRepository getInstance()
        {
            if (shoppingBasket == null)
            {
                
                shoppingBasket = new ShoppingRepository();
                Items.Add("drinks", items);
                return shoppingBasket;
            }
            else
                return shoppingBasket;
        }

        public void addItems(string type,List<Item> items)
        {
            if (!Items.ContainsKey(type))
            {
                Items.Add(type, items);
            }
            else
            {
                foreach (Item item in items)
                    Items[type].Add(item);
            }
        }

        public Dictionary<string, List<Item>> GetAll()
        {
            return Items;
        }

        public IEnumerable<Item> getByType(string itemType)
        {
            return Items[itemType];
        }

        public Item getItem(string type, string itemName)
        {
            return Items[type].Find(i => i.name == itemName);
        }

        public Item getItem(string type, int itemId)
        {
            return Items[type].Find(i => i.Id == itemId);

        }

        public List<Item> Add(string itemType,List<Item> items)
        {
            ShoppingRepository.getInstance().addItems(itemType, items);
            return items;
        }

        public void Delete(string type,int id)
        {
            var item = Items[type].FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items[type].Remove(item);
            }
        }

        public bool Update(string type,int id,Item item)
        {
            Item rItem = Items[type].FirstOrDefault(p => p.Id == id);
            if (rItem != null)
            {
                rItem.name = item.name;
                rItem.quantity = item.quantity;
                rItem.price = item.price;
                return true;
            }
            return false;
        }
   
    }
}