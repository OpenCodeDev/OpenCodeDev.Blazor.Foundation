namespace OpenCodeDev.Blazor.Foundation.Doc.Host
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DebugMiddleware
    {
        private readonly RequestDelegate _next;

        public DebugMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            foreach (var item in httpContext.Request.Headers)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            
            return _next(httpContext);
        }
    }

}
