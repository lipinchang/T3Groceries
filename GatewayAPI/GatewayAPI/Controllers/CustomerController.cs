using GatewayAPI.Models;
using GatewayAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]   //remember to add this
    public class CustomerController : ControllerBase
    {
        private readonly IRepo<int, Customer> _repo;

        public CustomerController(IRepo<int, Customer> repo)
        {
            _repo = repo;
        }

        // GET: CustomerController
        [Route("GetAllCustomers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> IndexAsync()
        {
            var customers = await _repo.GetAll();
            return Ok(customers.ToList());
        }

        // GET: CustomerController/Details/5
        [Route("GetCustomer")]
        [HttpGet]
        public async Task<ActionResult<Customer>> Details(int id)
        {
            var customer = await _repo.Get(id);
            if (customer == null)
                return BadRequest("No such Customer");
            return Ok(customer);
        }

        // GET: CustomerController/Create
        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            var cust = await _repo.Add(customer);
            if (cust == null)
                return NotFound();
            return Created("Customer created", customer);
        }


        // GET: CustomerController/Edit/5
        [HttpPut]
        public async Task<ActionResult<Customer>> Edit(int id, Customer customer)
        {
            var cust = await _repo.Update(customer);
            if (cust == null)
                return NotFound();
            return Ok(customer);
        }


        [HttpDelete]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            //try
            //{
                var cust = await _repo.Delete(id);
                if (cust == null)
                    return NotFound();
                return Ok(cust);
            //}
            //catch
            //{
            //    //return View();
            //}
        }
    }
}
