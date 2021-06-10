using Kanbersky.HC.Core.Results.ApiResponses.Concrete;
using Kanbersky.HC.Ordering.Services.Commands;
using Kanbersky.HC.Ordering.Services.DTO.Request.v1;
using Kanbersky.HC.Ordering.Services.DTO.Response.v1;
using Kanbersky.HC.Ordering.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Kanbersky.HC.Ordering.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/orders")]
    [ApiController]
    public class OrdersController : KanberskyControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Get Order By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(OrderResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponseModel>> GetOrderById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetOrderByIdQuery(id));
            return ApiOk(response);
        }

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="createOrderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(OrderResponseModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderResponseModel>> AddOrder([FromBody] CreateOrderRequestModel createOrderRequest)
        {
            var response = await _mediator.Send(new CreateOrderCommand(createOrderRequest));
            return ApiCreated(response);
        }
    }
}
