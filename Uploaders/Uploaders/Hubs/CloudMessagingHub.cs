using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Uploaders.Hubs
{
    public class CloudMessagingHub : Hub
    {
        private readonly IHubContext _uptimeHub;
        public CloudMessagingHub()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<CloudMessagingHub>();
        }

    }
}