using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Survey_Project.ActionFilters
{
    public class GlobalRouting : IActionFilter
    {
            private readonly ClaimsPrincipal _claimsPrincipal;
            public GlobalRouting(ClaimsPrincipal claimsPrincipal)
            {
                _claimsPrincipal = claimsPrincipal;
            }
            public void OnActionExecuting(ActionExecutingContext context)
            {
                var controller = context.RouteData.Values["controller"];
                if (controller.Equals("Home"))
                {
                    if (_claimsPrincipal.IsInRole("Customer"))
                    {
                        context.Result = new RedirectToActionResult("Create",
                        "Customer", null);
                    }
                else if (_claimsPrincipal.IsInRole("Admin"))
                    {
                        context.Result = new RedirectToActionResult("Create",
                        "Admin", null);
                    }
                }
        }
            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        
    }
}
