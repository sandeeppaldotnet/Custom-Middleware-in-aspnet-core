using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CustomMiddlewareinasp.netcore.CustomMiddleware
{
    //Middleware 1
    public class CustomMiddlewareByIMiddlewareInterface : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //before action
            await context.Response.WriteAsync("Custom middleware start.");
            await next(context);
            //after action
            await context.Response.WriteAsync("Custom middleware end.");
        }
    }

    //middleware 2

    public static class UseCustomMiddlewareByExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddlewareByIMiddlewareInterface>();
        }
    }

   // middleware 3
    public class TestCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public TestCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            //before action
            await context.Response.WriteAsync(" TestCustom middleware start.");
            await _next(context);
            //after action
            await context.Response.WriteAsync(" testCustom middleware start.");
        }
    }
    //Extension method used to add the middleware to the HTTP request pipeline.
    public static class TestCustomModdleExtensions
    {
        public static IApplicationBuilder UseTestCustomMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomMiddlewareByIMiddlewareInterface>();
            return builder.UseMiddleware<TestCustomMiddleware>();
        }
    }



}
