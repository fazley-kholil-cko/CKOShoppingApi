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
           
            return shoppingRepo.GetAll();
        }

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
    }
}
