using Kanbersky.HC.BFF.Services.Clients.Abstract;
using Kanbersky.HC.BFF.Services.DTO.Request.Ordering;
using Kanbersky.HC.BFF.Services.DTO.Response.Ordering;
using System.Net.Http;
using Kanbersky.HC.Core.Extensions;
using System.Threading.Tasks;
using Kanbersky.HC.Core.Models;

namespace Kanbersky.HC.BFF.Services.Clients.Concrete
{
    public class OrderingClientService : IOrderingClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public OrderingClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BaseClientModel<OrderResponseModel>> AddOrder(CreateOrderRequestModel createOrderRequest)
        {
            _client = _httpClientFactory.CreateClient("Ordering");
            var response = await _client.PostAsJsonAsync("/api/v1/orders", createOrderRequest);
            return await response.ReadContentAs<BaseClientModel<OrderResponseModel>>();
        }
    }
}
