using System.ComponentModel.DataAnnotations;

namespace LPApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
