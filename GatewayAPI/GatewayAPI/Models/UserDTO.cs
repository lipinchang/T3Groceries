namespace GatewayAPI.Models
{
    public class UserDTO
    {   //data transfer object
        public string Username { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? Token { get; set; }
        
    }
}
