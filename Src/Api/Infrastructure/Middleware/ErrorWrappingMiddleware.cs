// Copyright (c) Friss. All rights reserved.

using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Friss.FraudDetection.Api.Models.Responses;
using Friss.FraudDetection.Api.Models.Responses.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Friss.FraudDetection.Api.Infrastructure.Middleware
{
    /// <summary>
    /// Error handling middleware.
    /// </summary>
    public class ErrorWrappingMiddleware : BaseLogMiddleware
    {
        /// <summary>
        /// environment.
        /// </summary>
        private readonly IWebHostEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorWrappingMiddleware"/> class.
        /// </summary>
        /// <param name="next">RequestDelegate.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="env">IHostingEnvironment.</param>
        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger, IWebHostEnvironment env)
            : base(next, logger)
            => this.env = env;

        /// <summary>
        /// Invoke MW action.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>Task for next MW pipeline.</returns>
        public async Task Invoke(HttpContext context)
        {
            Exception? exception = default;
            var jsonResponse = string.Empty;
            try
            {
                await this.Next.Invoke(context);
            }
            catch (BadRequestException badReqEx)
            {
                HandleException(badReqEx, badReqEx.StatusCode, badReqEx.Message);
            }
            catch (NotFoundException notFoundEx)
            {
                HandleException(notFoundEx, notFoundEx.StatusCode, notFoundEx.Message);
            }
            catch (UnauthorizedException authEx)
            {
                HandleException(authEx, authEx.StatusCode, authEx.Message);
            }
            catch (ApiException apiEx)
            {
                HandleException(apiEx, apiEx.StatusCode, apiEx.Message);
            }
            catch (ArgumentOutOfRangeException argOutEx)
            {
                HandleException(argOutEx, HttpStatusCode.BadRequest, argOutEx.Message);
            }
            catch (ArgumentException argEx)
            {
                HandleException(argEx, HttpStatusCode.BadRequest, argEx.Message);
            }
            catch (Exception ex)
            {
                HandleException(ex, HttpStatusCode.InternalServerError, "Internal Server Error occurred");
            }

            void HandleException(Exception ex, HttpStatusCode httpStatusCode, string message)
            {
                exception = ex;
                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";

                var instanceId = $"urn:Friss:quotation:error:{Guid.NewGuid()}";
                var eventId = new EventId((int)httpStatusCode, instanceId);

                this.Logger.LogError(eventId, ex.Demystify(), ex.Message);

                // Log stack trace
                ApiResponse response = !this.env.IsProduction()
                    ? new JsonErrorResponse(instanceId, httpStatusCode, message, ex.Demystify().ToString())
                    : new JsonErrorResponse(instanceId, httpStatusCode, message);

                jsonResponse = JsonSerializer.Serialize(response);
            }

            if (!context.Response.HasStarted && exception != null)
            {
                await context.Response.WriteAsync(jsonResponse);
            }
        }

        /// <summary>
        /// Error response for exceptions.
        /// </summary>
        private record JsonErrorResponse : ApiResponse
        {
            public JsonErrorResponse(string instance, HttpStatusCode statusCode, string message, string? developerMessage = null)
                : base(statusCode, message)
            {
                this.DeveloperMessage = developerMessage;
                this.Instance = instance;
            }

            /// <summary>
            /// Gets detail stack trace.
            /// </summary>
            public string? DeveloperMessage { get; private set; }

            /// <summary>
            /// Gets a URI reference that identifies the specific occurrence of the problem.It may or may not yield further information if dereferences.
            /// </summary>
            public string Instance { get; private set; }
        }
    }
}
