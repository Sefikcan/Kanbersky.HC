using Kanbersky.MongoDB.Models;

namespace Kanbersky.HC.Catalog.Infrastructure.Entities
{
    public class Product : BaseMongoEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
