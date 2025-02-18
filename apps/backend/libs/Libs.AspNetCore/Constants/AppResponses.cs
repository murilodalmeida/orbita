﻿using System.Linq;
using FluentValidation.Results;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Constants;

public static class AppResponses
{
    // 2XX
    public static IResult Ok() => TypedResults.Ok();
    public static IResult Ok<T>(T? value) => TypedResults.Ok(value);
    public static IResult Created<T>(T value) => TypedResults.Created(string.Empty, value);
    public static IResult NoContent() => TypedResults.NoContent();

    // 4XX
    public static IResult ValidationErrors(ValidationResult? result)
    {
        var problem = Problems.BadRequest("Validation Errors", "The request has validation issues, check the values and try again.");

        if (result is not null)
        {
            var errors = result.Errors
                .GroupBy(x => x.ErrorCode)
                .ToDictionary(
                    x => x.Key.Camelize(),
                    x => x.ToDictionary(x => x.PropertyName.Camelize(), x => x.ErrorMessage));

            problem.Extensions.Add("validationErrors", errors);
        }

        return Results.Problem(problem);
    }
    public static IResult BadRequest(string title, string message) => Results.Problem(Problems.BadRequest(title, message));
    public static IResult BadRequest(string message) => Results.Problem(Problems.BadRequest(message));
    public static IResult NotFound() => NotFound("Resource not found. Check the input values and try again");
    public static IResult NotFound(string message) => Results.Problem(Problems.NotFound(message));
    public static IResult UnprocessableEntity(string message) => Results.Problem(Problems.UnprocessableEntity(message));

    // 5XX
    public static IResult InternalServerError() => Results.Problem(Problems.InternalServerError());
    public static IResult InternalServerError(string message) => Results.Problem(Problems.InternalServerError(message));
}
