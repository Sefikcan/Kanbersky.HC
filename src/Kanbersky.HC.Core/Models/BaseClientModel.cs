namespace Kanbersky.HC.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseClientModel<T> 
        where T: class
    {
        /// <summary>
        /// 
        /// </summary>
        public T Result { get; set; }
    }
}
