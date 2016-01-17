# checkout-net-shopping-api

### Requirements

.Net Framework 4.5 and later

### How does checkout shopping api operate
This Api enable you to store a list of items that you want to purchase at end of each month.
For example as it has been given in your question that it shall be able to tore a drink. 
In addition a shopping list can contain specific type or category of items such as stationary items.
Therefore this Api has cater for that. A sample response can be as follows :
```html
GET : http://localhost:49707/api/Shopping
```
response:
```javascript
{"drinks":    [
                {"Id":1,"name":"pepsi","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
                {"Id":2,"name":"fanta","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
                {"Id":3,"name":"sprite","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}],
 "stationary":[ {"Id":4,"name":"pen","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
                {"Id":5,"name":"eraser","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}]}
```
If you want to retrieve only drinks as per your requirements. Just 'type=drinks as a param':
```html
GET : http://localhost:49707/api/Shopping?type=drinks
```
response:
```javascript
[
  {"Id":1,"name":"pepsi","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
  {"Id":2,"name":"fanta","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
  {"Id":3,"name":"sprite","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
 ]
```

The shopping api has been design using a repository pattern since the data will be stored
at least for now in an in memory data store thus avoiding duplication of logic to access and store our data.


##### Repository/container for our shopping item
```csharp

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
```

##### ShoppingItem model
```csharp
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
```

##### ShoppingApi controller

```csharp
namespace CHCKOShoppingApi.Controllers
{
    public class ShoppingController : ApiController
    {
        //repo as a singleton 
        ShoppingRepository shoppingRepo = ShoppingRepository.getInstance();
       
        public Dictionary<string,List<Item>> GetShoppingItems()
        {
           //(1a) Get all shopping items. Can be of type drinks, stationary and any types that has been stored.
        }

        public HttpResponseMessage GetItems(string type)
        {
          //(2a Get shopping items of specific type. For example retrieve all drinks that is to be purchased.
        }

        public HttpResponseMessage GetItem(string type, string name)
        {
          //(3a) Get a specific shopping item of specific type given its name. For example retrieve a drink of name pepsi     
        }

        public HttpResponseMessage GetItem(string type, int id)
        {
          //(4a) Get a specific shopping item of specific type given its id. For example retrieve a drink of id 1     
        }

        public HttpResponseMessage PostItem(string type,List<Item> items)
        {
           //(5a) Add items to the shopping list. For example add a pepsi of type drinks.
        }
  
        public HttpResponseMessage Delete(string type,int id)
        {
          //(6a) Delete an item of a praticular type by its id.
        }

        public HttpResponseMessage Put(string type,int id, Item item)
        {
          //(7a) Update an item of a particular type given its id.
        }
    }
}
```
### usage and explanation
use case (1a) : Get all shopping items.
url : 
```html
GET : http://localhost:49707/api/Shopping
```
code:
```csharp
  public Dictionary<string,List<Item>> GetShoppingItems()
        {
            return shoppingRepo.GetAll();
        }
```
Response:
```javascript
{"drinks":    [
                {"Id":1,"name":"pepsi","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
                {"Id":2,"name":"fanta","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
                {"Id":3,"name":"sprite","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}],
 "stationary":[ {"Id":4,"name":"pen","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
                {"Id":5,"name":"eraser","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}]}
```

use case (2a) : Get shopping items of specific type.
url : 
```html
GET : http://localhost:49707/api/Shopping?type=drinks
```
code:
```csharp
   public HttpResponseMessage GetItems(string type)
        {
              IEnumerable<Item> items;
              try
              {
                  items = shoppingRepo.getByType(type);
                  return Request.CreateResponse(HttpStatusCode.OK, items);
              }
              catch (Exception e)
              {
                  return Request.CreateResponse(HttpStatusCode.NotFound);
              } 
        }
```
Response:
```javascript
[
  {"Id":1,"name":"pepsi","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
  {"Id":2,"name":"fanta","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
  {"Id":3,"name":"sprite","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
 ]
```

