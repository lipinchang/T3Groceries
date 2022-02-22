using Microsoft.AspNetCore.Mvc;
using T3PersonalWkSpcApp.Models;
using T3PersonalWkSpcApp.Services;

namespace T3PersonalWkSpcApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepo<int, ShopCartViewModel> _repo;
        private readonly IRepo<int, Product> _prodRepo;

        public CartController(IRepo<int, ShopCartViewModel> repo, IRepo<int, Product> prodRepo)
        {
            _repo = repo;
            _prodRepo = prodRepo;
        }

        //[HttpGet]
        public async Task<ActionResult> ViewCart()
        {

           

            if (HttpContext.Session.GetString("token") != null)
            {
                int userId = 1;   //harcode for now
                double total = 0;
                var cartFullDetails = new ShopCartViewModel();

                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                
                //var items = await _repo.GetSpecific(userId);
                var carts = await _repo.GetAll();
                var userCart = carts.Where(c => c.UserId == userId);
                foreach (var item in userCart)
                {
                    cartFullDetails.ProductId = item.ProductId;
                    var prod = await _prodRepo.Get(item.ProductId);
                    cartFullDetails.Name = prod.Name;
                    cartFullDetails.Pic = prod.Pic;
                    cartFullDetails.Qty=item.Qty;
                    cartFullDetails.Amount=item.Amount;
                    cartFullDetails.UserId = item.UserId;

                    total = total + item.Amount;
                    
                }
                //total is
                //ViewData["Message"] = total.ToString();
                ViewBag.Message = total.ToString();
                return View(cartFullDetails);
               
            }
            return View();
        }

        
        [HttpPost]
        public IActionResult Checkout(int[] ids, string command)
        {

            if (command.Equals("Checkout"))
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    //ids[i]
                    //add this entire row(using product id and user id) to order item table
                    //then delete this row at the end of payment transaction
                }
            }
            else
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    _repo.Remove(ids[i]);
                }
            }
            
            return RedirectToAction("ViewCart", "Cart");   //to be changed: should be redirected to confirm order page
        }

        //[HttpPost]
        //public IActionResult DeleteSelectedItems(int[] ids)
        //{
        //    for (int i = 0; i < ids.Length; i++)
        //    {
        //        _repo.Remove(ids[i]);
        //    }
        //    return RedirectToAction("ViewCart", "Cart");
        //}

        
    }
}
