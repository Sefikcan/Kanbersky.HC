using Kanbersky.HC.BFF.Services.Abstract;
using Kanbersky.HC.BFF.Services.DTO.Request.Ordering;
using Kanbersky.HC.BFF.Services.DTO.Response.Ordering;
using Kanbersky.HC.Core.Results.ApiResponses.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kanbersky.HC.BFF.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/bff/orders")]
    [ApiController]
    public class OrdersController : KanberskyControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="orderService"></param>
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="createOrderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponseModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel createOrderRequest)
        {
            var response = await _orderService.CreateOrder(createOrderRequest);
            return ApiCreated(response);
        }
    }
}
