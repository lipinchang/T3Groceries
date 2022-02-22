using System.ComponentModel.DataAnnotations;

namespace GatewayAPI.Models
{
    public class User
    {
        
        
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
