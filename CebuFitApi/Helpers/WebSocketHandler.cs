using System.Net.WebSockets;
using System.Text;

namespace CebuFitApi.Helpers
{
    public class WebSocketHandler
    {
        private readonly List<WebSocket> _sockets = new List<WebSocket>();

        public async Task HandleAsync(WebSocket webSocket)
        {
            _sockets.Add(webSocket);

            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _sockets.Remove(webSocket);
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        public async Task BroadcastMessageAsync(string message)
        {
            var messageBuffer = Encoding.UTF8.GetBytes(message);
            var tasks = _sockets.Select(socket =>
                socket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, CancellationToken.None)
            );

            await Task.WhenAll(tasks);
        }
    }
}
