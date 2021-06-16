using Kanbersky.HC.Catalog.Services.Commands;
using Kanbersky.HC.Catalog.Services.DTO.Request.v1;
using Kanbersky.HC.Catalog.Services.DTO.Response.v1;
using Kanbersky.HC.Catalog.Services.Queries;
using Kanbersky.HC.Core.Results.ApiResponses.Concrete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Kanbersky.HC.Catalog.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/catalogs")]
    [ApiController]
    public class CatalogsController : KanberskyControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CatalogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Product Info By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(CatalogResponseModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CatalogResponseModel>> GetProductById([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetCatalogByIdQuery(id));
            return ApiOk(response);
        }

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<CatalogResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CatalogResponseModel>>> GetAllProducts()
        {
            var response = await _mediator.Send(new GetAllCatalogQuery());
            return ApiOk(response);
        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="createCatalogRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(CatalogResponseModel), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<CatalogResponseModel>> AddProduct([FromBody] CreateCatalogRequestModel createCatalogRequest)
        {
            var response = await _mediator.Send(new CreateCatalogCommand(createCatalogRequest));
            return ApiCreated(response);
        }
    }
}
