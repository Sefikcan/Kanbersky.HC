namespace Kanbersky.HC.BFF.Services.DTO.Request.Ordering
{
    public class CreateOrderRequestModel
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public int OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
