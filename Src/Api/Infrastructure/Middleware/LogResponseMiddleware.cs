// Copyright (c) Friss. All rights reserved.

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Friss.FraudDetection.Api.Infrastructure.Middleware
{
    /// <summary>
    /// Log Response
    /// Credits: https://github.com/sulhome/log-request-response-middleware.
    /// </summary>
    public class LogResponseMiddleware : BaseLogMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogResponseMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="logger">ILogger.</param>
        public LogResponseMiddleware(RequestDelegate next, ILogger<LogResponseMiddleware> logger)
            : base(next, logger)
        {
        }

        /// <summary>
        /// Invoke Log Response Middleware.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>next Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            if (!bodyStream.CanRead || !bodyStream.CanWrite)
            {
                await this.Next(context);
                return;
            }

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await this.Next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            this.Logger.Log(LogLevel.Information, "20001", $"RESPONSE LOG: {responseBody}", null, this.DefaultFormatter);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }
    }
}
