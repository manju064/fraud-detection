// Copyright (c) Friss. All rights reserved.

using System.Net;

namespace Friss.FraudDetection.Api.Models.Responses.Exceptions
{
    /// <summary>
    /// BadRequestException for 400 status.
    /// </summary>
    public class BadRequestException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        public BadRequestException()
            : base(HttpStatusCode.BadRequest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="errorCode">error Code.</param>
        public BadRequestException(string message, string errorCode = "400")
            : base(HttpStatusCode.BadRequest, errorCode, message)
        {
        }
    }
}
