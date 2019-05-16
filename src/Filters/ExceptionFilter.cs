using Microsoft.AspNetCore.Mvc.Filters;

namespace SafeLinks.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           throw new System.NotImplementedException();
        }
    }
}