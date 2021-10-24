// Copyright (c) Friss. All rights reserved.

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace Friss.FraudDetection.Api.Infrastructure.Middleware
{
    /// <summary>
    /// Log request
    /// Credits: https://github.com/sulhome/log-request-response-middleware.
    /// </summary>
    public class LogRequestMiddleware : BaseLogMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogRequestMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="logger">ILogger.</param>
        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
            : base(next, logger)
        {
        }

        /// <summary>
        /// Invoke Log Request Middleware.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>next Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = context.Request.GetDisplayUrl();
            var requestBodyText = await new StreamReader(requestBodyStream).ReadToEndAsync();
            this.Logger.Log(LogLevel.Information, "10001", $"REQUEST METHOD: {context.Request.Method}, REQUEST BODY: {requestBodyText}, REQUEST URL: {url}", null, this.DefaultFormatter);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await this.Next(context);
            context.Request.Body = originalRequestBody;
        }
    }
}
