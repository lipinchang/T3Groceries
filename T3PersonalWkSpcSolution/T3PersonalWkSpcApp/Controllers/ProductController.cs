using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using T3PersonalWkSpcApp.Models;
using T3PersonalWkSpcApp.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace T3PersonalWkSpcApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepo<int, Product> _repo;
        private readonly IRepo<int, ShoppingCartItem> _cartRepo;
        public ProductController(IRepo<int, Product> repo, IRepo<int, ShoppingCartItem> cartRepo)
        {
            _repo = repo;
            _cartRepo = cartRepo;
        }
        public async Task<ActionResult> IndexAdmin()
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var products = await _repo.GetAll();
                return View(products.ToList());
            }

            return View();
        }
        public async Task<ActionResult> IndexShopper()
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var products = Categories();
                //var products = await _repo.GetAll();
                return View(products);
            }

            return View();
        }

        public async Task<ActionResult> IndexShopperCategory(string id)
        {
            var product = new Product();
            product.Category = id;
            product.Name = "";
            product.Price = 1;
            product.Pic = "nil";
            product.Description = "";
            product.Status = "";
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                ViewBag.CategoryName = id;
                var products = await _repo.GetSpecificUsingObj(product);
                return View(products);
            }

            return View();
        }

        public async Task<ActionResult> DetailsAdmin(int id)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var product = await _repo.Get(id);
                return View(product);
            }

            return View();

        }
        public async Task<ActionResult> DetailsShopper(int id)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var product = await _repo.Get(id);
                return View(product);
            }

            return View();
        }
        public async Task<ActionResult> Create()
        {
            ViewBag.Category = GetProductCategories();
            //ViewBag.Pic = GetProductPic();
            return View(new Product());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, IFormFile Pic)
        {
            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
            ViewBag.Category = GetProductCategories();

            try
            {
                if (HttpContext.Session.GetString("token") != null)
                {
                    string token = HttpContext.Session.GetString("token");
                    _repo.GetToken(token);

                    if (Pic == null)
                    {
                        //ModelState.AddModelError("Pic", "Please upload file");
                        ModelState.AddModelError(string.Empty, "Please upload file");
                        //ModelState.AddModelError("name", "Student Name Already Exists.");
                        return View();
                    }

                    if (Pic != null)
                    {
                        string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(Pic.FileName);
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageName);

                        using (var stream = new FileStream(pathToSave, FileMode.Create))
                        {
                            Pic.CopyTo(stream);
                            product.Pic = "/img/" + ImageName;
                        }
                        await _repo.Add(product);
                        return RedirectToAction("IndexAdmin", "Product");
                    }
                    

                }
                return View();

            }
            catch
            {
                return View();
            }

        }
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Category = GetProductCategories();

            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var product = await _repo.Get(id);
                return View(product);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Product product, IFormFile NewPic)
        {
            ViewBag.Category = GetProductCategories();
            try
            {
                if (HttpContext.Session.GetString("token") != null)
                {
                    string token = HttpContext.Session.GetString("token");
                    _repo.GetToken(token);

                    if (NewPic == null)   //file upload has nothing, nothing to update, update other fields
                    {
                        //ModelState.AddModelError(string.Empty, "Please upload file");
                        await _repo.Update(product);
                        return RedirectToAction("IndexAdmin", "Product");
                    }
                    if (NewPic != null)   //has file upload to update
                    {
                        string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(NewPic.FileName);
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageName);

                        using (var stream = new FileStream(pathToSave, FileMode.Create))
                        {
                            NewPic.CopyTo(stream);
                            product.Pic = "/img/" + ImageName;
                        }
                        await _repo.Update(product);
                        return RedirectToAction("IndexAdmin", "Product");
                    }
                    //await _repo.Update(product);
                    //return RedirectToAction("IndexAdmin");
                }
                return View();
            }
            catch
            {
                return View();
            }
            
        }
        public async Task<ActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var product = await _repo.Get(id);
                return View(product);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product product)
        { 
            try
            {
                if (HttpContext.Session.GetString("token") != null)
                {
                    string token = HttpContext.Session.GetString("token");
                    _repo.GetToken(token);
                    await _repo.Remove(id);                  
                }
                return RedirectToAction("IndexAdmin");
            }
            catch
            {
                return View();
            }
        }

        
        public async Task<IEnumerable<ShoppingCartItem>> GetUserItems(int userId)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                string token = HttpContext.Session.GetString("token");
                _repo.GetToken(token);
                var items = await _cartRepo.GetSpecific(userId);
                return items;
            }
            return null;
        }

        
        public async Task<ActionResult> QtyOfProduct(int Quantity, Product product)
        {
            int userId = 1;   //get session
            int qty = Quantity;   //put to session
            int productId = product.ProductId;   //put to session
            double price = product.Price;
            string pic=product.Pic;
            try
            {
                if (HttpContext.Session.GetString("token") != null)
                {
                    string token = HttpContext.Session.GetString("token");
                    _repo.GetToken(token);


                    //var carts = await _cartRepo.GetAll();
                    //var userCart = carts.Where(c => c.UserId == userId);    
                    //foreach (var item in userCart)
                    //{
                    //    if (userCart.Where(x => x.ProductId == item.ProductId) != null)  //means have
                    //    {
                            ShoppingCartItem item1 = new ShoppingCartItem();
                            item1.Qty = qty;//item1.Qty = item.Qty + qty;
                            item1.Amount = price * qty;  //item1.Amount = price * item.Qty;
                            item1.ProductId = productId;
                            item1.UserId = userId;
                            await _cartRepo.Update(item1);



                            //ShoppingCartItem item = new ShoppingCartItem();
                            //item.Qty = item.Qty + qty;
                            //item.Amount = price * item.Qty;
                            //item.ProductId = productId;
                            //item.UserId = userId;
                            //await _cartRepo.Update(item);
                    //    }


                    //    else
                    //    {
                    //        ShoppingCartItem newItem = new ShoppingCartItem();
                    //        newItem.ProductId = productId;
                    //        newItem.Qty = qty;
                    //        newItem.Amount = price * qty;
                    //        newItem.UserId = userId;


                    //        //Product productItem = new Product();
                    //        //newItem.Product.Pic = productItem;
                    //        //newItem.Product.Name = product.Name;
                    //        //newItem.Product.Price = product.Price;
                    //        //newItem.Product.Status = product.Status;
                    //        //newItem.Product.Category = product.Category;
                    //        //newItem.Product.Description = product.Description;
                    //        await _cartRepo.Add(newItem);
                    //    }
                    //}

                    return RedirectToAction("ViewCart", "Cart");
                }
                return RedirectToAction("IndexAdmin");
            }
            catch
            {
                return View();
            }


            

            
        }

        IEnumerable<SelectListItem> GetProductCategories()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "Bakery and Bread", Value = "Bakery and Bread" });
            category.Add(new SelectListItem { Text = "Seafood", Value = "Seafood" });
            category.Add(new SelectListItem { Text = "Meat", Value = "Meat" });
            category.Add(new SelectListItem { Text = "Pasta and Rice", Value = "Pasta and Rice" });
            category.Add(new SelectListItem { Text = "Oils, Sauces and Condiments", Value = "Oils, Sauces and Condiments" });
            category.Add(new SelectListItem { Text = "Cereals and Breakfast Foods", Value = "Cereals and Breakfast Foods" });
            category.Add(new SelectListItem { Text = "Soups and Canned Goods", Value = "Soups and Canned Goods" });
            category.Add(new SelectListItem { Text = "Frozen Foods", Value = "Frozen Foods" });
            category.Add(new SelectListItem { Text = "Dairy", Value = "Dairy" });

            return category;
        }

        List<Product> Categories()
        {
            List<Product> categories= new List<Product>();
            categories.Add(new Product { Category="Bakery and Bread", Pic= "/img/png-transparent-bakery-pita-loaf-small-bread-pan-baked-goods-food-baking-thumbnail.png" });
            categories.Add(new Product { Category= "Seafood", Pic = "/img/fresh-seafood-png-13-Transparent-Images.png" });
            categories.Add(new Product { Category= "Meat", Pic = "/img/215-2157327_pork-png-clipart-meat-and-meat-products-transparent.png" });
            categories.Add(new Product { Category= "Pasta and Rice", Pic = "/img/png-transparent-pasta-breakfast-cereal-food-cereal-gluten-grocery-store-rice.png" });
            categories.Add(new Product { Category= "Oils, Sauces and Condiments", Pic = "/img/169-1690862_condiments-and-spreads-like-tahini-paste-thousand-bottle.png" });
            categories.Add(new Product { Category= "Cereals", Pic = "/img/70-708520_cereal-bowl-png-bowl-of-cereal-png.png" });
            categories.Add(new Product { Category= "Soups and Canned Goods", Pic = "/img/istockphoto-1062795532-612x612.jpg" });
            categories.Add(new Product { Category= "Frozen Foods", Pic = "/img/c7e5b6572e75509a67428531f3efde0e.png" });
            categories.Add(new Product { Category= "Dairy", Pic = "/img/160-1608339_transparent-dairy-png-milk-products-images-png-png.png" });

            return categories;
        }

        //IEnumerable<SelectListItem> GetProductPic()
        //{
        //    List<SelectListItem> pics = new List<SelectListItem>();
        //    pics.Add(new SelectListItem { Text = "Green Bell Pepper", Value = "/images/green.png" });
        //    pics.Add(new SelectListItem { Text = "Red Bell Pepper", Value = "/images/red.png" });
        //    pics.Add(new SelectListItem { Text = "Stacking Wooden Hoop", Value = "/images/teddy.png" });

        //    return pics;
        //}

    }
}
