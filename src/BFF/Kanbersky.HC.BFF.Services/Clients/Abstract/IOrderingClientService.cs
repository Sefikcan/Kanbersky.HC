using Kanbersky.HC.BFF.Services.DTO.Request.Ordering;
using Kanbersky.HC.BFF.Services.DTO.Response.Ordering;
using Kanbersky.HC.Core.Models;
using System.Threading.Tasks;

namespace Kanbersky.HC.BFF.Services.Clients.Abstract
{
    public interface IOrderingClientService
    {
        Task<BaseClientModel<OrderResponseModel>> AddOrder(CreateOrderRequestModel createOrderRequest);
    }
}
