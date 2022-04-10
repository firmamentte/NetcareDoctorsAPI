using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using NetcareDoctorsAPI.BLL.BLLClasses;
using System.Net;

namespace NetcareDoctorsAPI.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthenticateAdministratorApplicationUser : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;

        public AuthenticateAdministratorApplicationUser()
        {
            ApplicationUserBLL = new();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Username", out StringValues _username))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new ApiErrorResp("Username required"));
            }
            else
            {
                if (!await ApplicationUserBLL.IsAdministratorApplicationUser(_username.FirstOrDefault()))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new JsonResult(new ApiErrorResp("Request Forbidden"));
                }
            }
        }
    }
}
