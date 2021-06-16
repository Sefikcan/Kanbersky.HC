using Newtonsoft.Json;

namespace Kanbersky.HC.BFF.Services.DTO.Response.Catalog.v1
{
    public class CatalogResponseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }
    }
}
