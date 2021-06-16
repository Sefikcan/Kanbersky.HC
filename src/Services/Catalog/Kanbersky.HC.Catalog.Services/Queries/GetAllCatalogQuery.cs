using Kanbersky.HC.Catalog.Infrastructure.Entities;
using Kanbersky.HC.Catalog.Services.DTO.Response.v1;
using Kanbersky.HC.Core.Mappings.Abstract;
using Kanbersky.MongoDB.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Catalog.Services.Queries
{
    public class GetAllCatalogQuery : IRequest<List<CatalogResponseModel>>
    {
    }

    public class GetAllCatalogQueryHandler : IRequestHandler<GetAllCatalogQuery, List<CatalogResponseModel>>
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IKanberskyMapping _mapper;

        public GetAllCatalogQueryHandler(IMongoRepository<Product> repository,
            IKanberskyMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CatalogResponseModel>> Handle(GetAllCatalogQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.FindAllAsync();
            return _mapper.Map<List<Product>, List<CatalogResponseModel>>(response.ToList());
        }
    }
}
