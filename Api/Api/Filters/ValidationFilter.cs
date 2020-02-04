﻿using Api.Contracts.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors
                    .Select(x => x.ErrorMessage))
                    .ToArray();

                var errResponse = new ErrorResponse();

                foreach (var error in errors)
                {
                    foreach(var sub in error.Value)
                    {
                        var errorModel = new ErrorModel
                        {
                            FieldName = error.Key.ToLower(),
                            Message = sub
                        };

                        errResponse.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errResponse);
                return;
            }

            await next();
        }
    }
}
