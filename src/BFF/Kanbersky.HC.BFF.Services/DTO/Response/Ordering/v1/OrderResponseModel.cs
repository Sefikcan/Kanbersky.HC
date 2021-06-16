namespace Kanbersky.HC.BFF.Services.DTO.Response.Ordering
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
