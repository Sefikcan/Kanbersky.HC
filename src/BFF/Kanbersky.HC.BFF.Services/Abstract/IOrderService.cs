using Kanbersky.HC.BFF.Services.DTO.Request.Ordering;
using Kanbersky.HC.BFF.Services.DTO.Response.Ordering;
using System.Threading.Tasks;

namespace Kanbersky.HC.BFF.Services.Abstract
{
    public interface IOrderService
    {
        Task<OrderResponseModel> CreateOrder(CreateOrderRequestModel createOrderRequest);
    }
}
