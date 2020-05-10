using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ChequerWorkspace.Binders
{
    public class GuidQueryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(Guid))
                return null;

            if (context.BindingInfo.BindingSource == BindingSource.Query)
                return new BinderTypeModelBinder(typeof(GuidQueryModelBinder));

            return null;
        }
    }
}