using Google.Apis.Services;

namespace Ineval.Controllers
{
    internal class DiscoveryService
    {
        internal readonly object Apis;
        private BaseClientService.Initializer initializer;

        public DiscoveryService(BaseClientService.Initializer initializer)
        {
            this.initializer = initializer;
        }
    }
}