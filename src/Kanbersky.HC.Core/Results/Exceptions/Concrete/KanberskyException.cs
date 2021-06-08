using System;

namespace Kanbersky.HC.Core.Results.Exceptions.Concrete
{
    /// <summary>
    /// 
    /// </summary>
    public class KanberskyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static NotFoundException NotFoundException()
        {
            return new NotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static NotFoundException NotFoundException(string message)
        {
            return new NotFoundException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static NotFoundException NotFoundException(string message, Exception ex)
        {
            return new NotFoundException(message, ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static BadRequestException BadRequestException()
        {
            return new BadRequestException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static BadRequestException BadRequestException(string message)
        {
            return new BadRequestException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static BadRequestException BadRequestException(string message, Exception ex)
        {
            return new BadRequestException(message, ex);
        }
    }
}
