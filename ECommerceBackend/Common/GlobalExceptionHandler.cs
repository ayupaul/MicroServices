using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;


namespace Common.Global_Exception_Handler
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandler(context, ex);
            }
        }
        private async Task ExceptionHandler(HttpContext context,Exception ex) { 

            var response=new {error=ex.Message};
            var payload=JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(payload);


        }
    }
}
