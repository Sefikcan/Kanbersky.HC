using Kanbersky.HC.Core.Mappings.Abstract;
using Kanbersky.HC.Core.Results.Exceptions.Concrete;
using Kanbersky.HC.Ordering.Infrastructure.DataAccess.EntityFramework;
using Kanbersky.HC.Ordering.Services.DTO.Response.v1;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Ordering.Services.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponseModel>
    {
        public int Id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponseModel>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IKanberskyMapping _mapper;

        public GetOrderByIdQueryHandler(IKanberskyMapping mapper,
            OrderDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderResponseModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (order == null)
            {
                throw new NotFoundException("Order not found!");
            }

            return _mapper.Map<Infrastructure.Entities.Order, OrderResponseModel>(order);
        }
    }
}
