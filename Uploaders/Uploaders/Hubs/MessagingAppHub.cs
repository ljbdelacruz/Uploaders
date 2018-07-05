using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Uploaders.Hubs
{
    public class MessagingAppHub : Hub
    {
        private readonly IHubContext _uptimeHub;
        public MessagingAppHub()
        {
            _uptimeHub = GlobalHost.ConnectionManager.GetHubContext<MessagingAppHub>();
        }
        //id = id of the newly created message
        public static void SendNotification(string id, string api, string roomID)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessagingAppHub>();
            hubContext.Clients.All.Sendnotify(id, api, roomID);
        }
        public static void LeaveRoom(string roomID, string userID, string api) {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessagingAppHub>();
            hubContext.Clients.All.Leaveroom(roomID, userID, api);
        }

    }
}