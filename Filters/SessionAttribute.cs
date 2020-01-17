using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener
{
    public class SessionAttribute : ActionFilterAttribute
    {
        public SessionAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserId") == null)
            {
                context.HttpContext.Session.SetString("UserId", Guid.NewGuid().ToString());
            }
            base.OnActionExecuting(context);
        }
    }
}
