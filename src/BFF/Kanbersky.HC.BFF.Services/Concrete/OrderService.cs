using Kanbersky.HC.BFF.Services.Abstract;
using Kanbersky.HC.BFF.Services.Clients.Abstract;
using Kanbersky.HC.BFF.Services.DTO.Request.Ordering;
using Kanbersky.HC.BFF.Services.DTO.Response.Ordering;
using Kanbersky.HC.Core.Results.Exceptions.Concrete;
using System.Threading.Tasks;

namespace Kanbersky.HC.BFF.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IProductClientService _productClient;
        private readonly IOrderingClientService _orderingClient;

        public OrderService(IProductClientService productClient,
            IOrderingClientService orderingClient)
        {
            _productClient = productClient;
            _orderingClient = orderingClient;
        }

        public async Task<OrderResponseModel> CreateOrder(CreateOrderRequestModel createOrderRequest)
        {
            var product = await _productClient.GetProductById(createOrderRequest.ProductId);
            if (product?.Result == null)
                throw new NotFoundException("Product not found!");

            var order = await _orderingClient.AddOrder(createOrderRequest);
            return order.Result;
        }
    }
}
