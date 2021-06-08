using Kanbersky.HC.Core.Results.ApiResponses.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.HC.Core.Results.ApiResponses.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class KanberskyOkResult : StatusCodeResult, IKanberskyActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        public KanberskyOkResult() : base(StatusCodes.Status200OK)
        {
        }
    }
}
