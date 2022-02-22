using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T3PersonalWkSpcApp.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }
        public int Qty { get; set; }


        
        public double Amount { get; set; }  //per item

        public int UserId { get; set; }




    }
}
