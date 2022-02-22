using LPApi.Models;
using LPApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepo<int, ShoppingCartItem> _repo;
        public CartController(IRepo<int, ShoppingCartItem> repo)
        {
            _repo = repo;
        }

        [HttpGet]  
        public ActionResult<IEnumerable<ShoppingCartItem>> Get()     //notice action result
        {
            //List<ShoppingCartItem> cartItems = _repo.GetSpecific(userId).ToList();
            //if (cartItems.Count == 0)
            //    return BadRequest("No cart items found");
            //return Ok(cartItems);

            List<ShoppingCartItem> cartItems = _repo.GetAll().ToList();
            if (cartItems.Count == 0)
                return BadRequest("No cart items found");
            return Ok(cartItems);
        }

        //[HttpGet]   
        //[Route("SingleCartItem")]    //may not use
        //public ActionResult<ShoppingCartItem> Get(int id)
        //{
        //    var cartItem = _repo.GetT(id);
        //    if (cartItem == null)
        //        return NotFound();
        //    return Ok(cartItem);
        //}

        [HttpPost]
        public ActionResult<Product> Post(ShoppingCartItem cartItem)
        {
            var item = _repo.Add(cartItem);
            if (item != null)
            {
                return Created("Item added to cart", item);
            }
            return BadRequest("Unable to add item to cart");
        }

        [HttpPut]
        public ActionResult<ShoppingCartItem> Put(ShoppingCartItem cartItem)   //update    //in postman to put in parameter id(3) and body(one customer obj id 3 in json)
        {
            var item = _repo.Update(cartItem);
            if (item != null)
            {
                return Created("Updated", item);
            }
            return NotFound();
        }

        [HttpDelete]
        public ActionResult<ShoppingCartItem> Delete(int id)
        {
            var item = _repo.Remove(id);
            if (item != null)
            {
                return NoContent();
            }
            return NotFound(item);
        }
    }
}
