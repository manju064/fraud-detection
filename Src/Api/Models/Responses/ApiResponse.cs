// Copyright (c) Friss. All rights reserved.

using System.Net;

namespace Friss.FraudDetection.Api.Models.Responses
{
    /// <summary>
    /// Api Response base class
    /// Credits: https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api.
    /// </summary>
    public record ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode.</param>
        public ApiResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Message = this.GetDefaultMessageForStatusCode(statusCode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode.</param>
        /// <param name="message">message.</param>
        public ApiResponse(HttpStatusCode statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        /// <summary>
        /// Gets status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets Message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Get Default message for statusCode.
        /// </summary>
        /// <param name="status">HttpStatusCode.</param>
        /// <returns>default messages.</returns>
        private string GetDefaultMessageForStatusCode(HttpStatusCode status)
            => status switch
            {
                HttpStatusCode.OK => "Success",
                HttpStatusCode.NotFound => "Resource not found. Please check the request.",
                HttpStatusCode.BadRequest => "Invalid input data, please verify.",
                HttpStatusCode.InternalServerError => "Internal Server Error, please try later.",
                HttpStatusCode.Unauthorized => "Unauthorized access, please login again.",
                _ => "Unknown status"
            };
    }
}
