using Kanbersky.HC.Core.Mappings.Abstract;
using Mapster;

namespace Kanbersky.HC.Core.Mappings.Concrete.Mapster
{
    /// <summary>
    /// 
    /// </summary>
    public class MapsterMapping : IKanberskyMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
