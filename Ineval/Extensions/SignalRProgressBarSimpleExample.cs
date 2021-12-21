using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Ineval.Extensions
{
    [HubName("progressHub")]
    public class ProgressHub : Hub
    {

    }

    public class Functions
    {
        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            var percentage = (progressCount * 100) / totalItems;

            hubContext.Clients.All.AddProgress(progressMessage, percentage + "%", percentage);
        }

        public static void SendProgressAsignando(string progressMessage, int progressCount, int totalItems)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            var percentage = (progressCount * 100) / totalItems;

            hubContext.Clients.All.AddProgressAsignando(progressMessage, percentage + "%", percentage);
        }
    }
}