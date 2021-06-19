using Kanbersky.HC.Catalog.Infrastructure.Entities;
using Kanbersky.HC.Catalog.Services.DTO.Response.v1;
using Kanbersky.HC.Core.Mappings.Abstract;
using Kanbersky.HC.Core.Results.Exceptions.Concrete;
using Kanbersky.MongoDB.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Catalog.Services.Queries
{
    public class GetCatalogByIdQuery : IRequest<CatalogResponseModel>
    {
        public string Id { get; set; }

        public GetCatalogByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetCatalogByIdQueryHandler : IRequestHandler<GetCatalogByIdQuery, CatalogResponseModel>
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IKanberskyMapping _mapper;

        public GetCatalogByIdQueryHandler(IMongoRepository<Product> repository,
            IKanberskyMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CatalogResponseModel> Handle(GetCatalogByIdQuery request, CancellationToken cancellationToken = default)
        {
            var response = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<Product, CatalogResponseModel>(response);
        }
    }
}
