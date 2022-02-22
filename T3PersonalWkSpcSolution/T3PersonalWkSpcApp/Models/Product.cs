using System.ComponentModel.DataAnnotations;

namespace T3PersonalWkSpcApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        [Range(1, 50, ErrorMessage = "Invalid price")]
        public double Price { get; set; }
        [Range(0, 20, ErrorMessage = "Invalid quantity")]
        public int Qty { get; set; }
        [Display(Name = "Upload Image File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Pic { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
