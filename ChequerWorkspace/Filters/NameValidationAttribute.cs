using ChequerWorkspace.Models;
using ChequerWorkspace.Models.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChequerWorkspace.Filters
{
    public class NameValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var name = ((WorkspaceAddPayload)context.ActionArguments["body"]).Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse("Name should be exists", "NAME_NOT_EXISTS"));
            }
            else if (name?.Length > 500)
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse("Name is too long", "TOO_LONG_NAME"));
            }
        }
    }
}