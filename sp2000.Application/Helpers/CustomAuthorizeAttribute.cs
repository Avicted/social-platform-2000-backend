using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using sp2000.Application.Models;

namespace sp2000.Application.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {

        var user = context?.HttpContext.Items["User"] as ApplicationUser;

        if (user != null && context != null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized lol" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            /* context.Result = new CustomApiResponse()
            {
                Pagination = null,
                Code = 401,
                Message = "Unauthorized",
                Result = null,
            }; */
        }
    }
}
