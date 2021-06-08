using System.Collections.Generic;

namespace Kanbersky.HC.Basket.Infrastructure.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
