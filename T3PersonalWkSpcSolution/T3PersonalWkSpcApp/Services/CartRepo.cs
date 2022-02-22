using T3PersonalWkSpcApp.Models;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace T3PersonalWkSpcApp.Services
{
    public class CartRepo : IRepo<int, ShoppingCartItem>
    {
        private readonly HttpClient _httpClient;
        private string _token;

        public CartRepo()
        {
            _httpClient = new HttpClient();
        }

        public void GetToken(string token)
        {
            _token = token;
        }

        public async Task<ShoppingCartItem> Add(ShoppingCartItem item)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);   //put token everywhere
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5148/api/Cart", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItem = JsonConvert.DeserializeObject<ShoppingCartItem>(responseText);
                        return cartItem;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<ShoppingCartItem>> Get(int key)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            //using (_httpClient)
            //{
            //    using (var response = await _httpClient.GetAsync("http://localhost:5148/api/Cart/GetShopperCart?id=" + key))
            //    {
            //        if (response.IsSuccessStatusCode)
            //        {
            //            string responseText = await response.Content.ReadAsStringAsync();
            //            var cartItems = JsonConvert.DeserializeObject<ShoppingCartItem>(responseText);
            //            return cartItems;
            //        }
            //    }

            //}
            return null;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAll()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                using (var response = await _httpClient.GetAsync("http://localhost:5148/api/Cart/GetAllCarts"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var carts = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(responseText);
                        return carts.ToList();
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetSpecific(int key)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                using (var response = await _httpClient.GetAsync("http://localhost:5148/api/Cart/GetShopperCart?id=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItems = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(responseText);
                        return cartItems.ToList();
                    }
                }

            }
            return null;
        }

        //public async Task<IEnumerable<ShoppingCartItem>> GetAllCarts()
        //{
        //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        //    using (_httpClient)
        //    {
        //        using (var response = await _httpClient.GetAsync("http://localhost:5148/api/Cart/GetAllCarts"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string responseText = await response.Content.ReadAsStringAsync();
        //                var carts = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(responseText);
        //                return carts.ToList();
        //            }
        //        }

        //    }
        //    return null;
        //}



        public async Task<ShoppingCartItem> Remove(int key)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                using (var response = await _httpClient.DeleteAsync("http://localhost:5148/api/Cart?id=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItem = JsonConvert.DeserializeObject<ShoppingCartItem>(responseText);
                        return cartItem;
                    }
                }

            }
            return null;
        }

        public async Task<ShoppingCartItem> Update(ShoppingCartItem item)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync("http://localhost:5148/api/Cart" , content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItem = JsonConvert.DeserializeObject<ShoppingCartItem>(responseText);
                        return cartItem;
                    }
                }

            }
            return null;
        }

        Task<ShoppingCartItem> IRepo<int, ShoppingCartItem>.Get(int k)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingCartItem>> GetSpecificUsingObj(ShoppingCartItem t)
        {
            throw new NotImplementedException();
        }

        //public ShoppingCartItem Add(ShoppingCartItem item)
        //{
        //    _context.ShoppingCartItems.Add(item);
        //    _context.SaveChanges();
        //    return item;
        //}

        //public ICollection<ShoppingCartItem> GetSpecific(int id)
        //{
        //    return _context.ShoppingCartItems.Include(x => x.Product).Where(x => x.UserId == id).ToList();

        //}

        //public ICollection<ShoppingCartItem> Get()
        //{
        //    throw new NotImplementedException();
        //}

        //public ICollection<ShoppingCartItem> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Remove(int id)
        //{
        //    ShoppingCartItem shoppingCartItem = Get(id);
        //    _context.ShoppingCartItems.Remove(shoppingCartItem);
        //    _context.SaveChanges();
        //    return true;
        //}


        //public bool Update(ShoppingCartItem item)
        //{
        //    ShoppingCartItem shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.UserId == item.UserId);
        //    if (shoppingCartItem != null)
        //    {
        //        shoppingCartItem.Qty = item.Qty;
        //        shoppingCartItem.Amount = item.Amount;

        //        _context.ShoppingCartItems.Update(shoppingCartItem);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        //public ShoppingCartItem Get(int id)
        //{
        //    ShoppingCartItem shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(x => x.ProductId == id);
        //    return shoppingCartItem;
        //}

    }
}
