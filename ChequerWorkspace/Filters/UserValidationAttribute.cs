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
            var identifier = (string) context.HttpContext.Request.Headers["identifier"];

            if (MockDatabase.Users.Count(i => i.Identifier == identifier) == 0)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorResponse("User does not exists", "INVALID_USER_IDENTIFIER"));
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}