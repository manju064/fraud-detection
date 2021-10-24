// Copyright (c) Friss. All rights reserved.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Friss.FraudDetection.Api.Infrastructure.Middleware
{
    /// <summary>
    /// Base class for logger middleware.
    /// </summary>
    public abstract class BaseLogMiddleware
    {
        /// <summary>
        /// Next processing pipeline.
        /// </summary>
        protected readonly RequestDelegate Next;

        /// <summary>
        /// the logger for logging.
        /// </summary>
        protected readonly ILogger<BaseLogMiddleware> Logger;

        /// <summary>
        /// default formatter for logs.
        /// </summary>
        protected readonly Func<string, Exception, string> DefaultFormatter = (state, exception) => $"{state} - {exception?.Message}";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="logger">ILogger.</param>
        protected BaseLogMiddleware(RequestDelegate next, ILogger<BaseLogMiddleware> logger)
        {
            this.Next = next;
            this.Logger = logger;
        }
    }
}
