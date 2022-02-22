using T3PersonalWkSpcApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace T3PersonalWkSpcApp.Services
{
    public class ProductRepo : IRepo<int, Product>
    {
        private readonly HttpClient _httpClient;
        private string _token;
        public ProductRepo()
        {
            _httpClient = new HttpClient();
        }
        public void GetToken(string token)
        {
            _token = token;
        }

        public async Task<Product> Add(Product item)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);   //put token everywhere
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5148/api/Product", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<Product>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<Product> Get(int key)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                using (var response = await _httpClient.GetAsync("http://localhost:5148/api/Product/GetProduct?id=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<Product>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            using (_httpClient)
            {

                using (var response = await _httpClient.GetAsync("http://localhost:5148/api/Product/GetAllProducts"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<Product>>(responseText);
                        return products.ToList();
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetSpecific(int k)   //can use for category
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Remove(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                using (var response = await _httpClient.DeleteAsync("http://localhost:5148/api/Product?id=" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<Product>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<Product> Update(Product item)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync("http://localhost:5148/api/Product?id=" + item.ProductId, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<Product>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetSpecificUsingObj(Product product)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);   //put token everywhere

            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5148/api/Product/ByCategory" ,content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<Product>>(responseText);
                        return products.ToList();
                    }
                }

            }
            return null;
        }

        //public Product Add(Product item)
        //{
        //    _context.Products.Add(item);
        //    _context.SaveChanges();
        //    return item;
        //}

        //public Product Get(int id)
        //{
        //    Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
        //    return product;
        //}



        //public ICollection<Product> GetAll()
        //{
        //    return _context.Products.ToList();
        //}

        //public ICollection<Product> GetSpecific(int k)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Remove(int id)
        //{
        //    Product product = Get(id);
        //    _context.Products.Remove(product);
        //    _context.SaveChanges();
        //    return true;
        //}

        //public bool Update(Product item)
        //{
        //    Product product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId);
        //    if (product != null)
        //    {
        //        product.Name = item.Name;
        //        product.Category = item.Category;
        //        product.Price = item.Price;
        //        product.Qty = item.Qty;
        //        product.Pic = item.Pic;
        //        product.Description = item.Description;
        //        product.Status = item.Status;
        //        _context.Products.Update(product);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}


    }
}
