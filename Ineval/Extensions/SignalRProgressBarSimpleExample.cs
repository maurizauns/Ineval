using Microsoft.AspNet.SignalR;

namespace Ineval.Extensions
{
    public class ProgressHub : Hub
    {

    }

    public class Functions
    {
        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            var percentage = (progressCount * 100) / totalItems;

            hubContext.Clients.All.AddProgress(progressMessage, percentage + "%");
        }
    }
}