using System.Linq;
using ChequerWorkspace.Database;
using ChequerWorkspace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChequerWorkspace.Filters
{
    public class WorkspaceValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var workspaceIdStr = (string) context.HttpContext.Request.RouteValues["workspaceId"];
            
            if (!int.TryParse(workspaceIdStr, out int workspaceId))
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse("Workspace Id is not valid", "INVALID_WORKSPACE_ID"));
                return;
            }
            
            if (MockDatabase.Workspaces.Count(i => i.Id == workspaceId) == 0)
            {
                context.Result = new NotFoundObjectResult(new ErrorResponse("Workspace does not exists", "WORKSPACE_NOT_EXISTS"));
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}