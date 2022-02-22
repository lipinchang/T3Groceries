using LPApi.Models;
using LPApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepo<int, Product> _repo;
        public ProductController(IRepo<int, Product> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()     //notice action result
        {
            List<Product> products = _repo.GetAll().ToList();
            if (products.Count == 0)
                return BadRequest("No products found");
            return Ok(products);
        }

        [HttpGet]
        [Route("SingleProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _repo.GetT(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Route("ByCategory")]
        public ActionResult<IEnumerable<Product>> Get(Product prod)
        {
            var products = _repo.GetSpecificUsingObj(prod);
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            var prod = _repo.Add(product);
            if (prod != null)
            {
                return Created("Product Created", prod);
            }
            return BadRequest("Unable to create");
        }

        [HttpPut]
        public ActionResult<Product> Put(int id, Product prod)   //update    //in postman to put in parameter id(3) and body(one customer obj id 3 in json)
        {
            var product = _repo.Update(prod);
            if (product != null)
            {
                return Created("Updated", product);
            }

            return NotFound();
        }

        [HttpDelete]
        public ActionResult<Product> Delete(int id)
        {
            var product = _repo.Remove(id);
            if (product != null)
            {
                return NoContent();
            }
            return NotFound(product);
        }
    }
}
