using System.Linq;
using ChequerWorkspace.Database;
using ChequerWorkspace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ChequerWorkspace.Filters
{
    public class UserValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            var identifier = (string) context.HttpContext.Request.Headers[XUserIdAttribute.HeaderName];

            if (MockDatabase.Users.All(i => i.Identifier != identifier))
            {
                context.Result = new UnauthorizedObjectResult(new ErrorResponse("User does not exists", "INVALID_USER_IDENTIFIER"));
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}