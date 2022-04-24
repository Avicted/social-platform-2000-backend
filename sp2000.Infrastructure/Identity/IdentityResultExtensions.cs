/* using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Identity;
using sp2000.Application.Helpers;

namespace sp2000.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static CustomApiResponse ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? new CustomApiResponse(
                message: "User successfully created",
                statusCode: 201
            )
            : new CustomApiResponse(
                message: "Validation error",
                result: result.Errors,
                statusCode: 422
            );
    }
} */