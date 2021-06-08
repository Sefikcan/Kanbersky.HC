using Kanbersky.HC.Basket.Services.Commands;
using Kanbersky.HC.Basket.Services.DTO.Request;
using Kanbersky.HC.Basket.Services.DTO.Response;
using Kanbersky.HC.Basket.Services.Queries;
using Kanbersky.HC.Core.Results.ApiResponses.Concrete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Kanbersky.HC.Basket.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/baskets")]
    [ApiController]
    public class BasketsController : KanberskyControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Basket Info By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{userName}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ShoppingCartResponseModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponseModel>> GetBasketByUserName(string userName)
        {
            var response = await _mediator.Send(new GetBasketByUserNameQuery(userName));
            return ApiOk(response);
        }

        /// <summary>
        /// Add Basket
        /// </summary>
        /// <param name="createShoppingCartRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ShoppingCartResponseModel), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ShoppingCartResponseModel>> AddBasket([FromBody]CreateShoppingCartRequestModel createShoppingCartRequestModel)
        {
            var response = await _mediator.Send(new CreateBasketCommand(createShoppingCartRequestModel));
            return ApiCreated(response);
        }
    }
}
