using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHCKOShoppingApi.Models
{
    class ShoppingRepository : IShoppingRepository
    {
        public static ShoppingRepository shoppingBasket= null;
        public static Dictionary<string, List<Item>> Items = new Dictionary<string, List<Item>>();

        List<Item> drinkItems = new List<Item>
             {
                 new Drink { bottleType = "Pepsi", CapacityInLitre = 1,  name = "drink", price = 15, quantity = 1 },
                 new Drink { bottleType = "Coca", CapacityInLitre = 1,  name = "drink", price = 15, quantity = 1 },
                 new Drink { bottleType = "Sprite", CapacityInLitre = 1,  name = "drink", price = 15, quantity = 1 },
                 new Drink { bottleType = "Fanta", CapacityInLitre = 1,  name = "drink", price = 15, quantity = 1 }
             };

        private ShoppingRepository() { }

        public void initRepository()
        {
            ShoppingRepository.getInstance().addItems("drinks", drinkItems);
        }

        public static ShoppingRepository getInstance()
        {
            if (shoppingBasket == null)
            {
                return new ShoppingRepository();;
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

        public List<Item> getByType(string itemType)
        {
            try 
            { 
                return Items[itemType];
            }
            catch (Exception e) 
            { 
                return null; 
            }
            
        }

        public Item getByName(string itemName)
        {
            var item = Items["drinks"].Find(i => i.name == itemName);
            return item;
        }

        public Item Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}