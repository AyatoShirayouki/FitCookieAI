using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.ActionFilters
{
    public class AdminsDeveloperStatusFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetInt32("StatusId") != 1)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}
