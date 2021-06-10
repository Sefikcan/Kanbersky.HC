namespace Kanbersky.HC.Ordering.Services.DTO.Request.v1
{
    public class CreateOrderRequestModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
