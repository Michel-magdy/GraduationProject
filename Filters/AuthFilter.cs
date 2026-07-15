using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GraduationProject.Filters;

/// <summary>
/// Requires the user to be logged in. Redirects to Login if not.
/// </summary>
public class AuthFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
        }
        base.OnActionExecuting(context);
    }
}

/// <summary>
/// Requires the user to have a specific role. Redirects to AccessDenied if not.
/// </summary>
public class RoleFilter : ActionFilterAttribute
{
    private readonly string[] _roles;

    public RoleFilter(params string[] roles)
    {
        _roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl });
            return;
        }

        var userRole = context.HttpContext.Session.GetString("UserRole") ?? "";
        if (!_roles.Contains(userRole))
        {
            context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
        }

        base.OnActionExecuting(context);
    }
}