use case (3a) : Get a specific shopping item of specific type given its name.
url : 
```html
GET : http://localhost:49707/api/Shopping?type=drinks&name=pepsi
```
code:
```csharp
   public HttpResponseMessage GetItem(string type, string name)
        {
            Item item;
            try
            {
                item = shoppingRepo.getItem(type, name);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }     
        }
```
Response:
```javascript
{"Id":1,"name":"pepsi","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
```

use case (4a) : Get a specific shopping item of specific type given its id.
url : 
```html
GET : http://localhost:49707/api/Shopping/3?type=drinks
```
code:
```csharp
 public HttpResponseMessage GetItem(string type, int id)
        {
            Item item;
            try
            {
                item = shoppingRepo.getItem(type, id);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
```
Response:
```javascript
{"Id":3,"name":"sprite","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
```

use case (5a) : Add items to the shopping list.
url :
```html
POST : http://localhost:49707/api/Shopping?type=drinks
```
```javascript
BODY : [{"Id":6,"name":"coca cola","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}]
```
code:
```csharp
  public HttpResponseMessage PostItem(string type,List<Item> items)
        {
            if (ModelState.IsValid)
            {
                items = shoppingRepo.Add(type,items);
                var response = Request.CreateResponse(HttpStatusCode.Created, items);

                string uri = Url.Link("DefaultApi", items);
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
```
Response:
```javascript
//status 201 created
{"Id":6,"name":"coca cola","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
```
###Adding a new category of items such as stationary :
url :
```html
POST : http://localhost:49707/api/Shopping?type=stationary
```
```javascript
BODY : [{"Id":6,"name":"pen parker","quantity":10,"price":1500.0,"DatePurchased":"0001-01-01T00:00:00"}]
```
Response:
```javascript
//status 201 created
[{"Id":6,"name":"pen parker","quantity":10,"price":1500.0,"DatePurchased":"0001-01-01T00:00:00"}]
```
### Get all shopping items including newly category just added.
url :
```html
GET : http://localhost:49707/api/Shopping
```
Response:
```javascript
{"drinks":
          [{"Id":1,"name":"pepsi","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
           {"Id":2,"name":"fanta","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
           {"Id":3,"name":"perona","quantity":10,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"
          }],
"stationary":[{"Id":1,"name":"pen","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
              {"Id":2,"name":"eraser","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
              {"Id":6,"name":"pen parker","quantity":10,"price":1500.0,"DatePurchased":"0001-01-01T00:00:00"}
              ]}
```
### To get shopping items of just a particular type
url :
```html
GET : http://localhost:49707/api/Shopping?type=stationary
```
Response:
```javascript
[ {"Id":1,"name":"pen","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
  {"Id":2,"name":"eraser","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"},
  {"Id":6,"name":"pen parker","quantity":10,"price":1500.0,"DatePurchased":"0001-01-01T00:00:00"}
]
 ```             

use case (6a) : Delete an item of a praticular type by its id.
url : 
```html
DELETE : http://localhost:49707/api/Shopping/6?type=drinks
```
code:
```csharp
  public HttpResponseMessage Delete(string type,int id)
        {
           Item item;
            try
            {
                item = shoppingRepo.getItem(type, id);
                if (item != null)
                {
                    shoppingRepo.Delete(type, id);
                    return Request.CreateResponse(HttpStatusCode.OK, item);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);          
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }  
        }
```
Response:
```javascript
//status 200 ok
 {"Id":6,"name":"coca cola","quantity":1,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
```

use case (7a) : Update an item of a particular type given its id.
url : 
```html
PUT : http://localhost:49707/api/Shopping/3?type=drinks
```
```javascript
BODY :{"Id":3,"name":"perona","quantity":10,"price":15.0,"DatePurchased":"0001-01-01T00:00:00"}
```
code:
```csharp
   public HttpResponseMessage Put(string type,int id, Item item)
        {
            if (ModelState.IsValid && id == item.Id)
            {
                var result = shoppingRepo.Update(type,id, item);
                if (result == false)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
```
Response:
```javascript
//status 200 ok
```

####Error handling
For any bad request or wrong body,url requested.
The response status will be 404 or others.
