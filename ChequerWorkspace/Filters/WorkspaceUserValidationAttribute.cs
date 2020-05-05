using System.Linq;
using ChequerWorkspace.Database;
using ChequerWorkspace.Models;
using ChequerWorkspace.Models.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChequerWorkspace.Filters
{
    public class WorkspaceUserValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var identifier = ((WorkspaceUserPayload)context.ActionArguments["body"]).Identifier;
            if (MockDatabase.Users.Count(i => i.Identifier == identifier) == 0)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorResponse("User does not exists", "INVALID_USER_IDENTIFIER"));
            }
        }
    }
}