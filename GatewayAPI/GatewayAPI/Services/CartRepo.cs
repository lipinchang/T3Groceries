using GatewayAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace GatewayAPI.Services
{
    public class CartRepo : IRepo<int, ShoppingCartItemDTO>
    {
        private readonly HttpClient _httpClient;

        public CartRepo()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ShoppingCartItemDTO> Add(ShoppingCartItemDTO item)
        {
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5041/api/Cart", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItem = JsonConvert.DeserializeObject<ShoppingCartItemDTO>(responseText);
                        return cartItem;
                    }
                }

            }
            return null;
        }

        public async Task<ShoppingCartItemDTO> Delete(int key)   //with product id
        {
            using (_httpClient)
            {

                using (var response = await _httpClient.DeleteAsync("http://localhost:5041/api/Cart?id=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItem = JsonConvert.DeserializeObject<ShoppingCartItemDTO>(responseText);
                        return cartItem;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<ShoppingCartItemDTO>> GetSpecific(int key)   //get userid
        {
            using (_httpClient)
            {
                using (var response = await _httpClient.GetAsync("http://localhost:5041/api/Cart?userId=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItems = JsonConvert.DeserializeObject<List<ShoppingCartItemDTO>>(responseText);
                        return cartItems;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<ShoppingCartItemDTO>> GetAll()
        {
            using (_httpClient)
            {

                using (var response = await _httpClient.GetAsync("http://localhost:5041/api/Cart"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var carts = JsonConvert.DeserializeObject<List<ShoppingCartItemDTO>>(responseText);
                        return carts.ToList();
                    }
                }

            }
            return null;
        }

        public async Task<ShoppingCartItemDTO> Update(ShoppingCartItemDTO item)   //no use yet
        {
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync("http://localhost:5041/api/Cart"  , content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var cartItem = JsonConvert.DeserializeObject<ShoppingCartItemDTO>(responseText);
                        return cartItem;
                    }
                }

            }
            return null;
        }

        public Task<ShoppingCartItemDTO> Get(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingCartItemDTO>> GetSpecificUsingObj(ShoppingCartItemDTO t)
        {
            throw new NotImplementedException();
        }
    }
}
