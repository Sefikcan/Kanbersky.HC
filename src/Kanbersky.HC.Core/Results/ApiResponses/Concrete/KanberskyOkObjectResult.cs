using Kanbersky.HC.Core.Results.ApiResponses.Abstract;
using Kanbersky.HC.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.HC.Core.Results.ApiResponses.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KanberskyOkObjectResult<T> : ObjectResult, IKanberskyActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public KanberskyOkObjectResult(T result) : base(new KanberskyObjectModel<T>(result))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
