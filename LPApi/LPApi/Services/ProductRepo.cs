using LPApi.Models;

namespace LPApi.Services
{
    public class ProductRepo : IRepo<int, Product>
    {
        private readonly T3ShopContext _context;
        public ProductRepo(T3ShopContext context)
        {
            _context = context;
        }
        public ProductRepo()
        {

        }

        public Product Add(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Product GetT(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            return product;
        }

        public ICollection<Product> GetAll()
        {
            return _context.Products.ToList();
        }

    

        public Product Remove(int id)
        {
            var product = GetT(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return product;
        }

        public Product Update(Product item)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (product != null)
            {
                product.Name = item.Name;
                product.Category = item.Category;
                product.Price = item.Price;
                product.Qty = item.Qty;
                product.Pic = item.Pic;
                product.Description = item.Description;
                product.Status = item.Status;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            return product;
        }

        public ICollection<Product> GetSpecific(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetSpecificUsingObj(Product t)
        {
            return _context.Products.Where(x => x.Category == t.Category).ToList();
        }
    }
}
