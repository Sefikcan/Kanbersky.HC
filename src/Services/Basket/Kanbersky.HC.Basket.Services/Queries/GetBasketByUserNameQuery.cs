using Kanbersky.HC.Basket.Infrastructure.Entities;
using Kanbersky.HC.Basket.Services.DTO.Response;
using Kanbersky.HC.Core.Caching.Abstract;
using Kanbersky.HC.Core.Constants.Caching;
using Kanbersky.HC.Core.Mappings.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Basket.Services.Queries
{
    public class GetBasketByUserNameQuery : IRequest<ShoppingCartResponseModel>
    {
        public string UserName { get; set; }

        public GetBasketByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponseModel>
    {
        private readonly ICacheService _cacheService;
        private readonly IKanberskyMapping _mapping;

        public GetBasketByUserNameQueryHandler(ICacheService cacheService,
            IKanberskyMapping mapping)
        {
            _cacheService = cacheService;
            _mapping = mapping;
        }

        public async Task<ShoppingCartResponseModel> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var response = await _cacheService.GetAsync<ShoppingCart>(string.Format(CacheConstants.ShoppingCartCacheKey, request.UserName));
            return _mapping.Map<ShoppingCart, ShoppingCartResponseModel>(response);
        }
    }
}
