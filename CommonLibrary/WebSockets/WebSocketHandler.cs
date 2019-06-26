using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Common.WebSockets
{
    public abstract class WebSocketHandler
    {
        private readonly WebSocketManager _webSocketConnectionManager;

        protected WebSocketHandler(WebSocketManager webSocketConnectionManager) =>
            _webSocketConnectionManager = webSocketConnectionManager;

        public virtual bool OnConnected(WebSocket socket, out Guid socketId) =>
            _webSocketConnectionManager.AddSocket(socket, out socketId);

        public virtual async Task<bool> OnDisconnected(WebSocket socket) =>
            await _webSocketConnectionManager.RemoveSocket(
                _webSocketConnectionManager.GetSocketId(socket)
            );

        public async Task Send(
            WebSocket socket,
            byte[] message,
            CancellationToken token = default
        )
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(
                new ArraySegment<byte>(
                    message,
                    0,
                    message.Length
                ),
                WebSocketMessageType.Text,
                true,
                token);
        }

        public async Task Send(
            Guid socketId,
            byte[] message,
            CancellationToken token = default
        )
        {
            var socket = _webSocketConnectionManager
                .GetSocketById(socketId);
            await Send(socket, message, token);
        }

        public async Task SendToAll(
            byte[] message,
            CancellationToken token = default)
        {
            foreach (var webSocket in _webSocketConnectionManager
                .GetAll())
                if (webSocket.Value.State == WebSocketState.Open)
                    await Send(webSocket.Value, message, token);
        }

        public abstract Task Receive(
            Guid socketId,
            WebSocketReceiveResult result,
            byte[] buffer,
            CancellationToken token = default
        );
    }
}