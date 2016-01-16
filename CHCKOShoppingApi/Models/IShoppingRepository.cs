using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHCKOShoppingApi.Models
{
    interface IShoppingRepository
    {
        Dictionary<string, List<Item>> GetAll();
        IEnumerable<Item> getByType(string itemType);
        Item getItem(string type, string itemName);
        Item getItem(string type, int id);
        List<Item> Add(string itemType, List<Item> items);
        void Delete(string type, int id);
        bool Update(string type, int id, Item item);

    }
}
