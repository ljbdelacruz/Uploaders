using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Uploaders.Hubs
{
    public class NotificationManagerHub : Hub
    {
        private readonly IHubContext _uptimeHub;
        public NotificationManagerHub()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<NotificationManagerHub>();
        }
        //owner id refers to the user id that will receive the notification
        //id is the notification id
        public static void SendNotification(string id, string api, string ownerID, string title, string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationManagerHub>();
            hubContext.Clients.All.Sendnotify(id, api, ownerID, title, message);
        }
        public static void Test() {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationManagerHub>();
            hubContext.Clients.All.Test("Testing");
        }
    }
}