using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EndPoint_WebUi.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class JanAuthorization : Attribute, IAuthorizationFilter
{
    public string AccessType { get; set; }
    public JanAuthorization(string AccessType)
    {
        this.AccessType = AccessType;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;
        if (context!.HttpContext!.User!.Identity!.IsAuthenticated)
        {
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            var claims = claimsIdentity?.Claims;
            var type = claims!.Where(w => w.Value == AccessType).FirstOrDefault();
            if (type == null)
            {
                context.Result = new RedirectResult("/AccessDenied");


            }

        }
        else
        {
            context.Result = new RedirectResult("/AccessDenied");
       
        }
    }
}