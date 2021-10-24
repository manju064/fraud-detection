// Copyright (c) Friss. All rights reserved.

using System.Net;

namespace Friss.FraudDetection.Api.Models.Responses
{
    /// <summary>
    /// Object for sending successful api OK response.
    /// </summary>
    /// <typeparam name="T">data</typeparam>
    public record ApiOkResponse<T> : ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiOkResponse{T}"/> class.
        /// </summary>
        /// <param name="data">Generic type data</param>
        public ApiOkResponse(T data)
            : base(HttpStatusCode.OK)
            => this.Data = data;

        /// <summary>
        /// Gets Data.
        /// </summary>
        public T Data { get; }
    }
}
