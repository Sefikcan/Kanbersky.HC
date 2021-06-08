namespace Kanbersky.HC.Core.Results.ApiResponses.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KanberskyObjectModel<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public KanberskyObjectModel(T result)
        {
            Result = result;
        }
    }
}
