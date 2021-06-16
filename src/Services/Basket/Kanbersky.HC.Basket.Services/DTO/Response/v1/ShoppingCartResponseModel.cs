using System.Collections.Generic;

namespace Kanbersky.HC.Basket.Services.DTO.Response
{
    public class ShoppingCartResponseModel
    {
        public string UserName { get; set; }

        public List<ShoppingCartResponseItemModel> Items { get; set; } = new List<ShoppingCartResponseItemModel>();
    }

    public class ShoppingCartResponseItemModel
    {
        public int Quantity { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
