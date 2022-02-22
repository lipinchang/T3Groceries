using LPApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LPApi.Services
{
    public class CartRepo : IRepo<int, ShoppingCartItem>
    {
        private readonly T3ShopContext _context;
        public CartRepo(T3ShopContext context)
        {
            _context = context;
        }
        public ShoppingCartItem Add(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public ICollection<ShoppingCartItem> GetSpecific(int userId)  //get user shopping cart
        {
            //return _context.ShoppingCartItems.Include(x => x.Product).Where(x => x.UserId == userId).ToList();
            return _context.ShoppingCartItems.Where(x => x.UserId == userId).ToList();

        }

        public ICollection<ShoppingCartItem> GetAll()
        {
            return _context.ShoppingCartItems.ToList();
        }

        public ShoppingCartItem Remove(int id)
        {
            var shoppingCartItem = GetT(id);
            if(shoppingCartItem != null)
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
                _context.SaveChanges();
            }
            return shoppingCartItem;
        }


        public ShoppingCartItem Update(ShoppingCartItem item)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.UserId == item.UserId && x.ProductId == item.ProductId);
            if (shoppingCartItem != null)
            {
                shoppingCartItem.Qty = item.Qty;
                shoppingCartItem.Amount = item.Amount;

                _context.ShoppingCartItems.Update(shoppingCartItem);
                _context.SaveChanges();
            }
            return shoppingCartItem;
        }

        public ShoppingCartItem GetT(int id)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.ProductId == id);
            return shoppingCartItem;
        }

        public ICollection<ShoppingCartItem> GetSpecificUsingObj(ShoppingCartItem t)
        {
            throw new NotImplementedException();
        }
    }
}
