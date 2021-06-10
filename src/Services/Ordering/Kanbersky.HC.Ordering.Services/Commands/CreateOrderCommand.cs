using Kanbersky.HC.Core.Enums;
using Kanbersky.HC.Core.Mappings.Abstract;
using Kanbersky.HC.Core.Results.Exceptions.Concrete;
using Kanbersky.HC.Ordering.Infrastructure.DataAccess.EntityFramework;
using Kanbersky.HC.Ordering.Services.DTO.Request.v1;
using Kanbersky.HC.Ordering.Services.DTO.Response.v1;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Ordering.Services.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponseModel>
    {
        public CreateOrderRequestModel CreateOrderRequest { get; set; }

        public CreateOrderCommand(CreateOrderRequestModel createOrderRequest)
        {
            CreateOrderRequest = createOrderRequest;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponseModel>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IKanberskyMapping _mapper;

        public CreateOrderCommandHandler(IKanberskyMapping mapper,
            OrderDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderResponseModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<CreateOrderRequestModel, Infrastructure.Entities.Order>(request.CreateOrderRequest);
            order.OrderStatus = (int)OrderStatus.Pending;

            var response = await _dbContext.AddAsync(order, cancellationToken);
            if (await _dbContext.SaveChangesAsync(cancellationToken) <= 0)
            {
                throw new BadRequestException("Create Order Failed!");
            }

            return _mapper.Map<Infrastructure.Entities.Order, OrderResponseModel>(response.Entity);
        }
    }
}
