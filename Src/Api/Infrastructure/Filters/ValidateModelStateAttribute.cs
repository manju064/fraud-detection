﻿// Copyright (c) Friss. All rights reserved.

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Friss.FraudDetection.Api.Infrastructure.Filters
{
    /// <summary>
    /// Global filter to check model state.
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// On action executing filter.
        /// </summary>
        /// <param name="context">action context.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
