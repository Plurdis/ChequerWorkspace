using Microsoft.AspNetCore.Mvc;

namespace ChequerWorkspace
{
    public class XUserIdAttribute : FromHeaderAttribute
    {
        public const string HeaderName = "X-User-Id";

        public XUserIdAttribute()
        {
            Name = HeaderName;
        }
    }
}