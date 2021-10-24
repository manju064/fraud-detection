// Copyright (c) Friss. All rights reserved.

using System.Net;

namespace Friss.FraudDetection.Api.Models.Responses.Exceptions
{
    /// <summary>
    /// NotFoundException for 404 status.
    /// </summary>
    public class NotFoundException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
            : base(HttpStatusCode.NotFound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="errorCode">errorCode.</param>
        public NotFoundException(string message, string errorCode = "404")
            : base(HttpStatusCode.NotFound, errorCode, message)
        {
        }
    }
}
