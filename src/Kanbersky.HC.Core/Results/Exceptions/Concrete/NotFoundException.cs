using Kanbersky.HC.Core.Results.Exceptions.Abstract;
using Microsoft.AspNetCore.Http;
using System;

namespace Kanbersky.HC.Core.Results.Exceptions.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundException : Exception, IKanberskyException
    {
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NotFoundException()
        {
            StatusCode = StatusCodes.Status404NotFound;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status404NotFound;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public NotFoundException(string message, Exception exception) : base(message, exception)
        {
            StatusCode = StatusCodes.Status404NotFound;
        }
    }
}
