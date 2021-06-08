using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.HC.Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingDelegatingHandler> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                    _logger.LogInformation("Received a success response from {Url}", response.RequestMessage.RequestUri);
                else
                    _logger.LogWarning("Received a non-success status code {StatusCode} from {Url}", (int)response.StatusCode, response.RequestMessage.RequestUri);

                return response;
            }
            catch (Exception ex)
             when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionRefused)
            {
                var hostWithPort = request.RequestUri.IsDefaultPort
                    ? request.RequestUri.DnsSafeHost
                    : $"{request.RequestUri.DnsSafeHost}:{request.RequestUri.Port}";

                _logger.LogCritical(ex, "Unable to connect to {Host} Please check the configuration to ensure the correct URL for the service has beed configured.", hostWithPort);
            }

            return new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                RequestMessage = request
            };
        }
    }
}
