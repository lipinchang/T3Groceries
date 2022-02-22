using GatewayAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace GatewayAPI.Services
{
    public class ProductRepo : IRepo<int, ProductDTO>
    {
        private readonly HttpClient _httpClient;

        public ProductRepo()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ProductDTO> Add(ProductDTO item)
        {
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5041/api/Product", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductDTO>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<ProductDTO> Delete(int key)
        {
            using (_httpClient)
            {

                using (var response = await _httpClient.DeleteAsync("http://localhost:5041/api/Product?id=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductDTO>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<ProductDTO> Get(int key)
        {
            using (_httpClient)
            {
                using (var response = await _httpClient.GetAsync("http://localhost:5041/api/Product/SingleProduct?id=" + key))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductDTO>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            using (_httpClient)
            {

                using (var response = await _httpClient.GetAsync("http://localhost:5041/api/Product"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<ProductDTO>>(responseText);
                        return products.ToList();
                    }
                }

            }
            return null;
        }

        public Task<IEnumerable<ProductDTO>> GetSpecific(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDTO>> GetSpecificUsingObj(ProductDTO product)
        {
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5041/api/Product/ByCategory", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<ProductDTO>>(responseText);
                        return products.ToList();
                    }
                }

            }
            return null;
        }

        public async Task<ProductDTO> Update(ProductDTO item)
        {
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PutAsync("http://localhost:5041/api/Product?id=" + item.ProductId, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductDTO>(responseText);
                        return product;
                    }
                }

            }
            return null;
        }
    }
}
