using Newtonsoft.Json;
using System.Text;
using T3PersonalWkSpcApp.Models;

namespace T3PersonalWkSpcApp.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        public LoginService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<User> Register(User user)
        {
            //call to api
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5148/api/User/Register", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var user1 = JsonConvert.DeserializeObject<User>(responseText);
                        return user1;
                    }
                }

            }

            return null;
        }

        public async Task<User> Login(User user)
        {
            using (_httpClient)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.PostAsync("http://localhost:5148/api/User/Login", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var user1 = JsonConvert.DeserializeObject<User>(responseText);
                        return user1;
                    }
                }
            }
            return null;
        }

        public async Task<User> GetUserDetails (string username)
        {
            using (_httpClient)
            {
                using (var response = await _httpClient.GetAsync("http://localhost:5148/api/User/GetUser?username=" + username))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseText = await response.Content.ReadAsStringAsync();
                        var user1 = JsonConvert.DeserializeObject<User>(responseText);
                        return user1;
                    }
                }

            }
            return null;
        }
    }
}
