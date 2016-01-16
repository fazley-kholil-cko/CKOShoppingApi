using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHCKOShoppingApi.Models
{
    interface IShoppingRepository
    {
        Dictionary<string, List<Item>> GetAll(); // implemented
        List<Item> getByType(string itemType);
        Item getByName(string itemName);
        Item Add();
        void Delete();
        bool Update();

    }
}
