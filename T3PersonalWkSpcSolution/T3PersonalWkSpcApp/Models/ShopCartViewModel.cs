namespace T3PersonalWkSpcApp.Models
{
    public class ShopCartViewModel
    {
        public int ShoppingCartId { get; set; }


        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }

        public int Qty { get; set; }
        public double Amount { get; set; }  //per item


        public int UserId { get; set; }
    }
}
