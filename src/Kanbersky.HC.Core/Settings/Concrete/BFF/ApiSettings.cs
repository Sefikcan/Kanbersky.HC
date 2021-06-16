using Kanbersky.HC.Core.Settings.Abstract;

namespace Kanbersky.HC.Core.Settings.Concrete.BFF
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiSettings : ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string CatalogUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderingUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BasketUrl { get; set; }
    }
}
