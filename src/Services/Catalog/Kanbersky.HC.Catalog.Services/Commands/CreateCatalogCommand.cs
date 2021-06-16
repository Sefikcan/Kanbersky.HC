using Kanbersky.HC.Catalog.Infrastructure.Entities;
using Kanbersky.HC.Catalog.Services.DTO.Request.v1;
using Kanbersky.HC.Catalog.Services.DTO.Response.v1;
using Kanbersky.HC.Core.Mappings.Abstract;
using Kanbersky.MongoDB.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Catalog.Services.Commands
{
    public class CreateCatalogCommand : IRequest<CatalogResponseModel>
    {
        public CreateCatalogRequestModel CreateCatalogRequest { get; set; }

        public CreateCatalogCommand(CreateCatalogRequestModel createCatalogRequest)
        {
            CreateCatalogRequest = createCatalogRequest;
        }
    }

    public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommand, CatalogResponseModel>
    {
        private readonly IMongoRepository<Product> _repository;
        private readonly IKanberskyMapping _mapper;

        public CreateCatalogCommandHandler(IMongoRepository<Product> repository,
            IKanberskyMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CatalogResponseModel> Handle(CreateCatalogCommand request, CancellationToken cancellationToken = default)
        {
            var requestModel = _mapper.Map<CreateCatalogRequestModel, Product>(request.CreateCatalogRequest);
            await _repository.InsertAsync(requestModel);
            return _mapper.Map<CreateCatalogRequestModel, CatalogResponseModel>(request.CreateCatalogRequest);
        }
    }
}
