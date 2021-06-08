using Kanbersky.HC.Basket.Infrastructure.Entities;
using Kanbersky.HC.Basket.Services.DTO.Request;
using Kanbersky.HC.Basket.Services.DTO.Response;
using Kanbersky.HC.Core.Caching.Abstract;
using Kanbersky.HC.Core.Constants.Caching;
using Kanbersky.HC.Core.Mappings.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Basket.Services.Commands
{
    public class CreateBasketCommand : IRequest<ShoppingCartResponseModel>
    {
        public CreateShoppingCartRequestModel CreateShoppingCartRequest { get; set; }

        public CreateBasketCommand(CreateShoppingCartRequestModel createShoppingCartRequest)
        {
            CreateShoppingCartRequest = createShoppingCartRequest;
        }
    }

    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, ShoppingCartResponseModel>
    {
        private readonly ICacheService _cacheService;
        private readonly IKanberskyMapping _mapping;
        private const int AddShoppingCartCacheTime = 5;

        public CreateBasketCommandHandler(ICacheService cacheService,
            IKanberskyMapping mapping)
        {
            _cacheService = cacheService;
            _mapping = mapping;
        }

        public async Task<ShoppingCartResponseModel> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var mappedRequest = _mapping.Map<CreateShoppingCartRequestModel, ShoppingCart>(request.CreateShoppingCartRequest);
            var response = await _cacheService.AddAsync(key: string.Format(CacheConstants.ShoppingCartCacheKey, request.CreateShoppingCartRequest.UserName), data: mappedRequest, duration: AddShoppingCartCacheTime);

            return _mapping.Map<ShoppingCart, ShoppingCartResponseModel>(response);
        }
    }
}
