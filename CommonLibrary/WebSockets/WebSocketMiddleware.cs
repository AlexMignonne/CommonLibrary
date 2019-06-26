using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Common.WebSockets
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(
            RequestDelegate next,
            WebSocketHandler webSocketHandler
        )
        {
            _next = next;
            WebSocketHandler = webSocketHandler;
        }

        private WebSocketHandler WebSocketHandler { get; }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = 400;
                return;
            }

            var token = context.RequestAborted;
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var successConnected = WebSocketHandler.OnConnected(socket, out var socketId);

            if (!successConnected)
                return;

            await Receive(socket, async (result, buffer) =>
                {
                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            await WebSocketHandler.Receive(
                                socketId,
                                result,
                                buffer,
                                token);
                            return;
                        case WebSocketMessageType.Close:
                            await WebSocketHandler.OnDisconnected(socket);
                            return;
                        case WebSocketMessageType.Text:
                            await WebSocketHandler.Receive(
                                socketId,
                                result, buffer,
                                token);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                },
                token);

            //await _next.Invoke(context);
        }

        private static async Task Receive(
            WebSocket socket,
            Action<WebSocketReceiveResult, byte[]> handleMessage,
            CancellationToken token
        )
        {
            var buffer = new byte[4 * 1024];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    token
                );

                handleMessage(result, buffer);
            }
        }
    }
}