using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChequerWorkspace.Binders
{
    public class GuidQueryModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.BindingSource.CanAcceptDataFrom(BindingSource.Path) &&
                bindingContext.HttpContext.Request.RouteValues.TryGetValue(bindingContext.FieldName, out var id) &&
                Guid.TryParse(id?.ToString() ?? string.Empty, out var workspaceId))
            {
                bindingContext.ModelState.SetModelValue(bindingContext.FieldName, id, workspaceId.ToString());
                bindingContext.Result = ModelBindingResult.Success(workspaceId);
            }
            else
            {
                bindingContext.ModelState.SetModelValue(bindingContext.FieldName, null, Guid.Empty.ToString());
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}