using Kanbersky.HC.BFF.Services.Clients.Abstract;
using Kanbersky.HC.BFF.Services.DTO.Response.Catalog.v1;
using Kanbersky.HC.Core.Extensions;
using Kanbersky.HC.Core.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kanbersky.HC.BFF.Services.Clients.Concrete
{
    public class ProductClientService : IProductClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public ProductClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BaseClientModel<CatalogResponseModel>> GetProductById(string id)
        {
            _client = _httpClientFactory.CreateClient("Catalog");
            var response = await _client.GetAsync($"/api/v1/catalogs/{id}");
            return await response.ReadContentAs<BaseClientModel<CatalogResponseModel>>();
        }
    }
}
