using Finsa.Caravan.WebService.Models.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading;

namespace Finsa.Caravan.WebService.Hubs
{
    [HubName("serverTime")]
    public sealed class ServerTimeHub : Hub
    {
        private readonly ServerTime _serverTime;

        public ServerTimeHub() : this(ServerTime.Instance)
        {
        }

        public ServerTimeHub(ServerTime serverTime)
        {
            _serverTime = serverTime;
        }
    }

    public sealed class ServerTime
    {
        // Singleton instance
        private static readonly Lazy<ServerTime> LazyInstance = new Lazy<ServerTime>(() => new ServerTime(GlobalHost.ConnectionManager.GetHubContext<ServerTimeHub>().Clients));

        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5);
        private readonly Timer _timer;

        private ServerTime(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
        }

        public static ServerTime Instance => LazyInstance.Value;

        private IHubConnectionContext<dynamic> Clients { get; set; }

        public DateTime GetServerTime() => DateTime.UtcNow;

        private void UpdateStockPrices(object state)
        {
            BroadcastStockPrice(new ServerUtcNow
            {
                UtcNow = GetServerTime()
            });
        }

        private void BroadcastStockPrice(ServerUtcNow utcNow)
        {
            Clients.All.getServerTime(utcNow);
        }
    }
}