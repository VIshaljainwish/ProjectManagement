using System.Threading.Tasks;

namespace ProjectManagement.CustomeMiddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Hanling Request: "+ context.Request.Path);
            await _next(context);
            Console.WriteLine("Finish the handling request.");
        }
    }
}
