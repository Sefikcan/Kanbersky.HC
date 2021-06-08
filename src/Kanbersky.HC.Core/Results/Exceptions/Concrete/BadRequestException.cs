using Kanbersky.HC.Core.Results.Exceptions.Abstract;
using Microsoft.AspNetCore.Http;
using System;

namespace Kanbersky.HC.Core.Results.Exceptions.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class BadRequestException : Exception, IKanberskyException
    {
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public BadRequestException()
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public BadRequestException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public BadRequestException(string message, Exception exception) : base(message, exception)
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
