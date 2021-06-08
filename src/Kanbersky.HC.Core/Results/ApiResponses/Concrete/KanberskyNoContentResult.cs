using Kanbersky.HC.Core.Results.ApiResponses.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.HC.Core.Results.ApiResponses.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class KanberskyNoContentResult : StatusCodeResult, IKanberskyActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        public KanberskyNoContentResult() : base(StatusCodes.Status204NoContent)
        {
        }
    }
}
