using System;
using Common.Helpers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Common.Middlewares
{
    public class RemoteIpAddressMiddleware
    {
        private readonly RequestDelegate _next;

        public RemoteIpAddressMiddleware(RequestDelegate next) =>
            _next = next ?? throw new ArgumentNullException(nameof(next));

        public Task InvokeAsync(HttpContext context)
        {
            var remoteIpAddress = context.Request.HttpContext.Connection.RemoteIpAddress;
            RemoteIpContext.Set(remoteIpAddress);
            return _next(context);
        }
    }
}