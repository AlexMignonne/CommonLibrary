using System;
using System.Threading.Tasks;
using CommonLibrary.Helpers;
using Microsoft.AspNetCore.Http;

namespace CommonLibrary.Middlewares
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