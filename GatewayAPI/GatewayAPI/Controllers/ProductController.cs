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
    public class ProductController : ControllerBase
    {
        private readonly IRepo<int, ProductDTO> _repo;

        public ProductController(IRepo<int, ProductDTO> repo)
        {
            _repo = repo;
        }

        [Route("GetAllProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> IndexAsync()
        {
            var products = await _repo.GetAll();
            return Ok(products.ToList());
        }

        // GET: CustomerController/Details/5
        [Route("GetProduct")]
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> Details(int id)
        {
            var prod = await _repo.Get(id);
            if (prod == null)
                return BadRequest("No such product");
            return Ok(prod);
        }

        // GET: CustomerController/Create
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO product)
        {
            var prod = await _repo.Add(product);
            if (prod == null)
                return NotFound();
            return Created("Product created", prod);
        }

        [Route("ByCategory")]
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Get(ProductDTO product)
        {
            var prod = await _repo.GetSpecificUsingObj(product);
            if (prod == null)
                return NotFound();
            return Created("Product category found", prod);
        }

        // GET: CustomerController/Edit/5
        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Edit(int id, ProductDTO product)
        {
            var prod = await _repo.Update(product);
            if (prod == null)
                return NotFound();
            return Ok(prod);
        }


        [HttpDelete]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            //try
            //{
            var prod = await _repo.Delete(id);
            if (prod == null)
                return NotFound();
            return Ok(prod);
            //}
            //catch
            //{
            //    //return View();
            //}
        }
    }
}
