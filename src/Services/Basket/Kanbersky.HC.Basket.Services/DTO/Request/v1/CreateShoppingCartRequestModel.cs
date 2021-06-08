using System.Collections.Generic;

namespace Kanbersky.HC.Basket.Services.DTO.Request
{
    public class CreateShoppingCartRequestModel
    {
        public string UserName { get; set; }

        public List<CreateShoppingCartItemRequestModel> Items { get; set; } = new List<CreateShoppingCartItemRequestModel>();
    }

    public class CreateShoppingCartItemRequestModel
    {
        public int Quantity { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
