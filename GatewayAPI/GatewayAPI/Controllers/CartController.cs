using GatewayAPI.Models;
using GatewayAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]   //remember to add this
    public class CartController : ControllerBase
    {
        private readonly IRepo<int, ShoppingCartItemDTO> _repo;

        public CartController(IRepo<int, ShoppingCartItemDTO> repo)
        {
            _repo = repo;
        }

        // GET: CustomerController
        [Route("GetShopperCart")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemDTO>>> IndexAsync(int id)
        {
            var items = await _repo.GetSpecific(id);
            return Ok(items.ToList());
        }

        [Route("GetAllCarts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemDTO>>> GetAll()
        {
            var items = await _repo.GetAll();
            return Ok(items.ToList());
        }

        //// GET: CustomerController/Details/5
        //[Route("GetCustomer")]
        //[HttpGet]
        //public async Task<ActionResult<Customer>> Details(int id)
        //{
        //    var customer = await _repo.Get(id);
        //    if (customer == null)
        //        return BadRequest("No such Customer");
        //    return Ok(customer);
        //}

        // GET: CustomerController/Create
        [HttpPost]
        public async Task<ActionResult<ShoppingCartItemDTO>> Add(ShoppingCartItemDTO cartItem)
        {
            var item = await _repo.Add(cartItem);
            if (item == null)
                return NotFound();
            return Created("Product added to cart", item);
        }


       
        [HttpPut]     
        public async Task<ActionResult<ShoppingCartItemDTO>> Edit(ShoppingCartItemDTO cartItem)
        {
            var cust = await _repo.Update(cartItem);
            if (cust == null)
                return NotFound();
            return Ok(cust);
        }


        [HttpDelete]
        public async Task<ActionResult<ShoppingCartItemDTO>> Delete(int id)
        {
            //try
            //{
            var item = await _repo.Delete(id);
            if (item == null)
                return NotFound();
            return Ok(item);
            //}
            //catch
            //{
            //    //return View();
            //}
        }
    }
}
