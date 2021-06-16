using Kanbersky.HC.BFF.Services.DTO.Response.Catalog.v1;
using Kanbersky.HC.Core.Models;
using System.Threading.Tasks;

namespace Kanbersky.HC.BFF.Services.Clients.Abstract
{
    public interface IProductClientService
    {
        Task<BaseClientModel<CatalogResponseModel>> GetProductById(string id);
    }
}
