using CHCKOShoppingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using System.Net;

namespace CHCKOShoppingApi.Controllers
{
    public class ShoppingController : ApiController
    {
        ShoppingRepository shoppingRepo = ShoppingRepository.getInstance();
       

        public Dictionary<string,List<Item>> GetShoppingItems()
        {
            shoppingRepo.initRepository();
            return shoppingRepo.GetAll();
        }


        public  List<Item> GetShoppingItems(string type)
        {
            shoppingRepo.initRepository();
            return shoppingRepo.getByType(type);
        }

        public Item GetItem(string name,string type)
        {
            //var products = _repository.GetProducts();
            //var product = products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

            //if (product == null)
            //{
            //    throw new HttpResponseException(
            //        Request.CreateResponse(HttpStatusCode.NotFound));
            //}
            //else
            //{
            //    return product;
            //}

            //var items = shoppingRepo.getByType(type);
            //var item = items.FirstOrDefault(i => i.name == name);
            //if (item == null)
            //{
            //    throw new HttpResponseException(
            //        Request.CreateResponse(HttpStatusCode.NotFound));
            //}
            //else
            //    return item;

            throw new HttpResponseException(
                   Request.CreateResponse(HttpStatusCode.NotFound));

        }

    }
}
