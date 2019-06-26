using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLibrary.WebSockets
{
    public class WebSocketManager
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _sockets
            = new ConcurrentDictionary<Guid, WebSocket>();

        public WebSocket GetSocketById(Guid id) =>
            _sockets.FirstOrDefault(s => s.Key == id).Value;

        public Guid GetSocketId(WebSocket socket) =>
            _sockets.FirstOrDefault(s => s.Value == socket).Key;

        public ConcurrentDictionary<Guid, WebSocket> GetAll() =>
            _sockets;

        public bool AddSocket(WebSocket socket, out Guid socketId)
        {
            socketId = Guid.NewGuid();
            return _sockets.TryAdd(socketId, socket);
        }

        public async Task<bool> RemoveSocket(Guid socketId)
        {
            _sockets.TryRemove(socketId, out var socket);

            if (socket == null)
                return false;

            await socket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Closed by the WebSocketManager",
                CancellationToken.None
            );

            return true;
        }
    }
}