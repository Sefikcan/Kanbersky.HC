namespace Kanbersky.HC.Ordering.Services.DTO.Response.v1
{
    public class OrderResponseModel
    {
        public int Id { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public int OrderStatus { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
