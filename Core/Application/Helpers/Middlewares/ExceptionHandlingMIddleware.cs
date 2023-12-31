﻿using System.Text.Json;
using Application.Models.ExceptionModel;
using Application.Models.GenericRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Helpers.Middlewares;

public class ExceptionHandlingMIddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMIddleware(RequestDelegate next, ILogger<ExceptionHandlingMIddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        try
        {
            await _next(context);
            unitOfWork.Transaction.Commit();
        }
        catch (Exception e)
        {
            unitOfWork.Transaction.Rollback();
            List<ErrorDetails> problems = new List<ErrorDetails>();
            if (e is HttpException httpException)
            {
                problems.Add(new ErrorDetails()
                {
                    StatusCode = (int)httpException.HttpStatusCode,
                    Message = e.Message
                });
            }
            else if (e is ValidationException validationException)
            {
                foreach (var error in validationException.Errors)
                {
                    problems.Add(new ErrorDetails()
                    {
                        StatusCode = 422,
                        Message = error.ErrorMessage
                    });
                }
            }
            else
            {
                problems.Add(new ErrorDetails()
                {
                    StatusCode = 500,
                    Message = e.Message
                });
                _logger.LogError(e.Message);
            }

            context.Response.StatusCode = (int)problems[0].StatusCode;
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(problems);

            await context.Response.WriteAsync(json);
        }
    }
}