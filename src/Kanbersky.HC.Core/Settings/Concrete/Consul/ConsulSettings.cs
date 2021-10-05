using Kanbersky.HC.Core.Settings.Abstract;

namespace Kanbersky.HC.Core.Settings.Concrete.Consul
{
    /// <summary>
    /// Consul Settings
    /// </summary>
    public class ConsulSettings : ISettings
    {
        /// <summary>
        /// Consul Uri
        /// </summary>
        public string Url { get; set; }
    }
}
