using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using TodoApp.Models;

namespace TodoApp.Api.Middleware
{
    public class TodoObjectResultExecutor : ObjectResultExecutor
    {
        public TodoObjectResultExecutor(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory, ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
        {
        }

        public override Task ExecuteAsync(ActionContext context, ObjectResult result)
        {
            ResponceWrapper responce = new ResponceWrapper
            {
                
                StatusCode = GetStatusCode(result.StatusCode),
                IsSuccess = GetStatusCode(result.StatusCode) == 200 ? true : false,
                Message = GetMessage(result.StatusCode),
                Value = HandleNullObject(result.Value)
            };

            result.Value = responce;

            return base.ExecuteAsync(context, result);
        }
        private int GetStatusCode(int? statusCode)
        {
            if (statusCode is not null)
            {
                return statusCode.Value;
            }
            return 500;
        }
        private string GetMessage(int? statusCode)
        {
            if (statusCode.HasValue)
            {
                return statusCode switch
                {
                    200 => "Success",
                    201 => "Created",
                    400 => "BadRequest",
                    401 => "Unauthorized",
                    403 => "Forbidden",
                    404 => "NotFound",
                    500 => "InternalServerError",
                    _ => "Error"
                };
            }
            return "Error";
        }
        private Object HandleNullObject(Object? obj)
        {
            if(obj is null)
            {
                return false;
            } 
            return obj;
        }
    }
}
