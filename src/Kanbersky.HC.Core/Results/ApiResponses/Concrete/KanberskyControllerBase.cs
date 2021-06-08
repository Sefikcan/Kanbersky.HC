using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.HC.Core.Results.ApiResponses.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class KanberskyControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public static KanberskyCreatedObjectResult<TResult> ApiCreated<TResult>(TResult result) where TResult : class
        {
            return new KanberskyCreatedObjectResult<TResult>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public static KanberskyOkResult ApiOk()
        {
            return new KanberskyOkResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public static KanberskyOkObjectResult<TResult> ApiOk<TResult>(TResult result) where TResult : class
        {
            return new KanberskyOkObjectResult<TResult>(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public static KanberskyNoContentResult ApiNoContent()
        {
            return new KanberskyNoContentResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public static KanberskyUpdatedObjectResult<TResult> ApiUpdated<TResult>(TResult result) where TResult : class
        {
            return new KanberskyUpdatedObjectResult<TResult>(result);
        }
    }
}
