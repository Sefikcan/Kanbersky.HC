using Kanbersky.HC.Core.Settings.Abstract;

namespace Kanbersky.HC.Core.Settings.Concrete.CircuitBreaker
{
    /// <summary>
    /// 
    /// </summary>
    public class CircuitBreakerSettings : ISettings
    {
        /// <summary>
        /// 
        /// </summary>
        public int HandledEventsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DurationOfBreak { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RetryCount { get; set; }
    }
}
