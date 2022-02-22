using System.ComponentModel.DataAnnotations;

namespace T3PersonalWkSpcApp.Models
{
    public class User
    {
        //[Key]
        //public int UserId { get; set; }
        //public string Role { get; set; }

        //public string Username { get; set; }
        //public string Password { get; set; }
        


        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        //public byte[] PasswordHash { get; set; }
        public string Token { get; set; }

    }
}
